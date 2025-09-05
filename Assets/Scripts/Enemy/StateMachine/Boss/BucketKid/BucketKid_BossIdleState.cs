using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketKid_BossIdleState : BossStateMachine
{
    public BucketKid_BossIdleState(BucketKid_BossStateController bucketKidBoss) : base(bucketKidBoss) { }
    
    private float currentIdleTime;
    private float currentUltTime;
    public override void Start()
    {
        if(bucketKidBoss.isGameStart == true)
        {
            bucketKidBoss.bossAnimator.SetBool("isIdle", true);
            bucketKidBoss.bossAnimator.SetBool("isFullBody", false);
            bucketKidBoss.bossAnimator.SetBool("isTransition", false);
            bucketKidBoss.bossAnimator.SetFloat("transitionVariant", 0);
        }
        else
        {
            bucketKidBoss.StartCoroutine(bucketKidBoss.BossStartGameAnim());
        }

        bucketKidBoss.NotifyBoss(BossAction.Idle);
        bucketKidBoss.isBossInvulnerable = false;
        bucketKidBoss.bossUlt1 = false;
        bucketKidBoss.bossUlt2 = false;
        currentIdleTime = bucketKidBoss.bossScriptableObject.idleTime;
        bucketKidBoss.bossSpriteRenderer.sprite = bucketKidBoss.bossScriptableObject.idleSprite;
        bucketKidBoss.normalAttackPattern = Random.Range(1, 3);
    }
    public override void Update()
    {
        if(bucketKidBoss.isGameStart == true && bucketKidBoss.isFinishIntro == true)
        {
            currentIdleTime -= Time.deltaTime;
            if(currentIdleTime <= 0)
            {
                currentIdleTime = bucketKidBoss.bossScriptableObject.idleTime;
                if(bucketKidBoss.normalAttackCount > 2 && bucketKidBoss.isBossGoOutFromBarrel == false)
                {
                    bucketKidBoss.BossStateTransition(new BucketKid_Ult1State(bucketKidBoss));
                }
                else if(bucketKidBoss.isBossGoOutFromBarrel == true)
                {
                    bucketKidBoss.BossStateTransition(new BucketKid_Ult2State(bucketKidBoss));
                }
                else
                {
                    if (bucketKidBoss.normalAttackPattern == 1)
                    {
                        bucketKidBoss.BossStateTransition(new BucketKid_ThrowBalloonState(bucketKidBoss));
                    }
                    else
                    {
                        bucketKidBoss.BossStateTransition(new BucketKid_ThrowBoomerangState(bucketKidBoss));
                    }
                }
            }
            if (bucketKidBoss.bossHP.currentBossArmor <= 0)
            {
                bucketKidBoss.BossStateTransition(new BucketKid_BarrelTransitionState(bucketKidBoss));
            }
            else if (bucketKidBoss.bossHP.currentBossHP <= 0)
            {
                bucketKidBoss.BossStateTransition(new BucketKid_BossDieState(bucketKidBoss));
            }
        }
    }
    public override void FixedUpdate()
    {
    }
    public override void OnTriggerEnter(Collider2D eCollider)
    {
    }
    public override void OnTriggerExit(Collider2D eCollider)
    {

    }
    public override void OnColliderEnter(Collision2D pCollider)
    {

    }
    public override void OnColliderExit(Collision2D pCollider)
    {

    }
    public override void Exit()
    {
        
    }
}
