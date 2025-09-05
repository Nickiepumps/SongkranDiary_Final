using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_FallenState : PlayerSideScrollStateMachine
{
    public SideScroll_FallenState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Jump);
        playerSideScroll.isPlayerOnGround = false;
        playerSideScroll.playerAnimator.SetBool("Jump", true);
        playerSideScroll.playerAnimator.SetBool("Crouch", false);
        playerSideScroll.playerAnimator.SetBool("Dash", false);
        playerSideScroll.playerCollider.size = playerSideScroll.playerJumpColliderSize;
        playerSideScroll.playerCollider.offset = playerSideScroll.playerJumpColliderOffset;
        playerSideScroll.playerCollider.excludeLayers = playerSideScroll.pitfallLayerMask;
        playerSideScroll.playerRB.gravityScale = 0;
        playerSideScroll.playerRB.velocity = Vector2.zero;
        playerSideScroll.playerRB.AddForce(Vector2.up * playerSideScroll.jumpForce, ForceMode2D.Impulse);
    }
    public override void Update()
    {
        if (playerSideScroll.isPlayerOnGround == true || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (playerSideScroll.playerBulletShooting.isAim == false)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && playerSideScroll.isDash == false)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DashState(playerSideScroll));
        }
        if (playerSideScroll.playerCurrentHP <= 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DeadState(playerSideScroll));
        }
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
