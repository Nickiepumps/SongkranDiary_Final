using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_ShootState : PlayerSideScrollStateMachine
{
    public SideScroll_ShootState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        Debug.Log("Shoot State");
    }
    public override void Update()
    {
        /*if (Input.GetMouseButtonUp(0) && playerSideScroll.xDir == 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
        }
        else if (Input.GetMouseButtonUp(0) && playerSideScroll.xDir != 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
        }*/
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
        /*if (pCollider.gameObject.tag == "Side_Floor")
        {
            playerSideScroll.currentCollidername = pCollider;
            playerSideScroll.isPlayerOnGround = true;
        }*/
    }

    public override void OnColliderExit(Collision2D pCollider)
    {
        /*playerSideScroll.currentCollidername = null;
        playerSideScroll.isPlayerOnGround = false;*/
    }
    public override void Exit()
    {

    }

    public override void OnColliderStay(Collision2D pCollider)
    {
        
    }
}
