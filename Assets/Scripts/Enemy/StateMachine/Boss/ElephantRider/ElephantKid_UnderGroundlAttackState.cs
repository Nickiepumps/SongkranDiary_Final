using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantKid_UnderGroundlAttackState : BossStateMachine
{
    public ElephantKid_UnderGroundlAttackState(ElephantKid_BossStateController elephantKidBoss) : base(elephantKidBoss) { }
    public override void Start()
    {
        elephantKidBoss.StartCoroutine(elephantKidBoss.Boss_ElephantKidUndergroundAtk_Intro());
    }
    public override void Update()
    {
        Obstacle_FireHydrant component = elephantKidBoss.undergroundUltPatternList.PatternList[elephantKidBoss.undergroundPattern].incomingBullet[elephantKidBoss.undergroundUltPatternList.PatternList[elephantKidBoss.undergroundPattern].incomingBullet.Count - 1].GetComponent<Obstacle_FireHydrant>();
        if (component.isShoot == true)
        {
            elephantKidBoss.StartCoroutine(elephantKidBoss.Boss_ElephantKidUndergroundAtk_Outro());
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
