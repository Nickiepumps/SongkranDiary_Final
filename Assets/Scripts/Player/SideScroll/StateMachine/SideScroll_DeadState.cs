using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_DeadState : PlayerSideScrollStateMachine
{
    public SideScroll_DeadState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        playerSideScroll.isDead = true;
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Dead);
        // Play Dead Anim and Dead Sound
        playerSideScroll.gameObject.SetActive(false);
    }
    public override void Update()
    {

    }
    public override void FixedUpdate()
    {

    }
    public override void OntriggerEnter(Collider2D pCollider)
    {

    }
    public override void OntriggerExit(Collider2D pCollider)
    {

    }
    public override void OnColliderEnter(Collision2D pCollider)
    {

    }
    public override void OnColliderStay(Collision2D pCollider)
    {

    }
    public override void OnColliderExit(Collision2D pCollider)
    {

    }
    public override void Exit()
    {

    }
}
