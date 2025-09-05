using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketKid_Ult2State : BossStateMachine
{
    public BucketKid_Ult2State(BucketKid_BossStateController bucketKidBoss) : base(bucketKidBoss) { }
    private float ult2Time = 10f;
    private float currentUlt2Time;
    public override void Start()
    {
        
        //bucketKidBoss.bossUlt2 = true;
        currentUlt2Time = ult2Time;
        //bucketKidBoss.NotifyBoss(BossAction.Ult);
        bucketKidBoss.StartCoroutine(bucketKidBoss.PlayFullbodyUltAnim());
    }
    public override void Update()
    {
        if(bucketKidBoss.bossUlt2 == true)
        {
            currentUlt2Time -= Time.deltaTime;
            if (currentUlt2Time <= 0)
            {
                bucketKidBoss.BossStateTransition(new BucketKid_BossFullBodyIdleState(bucketKidBoss));
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
