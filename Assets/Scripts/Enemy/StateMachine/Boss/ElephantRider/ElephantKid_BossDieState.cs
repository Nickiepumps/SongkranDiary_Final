using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantKid_BossDieState : BossStateMachine
{
    public ElephantKid_BossDieState (ElephantKid_BossStateController elephantKidBoss) : base(elephantKidBoss) { }
    public override void Start()
    {
        elephantKidBoss.NotifyBoss(BossAction.Die);
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
