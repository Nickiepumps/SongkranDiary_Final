using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantKid_BossUlt1State : BossStateMachine
{
    public ElephantKid_BossUlt1State(ElephantKid_BossStateController elephantKidBoss) : base(elephantKidBoss) { }
    private float currentAttackTime;
    private float ultTime = 10;
    private float currentUltTime;
    public override void Start()
    {
        elephantKidBoss.StartCoroutine(elephantKidBoss.Boss_BabyElephantCallOut());
        currentAttackTime = 0.8f;
        currentUltTime = ultTime;
    }
    public override void Update()
    {
        if(elephantKidBoss.isHeal == false)
        {
            currentUltTime -= Time.deltaTime;
        }
        currentAttackTime -= Time.deltaTime;
        if (currentAttackTime <= 0)
        {
            elephantKidBoss.NotifyBoss(BossAction.Ult);
            currentAttackTime = 0.8f;
        }
        if(currentUltTime <= 0)
        {
            elephantKidBoss.BossStateTransition(new ElephantKid_BossIdleState(elephantKidBoss));
        }
        if (elephantKidBoss.bossHP.currentBossHP <= elephantKidBoss.bossHP.bossMaxHP - ((elephantKidBoss.bossHP.bossMaxHP * 25) / 100) * elephantKidBoss.healCount
            && elephantKidBoss.healCount < 4)
        {
            elephantKidBoss.healCount++;
            elephantKidBoss.StartCoroutine(elephantKidBoss.StartBossHealAnimation());
            //elephantKidBoss.BossStateTransition(new ElephantKid_BossHealState(elephantKidBoss));
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
