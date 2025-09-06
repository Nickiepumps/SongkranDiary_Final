using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantKid_NormalAttack1State : BossStateMachine
{
    public ElephantKid_NormalAttack1State(ElephantKid_BossStateController elephantKidBoss) : base(elephantKidBoss) { }
    private float currentAttackTime;
    private int shotCount = 0;
    public override void Start()
    {
        // Play normal attack anim both idle and active variant
        if(elephantKidBoss.normalAttackCount == 1)
        {
            elephantKidBoss.StartCoroutine(elephantKidBoss.Boss_ElephantKidNormalAtk_Intro());
        }
        elephantKidBoss.isNormalAttack = true;
        currentAttackTime = elephantKidBoss.bossScriptableObject.aspd;
    }
    public override void Update()
    {
        if (elephantKidBoss.isHeal == false)
        {
            currentAttackTime -= Time.deltaTime;
        }
        if (currentAttackTime <= 0 && shotCount < 3)
        {
            elephantKidBoss.NotifyBoss(BossAction.Shoot);
            currentAttackTime = 2f;
            shotCount++;
        }
        else if(shotCount >= 3)
        {
            elephantKidBoss.isNormalAttack = false;
            elephantKidBoss.BossStateTransition(new ElephantKid_BossIdleState(elephantKidBoss));
        }
        if(elephantKidBoss.isShoot == false)
        {
            if (elephantKidBoss.bossHP.currentBossHP <= elephantKidBoss.bossHP.bossMaxHP - ((elephantKidBoss.bossHP.bossMaxHP * 25) / 100) * elephantKidBoss.healCount
            && elephantKidBoss.healCount < 4)
            {
                elephantKidBoss.healCount++;
                elephantKidBoss.StartCoroutine(elephantKidBoss.StartBossHealAnimation());
            }
        }
        if (elephantKidBoss.bossHP.currentBossHP <= 0)
        {
            elephantKidBoss.BossStateTransition(new ElephantKid_BossDieState(elephantKidBoss));
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
