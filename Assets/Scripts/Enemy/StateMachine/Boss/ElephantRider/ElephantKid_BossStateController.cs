using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ElephantKid_BossStateController : BossSubject
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
    public BoxCollider2D normalCollider;
    public BoxCollider2D normalHitBox;

    [Header("Boss UnderGround Attack Ultimate Pattern")]
    public IncomingBulletPatternList undergroundUltPatternList = new IncomingBulletPatternList();
    public PatternTimerList patternTimerList = new PatternTimerList();

    [Header("Boss Vacuum Ultimate Trigger")]
    public BoxCollider2D vacuumTrigger;

    // Hide in inspector
    public bool isDead = false;
    public bool isBossInvulnerable = false;
    public bool isHeal = false;
    public bool isKidAttack = false;
    public bool isElephantAttack = false;
    public bool isGameStart = false;
    public int normalAttackPattern = 0;
    public int normalAttackCount = 0;
    public int healCount = 0;
    public bool bossUlt1 = false;
    public bool bossUlt2 = false;
    public int undergroundPattern;
    private void Start()
    {
        BossStateTransition(new ElephantKid_BossIdleState(this));
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
                return;
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
    public void BossStateTransition(BossStateMachine newState)
    {
        currentBossState = newState;
        currentBossState.Start();
    }
    public IEnumerator StartBossHealAnimation()
    {
        isHeal = true;
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isAttack", true);
        bossAnimator.SetBool("isHeal", true);
        yield return new WaitForSeconds(2f);
        NotifyBoss(BossAction.Heal);
        bossAnimator.SetBool("isHeal", false);
        isHeal = false;
        //BossStateTransition(new ElephantKid_BossIdleState(this));
    }
    public IEnumerator StartBossVacuumAnimation()
    {
        bossAnimator.SetBool("isPrepareToAttack", false);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isUlt", true);
        bossAnimator.SetFloat("ultVariant", 0);
        vacuumTrigger.gameObject.SetActive(true);
        NotifyBoss(BossAction.Ult2);
        yield return new WaitForSeconds(8f);
        vacuumTrigger.gameObject.SetActive(false);
        BossStateTransition(new ElephantKid_BossIdleState(this));
    }
    public IEnumerator Boss_ElephantKidNormalAtk_Outro(BossStateMachine newState)
    {
        bossAnimator.SetBool("isPrepareToAttack", true);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isShoot", false);
        bossAnimator.SetFloat("prepareVariant", 1);
        yield return new WaitForSeconds(0.7f);
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isUlt", false);
        bossAnimator.SetBool("isPrepareToAttack", false);
        bossAnimator.SetBool("isShoot", false);
        bossAnimator.SetBool("isHeal", false);
        bossAnimator.SetFloat("prepareVariant", 0);
        bossAnimator.SetFloat("attackIdleVariant", 0);
        bossAnimator.SetFloat("ultVariant", 0);
        BossStateTransition(newState);
    }
    public IEnumerator Boss_ElephantKidNormalAtk_Intro()
    {
        bossAnimator.SetBool("isHeal", false);
        bossAnimator.SetBool("isPrepareToAttack", true);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isShoot", false);
        bossAnimator.SetFloat("prepareVariant", 0);
        yield return new WaitForSeconds(0.7f);
        bossAnimator.SetBool("isPrepareToAttack", false);
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isAttack", true);
        bossAnimator.SetBool("isShoot", false);
        bossAnimator.SetFloat("attackIdleVariant", 0);
    }
    public IEnumerator Boss_ElephantKidUndergroundAtk_Intro()
    {
        bossAnimator.SetBool("isHeal", false);
        bossAnimator.SetBool("isPrepareToAttack", true);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isShoot", false);
        bossAnimator.SetFloat("prepareVariant", 1);
        yield return new WaitForSeconds(0.7f);
        bossAnimator.SetBool("isPrepareToAttack", true);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetFloat("prepareVariant", 2);
        yield return new WaitForSeconds(0.7f);
        bossAnimator.SetBool("isPrepareToAttack", false);
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isAttack", true);
        bossAnimator.SetFloat("attackIdleVariant", 1);
        undergroundPattern = Random.Range(0, undergroundUltPatternList.PatternList.Count);
        for (int i = 0; i < undergroundUltPatternList.PatternList[undergroundPattern].incomingBullet.Count; i++)
        {
            Obstacle_FireHydrant component = undergroundUltPatternList.PatternList[undergroundPattern].incomingBullet[i].GetComponent<Obstacle_FireHydrant>();
            component.waterInitialCooldownTime = patternTimerList.PatternList[undergroundPattern].patternTimer[i];
            component.enabled = true;
            undergroundUltPatternList.PatternList[undergroundPattern].incomingBullet[i].gameObject.SetActive(true);
        }
    }
    public IEnumerator Boss_ElephantKidUndergroundAtk_Outro()
    {
        bossAnimator.SetBool("isPrepareToAttack", true);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isShoot", false);
        bossAnimator.SetFloat("prepareVariant", 3);
        yield return new WaitForSeconds(0.7f);
        BossStateTransition(new ElephantKid_BossIdleState(this));
    }
    public IEnumerator Boss_ElephantKidVacuum_Intro()
    {
        bossAnimator.SetBool("isPrepareToAttack", true);
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isShoot", false);
        bossAnimator.SetFloat("prepareVariant", 4);
        yield return new WaitForSeconds(0.7f);
        BossStateTransition(new ElephantKid_BossUlt1State(this));
    }
    public IEnumerator Boss_BabyElephantCallOut()
    {
        bossAnimator.SetBool("isIdle", false);
        bossAnimator.SetBool("isUlt", true);
        bossAnimator.SetFloat("ultVariant", 1);
        yield return new WaitForSeconds(1);
        bossAnimator.SetBool("isIdle", true);
        bossAnimator.SetBool("isAttack", false);
        bossAnimator.SetBool("isUlt", false);
        bossAnimator.SetBool("isPrepareToAttack", false);
        bossAnimator.SetFloat("ultVariant", 0);
    }
    #region Underground Attack Pattern
    [System.Serializable]
    public class UndergroundAttackPattern
    {
        public List<GameObject> incomingBullet;
    }
    [System.Serializable]
    public class IncomingBulletPatternList
    {
        public List<UndergroundAttackPattern> PatternList;
    
    }
    [System.Serializable]
    public class PatternTimer
    {
        public List<float> patternTimer;
    }
    [System.Serializable]
    public class PatternTimerList
    {
        public List<PatternTimer> PatternList;
    }
    #endregion
}
