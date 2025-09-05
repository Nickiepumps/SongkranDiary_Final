using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_CrouchState : PlayerSideScrollStateMachine
{
    public SideScroll_CrouchState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    private bool isRamp = false;
    public override void Start()
    {
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Crouch);
        playerSideScroll.playerAnimator.SetBool("Crouch", true);
        playerSideScroll.playerCollider.enabled = true;
        playerSideScroll.playerCollider.offset = playerSideScroll.playerCrouchColliderOffset;
        playerSideScroll.playerCollider.size = playerSideScroll.playerCrouchColliderSize;
    }
    public override void Update()
    {
        if (Input.GetKeyUp(playerSideScroll.keymapSO.bottom/*KeyCode.DownArrow*/) && playerSideScroll.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
        }
        else if (Input.GetKeyUp(playerSideScroll.keymapSO.bottom/*KeyCode.DownArrow*/) && playerSideScroll.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ != 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
        }
        else if (Input.GetKeyDown(playerSideScroll.keymapSO.jump/*KeyCode.Z*/))
        {
            if(playerSideScroll.currentCollider.usedByEffector == false)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
            }
            else
            {
                playerSideScroll.playerCollider.excludeLayers = playerSideScroll.platformDropdownLayerMaskExclude;
                playerSideScroll.currentCollider = null;
                playerSideScroll.isPlayerOnGround = false;
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
            }
        }
        if (playerSideScroll.playerCurrentHP <= 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DeadState(playerSideScroll));
        }
    }
    public override void FixedUpdate()
    {
        if (isRamp == false && playerSideScroll.isPullByVacuum == true && playerSideScroll.isPlayerOnGround == true)
        {
            playerSideScroll.playerRB.velocity = new Vector2(playerSideScroll.externalPullVelocity.x, playerSideScroll.playerRB.velocity.y);
        }
        else
        {
            playerSideScroll.playerRB.velocity = Vector2.zero;
        }
    }
    public override void OntriggerEnter(Collider2D pCollider)
    {
        
    }
    public override void OntriggerExit(Collider2D pCollider)
    {

    }
    public override void OnColliderEnter(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor")
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if (normal.y <= 1 && normal.y > -1 && normal.y != 0)
            {
                playerSideScroll.currentCollider = pCollider.collider;
            }
            playerSideScroll.isFallen = false;
        }
    }
    public override void OnColliderStay(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor" || pCollider.gameObject.tag == "Side_Interactable" && pCollider.collider.usedByEffector == false)
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if (normal.x != -1 && normal.x != 1 && normal.x != 0)
            {
                isRamp = true;
                playerSideScroll.playerRB.velocity = new Vector2(playerSideScroll.playerRB.velocity.x - (normal.x * 0.8f), playerSideScroll.playerRB.velocity.y);
            }
            else
            {
                isRamp = false;
            }
        }
    }
    public override void OnColliderExit(Collision2D pCollider)
    {
        if(pCollider.gameObject.tag == "Side_Floor" || pCollider.gameObject.tag == "Side_Interactable")
        {
            isRamp = false;
        }
    }
    public override void Exit()
    {

    }
}
