using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BucketKid_Ult1State : BossStateMachine
{
    public BucketKid_Ult1State(BucketKid_BossStateController bucketKidBoss) : base(bucketKidBoss) { }
    private float ultAttackTime = 0.2f;
    private float currentUltAttackTime;
    private int attackCount = 0;
    public override void Start()
    {
        bucketKidBoss.StartCoroutine(bucketKidBoss.PlayBarrelUltAnim());
        //bucketKidBoss.bossUlt1 = true;
        bucketKidBoss.normalAttackCount = 0;
        currentUltAttackTime = ultAttackTime;
    }
    public override void Update()
    {
        if(bucketKidBoss.bossUlt1 == true && bucketKidBoss.isReadyToAttack == true)
        {
            currentUltAttackTime -= Time.deltaTime;
            if(currentUltAttackTime <= 0 && attackCount <= 50)
            {
                bucketKidBoss.NotifyBoss(BossAction.Ult);
                currentUltAttackTime = ultAttackTime;
                attackCount++;
            }
        }
        if (attackCount >= 50)
        {
            bucketKidBoss.BossStateTransition(new BucketKid_BossIdleState(bucketKidBoss));
        }
        if (bucketKidBoss.bossHP.currentBossHP <= 0)
        {
            bucketKidBoss.BossStateTransition(new BucketKid_BossDieState(bucketKidBoss));
        }
        if(attackCount < 50 && bucketKidBoss.bossHP.currentBossArmor <= 0)
        {
            bucketKidBoss.isBossInvulnerable = true;
        }
        else
        {
            bucketKidBoss.isBossInvulnerable = false;
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
