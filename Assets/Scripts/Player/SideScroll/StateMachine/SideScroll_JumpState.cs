using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_JumpState : PlayerSideScrollStateMachine
{
    public SideScroll_JumpState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Jump);
        playerSideScroll.isPlayerOnGround = false;
        playerSideScroll.playerAnimator.SetBool("Jump", true);
        playerSideScroll.playerAnimator.SetBool("Crouch", false);
        playerSideScroll.playerAnimator.SetBool("Dash", false);
        playerSideScroll.playerCollider.offset = playerSideScroll.playerJumpColliderOffset;
        playerSideScroll.playerCollider.size = playerSideScroll.playerJumpColliderSize;
        playerSideScroll.playerRB.AddForce(Vector2.up * playerSideScroll.jumpForce, ForceMode2D.Impulse);
    }
    public override void Update()
    {
        if (playerSideScroll.isPlayerOnGround == true /*|| Input.GetAxisRaw("Horizontal") != 0*/)
        {
            if (playerSideScroll.playerBulletShooting.isAim == false)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
            }
        }
        else if (Input.GetKeyDown(playerSideScroll.keymapSO.dash/*KeyCode.LeftShift*/) && playerSideScroll.isDash == false)
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
        if (playerSideScroll.playerRB.velocity.y > 0 && playerSideScroll.isJump == true)
        {
            playerSideScroll.playerRB.velocity += playerSideScroll.gravityVelocity * playerSideScroll.jumpMultiplier * Time.fixedDeltaTime;
        }
        if (playerSideScroll.playerRB.velocity.y < 0)
        {
            playerSideScroll.playerRB.velocity -= playerSideScroll.gravityVelocity * playerSideScroll.fallMultiplier * Time.fixedDeltaTime;
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
        if (pCollider.gameObject.tag == "Side_Floor" || pCollider.gameObject.tag == "Side_Interactable")
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if(normal.y <= 1 && normal.y > -1 && normal.y != 0)
            {
                if (playerSideScroll.isPlayerHighFall == true)
                {
                    playerSideScroll.playerMovementAudioSource.clip = playerSideScroll.playerAudioClipArr[0];
                    playerSideScroll.playerMovementAudioSource.Play();
                }
                playerSideScroll.currentCollider = pCollider.collider;
                playerSideScroll.isPlayerOnGround = true;
                playerSideScroll.playerAnimator.SetBool("Jump", false);
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
            }
        }
    }
    public override void OnColliderStay(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor" || pCollider.gameObject.tag == "Side_Interactable" && pCollider.collider.usedByEffector == false)
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if (normal == Vector2.left && pCollider.collider.usedByEffector == false)
            {
                if (playerSideScroll.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == 1)
                {
                    playerSideScroll.playerCollider.sharedMaterial = playerSideScroll.antislipPhysicMat;
                    playerSideScroll.playerRB.sharedMaterial = playerSideScroll.antislipPhysicMat;
                }
                else
                {
                    playerSideScroll.playerCollider.sharedMaterial = null;
                    playerSideScroll.playerRB.sharedMaterial = null;
                }
            }
            else if (normal == Vector2.right && pCollider.collider.usedByEffector == false)
            {
                if (playerSideScroll.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == -1)
                {
                    //playerSideScroll.currentWalkSpeed = 0f;
                    playerSideScroll.playerCollider.sharedMaterial = playerSideScroll.antislipPhysicMat;
                    playerSideScroll.playerRB.sharedMaterial = playerSideScroll.antislipPhysicMat;
                }
                else
                {
                    //playerSideScroll.currentWalkSpeed = playerSideScroll.walkSpeed;
                    playerSideScroll.playerCollider.sharedMaterial = null;
                    playerSideScroll.playerRB.sharedMaterial = null;
                }
            }
        }
    }
    public override void OnColliderExit(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor")
        {
            playerSideScroll.currentWalkSpeed = playerSideScroll.walkSpeed;
            if (pCollider.collider == playerSideScroll.currentCollider)
            {
                playerSideScroll.isPlayerOnGround = false;
                playerSideScroll.currentCollider = null;
            }
            if (playerSideScroll.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ != 0)
            {
                if (playerSideScroll.playerBulletShooting.isAim == false)
                {
                    playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
                }
            }
            else
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
            }
        }
    }
    public override void Exit()
    {

    }
}
