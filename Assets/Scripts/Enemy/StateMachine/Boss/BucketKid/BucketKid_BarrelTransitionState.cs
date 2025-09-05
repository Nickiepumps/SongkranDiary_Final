using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketKid_BarrelTransitionState : BossStateMachine
{
    public BucketKid_BarrelTransitionState(BucketKid_BossStateController bucketKidBoss) : base(bucketKidBoss) { }
    public override void Start()
    {
        bucketKidBoss.NotifyBoss(BossAction.Idle);
        bucketKidBoss.isBossInvulnerable = true;
        bucketKidBoss.normalAttackCount = 0;
        if(bucketKidBoss.bossHP.currentBossArmor <= 0 && bucketKidBoss.isBossGoOutFromBarrel == false)
        {
            bucketKidBoss.StartCoroutine(bucketKidBoss.SwitchToFullbody());
        }
        else if(bucketKidBoss.bossHP.currentBossArmor <= 0 && bucketKidBoss.isBossGoOutFromBarrel == true)
        {
            bucketKidBoss.StartCoroutine(bucketKidBoss.SwitchToBarrel());
        }
    }
    public override void Update()
    {
        
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
