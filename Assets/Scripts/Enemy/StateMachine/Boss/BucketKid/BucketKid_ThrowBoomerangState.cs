using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketKid_ThrowBoomerangState : BossStateMachine
{
    public BucketKid_ThrowBoomerangState(BucketKid_BossStateController bucketKidBoss) : base(bucketKidBoss) { }
    private float currentAspd;
    private int throwCount;
    private int currentThrowCount;
    public override void Start()
    {
        bucketKidBoss.bossAnimator.SetBool("isThrowingBalloon", false);
        bucketKidBoss.StartCoroutine(bucketKidBoss.BossBarrelAttackPreparationAnim());

        bucketKidBoss.normalAttackCount++;
        currentAspd = 3;
        currentThrowCount = 0;
        throwCount = Random.Range(3, 5);
    }
    public override void Update()
    {
        currentAspd -= Time.deltaTime;
        if (currentAspd <= 0 && bucketKidBoss.isBossThrowingBoomerang == false && currentThrowCount < throwCount 
            && bucketKidBoss.isReadyToAttack == true)
        {
            bucketKidBoss.NotifyBoss(BossAction.Shoot);
            currentAspd = 3;
            currentThrowCount++;
        }
        else
        {
            // Play boss poke outside the barrel idle anim
        }
        if (currentThrowCount >= throwCount && bucketKidBoss.isBossThrowingBoomerang == false)
        {
            // Play boss hiding inside the barrel anim
            bucketKidBoss.BossStateTransition(new BucketKid_BossIdleState(bucketKidBoss));
        }
        if (bucketKidBoss.bossHP.currentBossArmor <= 0)
        {
            bucketKidBoss.BossStateTransition(new BucketKid_BarrelTransitionState(bucketKidBoss));
        }
        if (bucketKidBoss.bossHP.currentBossHP <= 0)
        {
            bucketKidBoss.BossStateTransition(new BucketKid_BossDieState(bucketKidBoss));
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
