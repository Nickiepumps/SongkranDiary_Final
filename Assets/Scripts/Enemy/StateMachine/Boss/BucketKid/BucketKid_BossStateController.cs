using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BucketKid_BossObserverController))]
public class BucketKid_BossStateController : BossSubject
{
    private BossStateMachine currentBossState;
    [Header("Boss Scriptable Object")]
    public BossScriptableObject bossScriptableObject;

    [Header("Boss Animator")]
    public Animator bossAnimator;

    [Header("Boss Properties")]
    public BossHealth bossHP;
    public SpriteRenderer bossSpriteRenderer;
    public Rigidbody2D bossRB;
    public CircleCollider2D ultCollider;
    public BoxCollider2D normalCollider;
    public CircleCollider2D ultHitBox;
    public BoxCollider2D normalHitBox;

    [Header("Boss Audio Properties")]
    public AudioSource bossAttackAudioSource;
    public AudioSource bossStatusAudioSource;
    public AudioSource bossEffectAudioSource;
    public AudioClip[] bossAttackAudioClipArr;
    public AudioClip[] bossEffectAudioClipArr;

    // Hide in inspector
    public bool isDead = false;
    public bool isBossInvulnerable = false;
    public bool isGameStart = false;
    public bool isFinishIntro = false;
    public bool isReadyToAttack = false;
    public bool isBossThrowingBoomerang = false;
    public bool isBossThrowingBalloon = false;
    public int normalAttackPattern = 0;
    public bool bossUlt1 = false;
    public bool bossUlt2 = false;
    public bool isBossGoOutFromBarrel = false;
    public int normalAttackCount = 0;
    private void Start()
    {
        BossStateTransition(new BucketKid_BossIdleState(this));
    }
    private void Update()
    {
        currentBossState.Update();
    }
    private void FixedUpdate()
    {
        currentBossState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("PlayerBullet"):
                if (isDead == false && isBossInvulnerable == false)
                {
                    NotifyBoss(BossAction.Damaged);
                }
                break;
            case ("PlayerUlt"):
                if (isDead == false && isBossInvulnerable == false)
                {
                    NotifyBoss(BossAction.UltDamaged);
                }
                break;
        }
        currentBossState.OnTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentBossState.OnTriggerExit(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentBossState.OnColliderEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentBossState.OnColliderExit(collision);
    }
    public void BossStateTransition(BossStateMachine newBossState)
    {
        currentBossState = newBossState;
        currentBossState.Start();
    }
    public IEnumerator BossBarrelAttackPreparationAnim()
    {
        isReadyToAttack = false;
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isPrepareToAttack", true);
        bossAnimator.SetBool("isFullBody", false);
        bossAnimator.SetBool("isTransition", false);
        bossAnimator.SetFloat("transitionVariant", 0);
        yield return new WaitForSeconds(0.8f);
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isAttack", true);
        bossAnimator.SetBool("isPrepareToAttack", false);
        bossAnimator.SetBool("isFullBody", false);
        bossAnimator.SetBool("isTransition", false);
        bossAnimator.SetFloat("transitionVariant", 0);
        isReadyToAttack = true;
    }
    public IEnumerator SwitchToBarrel()
    {
        // Play boss switch to barrel anim
        bossAnimator.SetFloat("transitionVariant", 1);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isFullBody", true);
        bossAnimator.SetBool("isTransition", true);
        yield return new WaitForSeconds(1.5f);
        bossEffectAudioSource.clip = bossEffectAudioClipArr[2];
        bossEffectAudioSource.loop = false;
        bossEffectAudioSource.Play();
        Debug.Log("Playing switch to barrel anim");
        yield return new WaitForSeconds(1.2f);
        // Play boss barrel idle anim
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isFullBody", false);
        bossAnimator.SetBool("isTransition", false);
        bossAnimator.SetFloat("transitionVariant", 0);
        isBossGoOutFromBarrel = false;
        bossHP.currentBossArmor = bossHP.bossMaxArmor;
        BossStateTransition(new BucketKid_BossIdleState(this));
    }
    public IEnumerator BossStartGameAnim()
    {
        yield return new WaitForSeconds(1.5f);
        // Play boss switch to barrel anim
        bossAnimator.SetFloat("transitionVariant", 1);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isFullBody", true);
        bossAnimator.SetBool("isTransition", true);
        yield return new WaitForSeconds(1.5f);
        bossEffectAudioSource.clip = bossEffectAudioClipArr[2];
        bossEffectAudioSource.loop = false;
        bossEffectAudioSource.Play();
        yield return new WaitForSeconds(1.2f);
        Debug.Log("Playing switch to barrel anim");
        // Play boss barrel idle anim
        bossAnimator.SetFloat("transitionVariant", 0);
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isFullBody", false);
        bossAnimator.SetBool("isTransition", false);
        isFinishIntro = true;
        isBossGoOutFromBarrel = false;
        bossHP.currentBossArmor = bossHP.bossMaxArmor;
    }
    public IEnumerator SwitchToFullbody()
    {
        bossEffectAudioSource.clip = bossEffectAudioClipArr[0];
        bossEffectAudioSource.Play();
        isReadyToAttack = false;
        bossAnimator.SetFloat("transitionVariant", 0);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isThrowingBoomerang", false);
        bossAnimator.SetBool("isThrowingBalloon", false);
        bossAnimator.SetBool("isFullBody", false);
        bossAnimator.SetBool("isTransition", true);
        Debug.Log("Playing switch to fullbody anim");
        yield return new WaitForSeconds(2.8f);
        isBossGoOutFromBarrel = true;
        isReadyToAttack = true;
        BossStateTransition(new BucketKid_BossFullBodyIdleState(this));
    }
    public IEnumerator PlayFullbodyUltAnim()
    {
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isFullBody", true);
        bossAnimator.SetBool("isUlt", true);
        yield return new WaitForSeconds(0.3f);
        bossAttackAudioSource.clip = bossAttackAudioClipArr[1];
        bossAttackAudioSource.loop = false;
        bossAttackAudioSource.Play();
        yield return new WaitForSeconds(1.3f);
        bossEffectAudioSource.clip = bossEffectAudioClipArr[1];
        bossEffectAudioSource.loop = false;
        bossEffectAudioSource.Play();
        yield return new WaitForSeconds(0.3f);
        bossUlt2 = true;
        NotifyBoss(BossAction.Ult);
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isFullBody", true);
        bossAnimator.SetBool("isUlt", false);
    }
    public IEnumerator PlayBarrelUltAnim()
    {
        bossAnimator.SetBool("isUlt", true);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", true);
        bossAnimator.SetBool("isPrepareToAttack", false);
        bossAttackAudioSource.clip = bossAttackAudioClipArr[1];
        bossAttackAudioSource.Play();
        yield return new WaitForSeconds(1.5f);
        bossAnimator.SetBool("isUlt", true);
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isAttack", true);
        bossAnimator.SetBool("isPrepareToAttack", false);
        bossAttackAudioSource.clip = bossAttackAudioClipArr[2];
        bossAttackAudioSource.loop = true;
        bossAttackAudioSource.Play();
        yield return new WaitForSeconds(2.1f);
        bossAnimator.SetBool("isUlt", false);
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isAttack", true);
        bossAnimator.SetBool("isPrepareToAttack", false);
        bossAttackAudioSource.clip = bossAttackAudioClipArr[3];
        bossAttackAudioSource.loop = true;
        bossAttackAudioSource.Play();
        bossUlt1 = true;
    }
}
