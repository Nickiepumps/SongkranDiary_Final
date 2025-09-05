using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatKid_BossUlt2State : BossStateMachine
{
    public FatKid_BossUlt2State(FatKid_BossStateController fatKidBoss) : base(fatKidBoss) { }
    private float changestateTime = 3.83f;
    private float currentTime;
    public override void Start()
    {
        fatKidBoss.bossAnimator.SetBool("isIdle", true);
        fatKidBoss.bossAnimator.SetBool("isAim", false);
        currentTime = changestateTime;
        fatKidBoss.NotifyBoss(BossAction.Ult2);
    }
    public override void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            fatKidBoss.BossStateTransition(new FatKid_BossIdleState(fatKidBoss));
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
