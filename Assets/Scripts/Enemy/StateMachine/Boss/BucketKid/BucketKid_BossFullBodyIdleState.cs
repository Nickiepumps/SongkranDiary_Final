using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketKid_BossFullBodyIdleState : BossStateMachine
{
    public BucketKid_BossFullBodyIdleState(BucketKid_BossStateController bucketKidBoss) : base(bucketKidBoss) { }
    
    private float currentIdleTime;
    private float currentUltTime;
    public override void Start()
    {
        bucketKidBoss.bossAnimator.SetBool("isIdle", true);
        bucketKidBoss.bossAnimator.SetBool("isFullBody", true);
        bucketKidBoss.bossAnimator.SetBool("isTransition", false);
        bucketKidBoss.bossAnimator.SetFloat("transitionVariant", 0);
        bucketKidBoss.NotifyBoss(BossAction.Idle);
        bucketKidBoss.isBossInvulnerable = false;
        bucketKidBoss.bossUlt1 = false;
        currentIdleTime = bucketKidBoss.bossScriptableObject.idleTime + 3;
    }
    public override void Update()
    {
        if(bucketKidBoss.isGameStart == true)
        {
            currentIdleTime -= Time.deltaTime;
            if(currentIdleTime <= 0 && bucketKidBoss.bossUlt2 == false && bucketKidBoss.isReadyToAttack == true)
            {
                currentIdleTime = bucketKidBoss.bossScriptableObject.idleTime;
                bucketKidBoss.BossStateTransition(new BucketKid_Ult2State(bucketKidBoss));
            }
            else if(currentIdleTime <= 0 && bucketKidBoss.bossUlt2 == true)
            {
                bucketKidBoss.bossUlt2 = false;
                bucketKidBoss.BossStateTransition(new BucketKid_BarrelTransitionState(bucketKidBoss));
            }
            if (bucketKidBoss.bossHP.currentBossHP <= 0)
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
