using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantKid_BossHealState : BossStateMachine
{
    public ElephantKid_BossHealState (ElephantKid_BossStateController elephantKidBoss) : base(elephantKidBoss) { }
    public override void Start()
    {
        elephantKidBoss.StartCoroutine(elephantKidBoss.StartBossHealAnimation());
    }
    public override void Update()
    {
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
