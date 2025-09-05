using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_IdleState : PlayerSideScrollStateMachine
{
    public SideScroll_IdleState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    private Vector2 moveDir;
    private bool isRamp;
    public override void Start()
    {
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Side_Idle);
        playerSideScroll.playerAnimator.SetBool("Idle", true);
        playerSideScroll.playerAnimator.SetBool("Run", false);
        playerSideScroll.playerAnimator.SetBool("Dash", false);
        if (playerSideScroll.isPlayerOnGround == false)
        {
            playerSideScroll.playerAnimator.SetBool("Jump", true);
            if(playerSideScroll.isPullByVacuum == true)
            {
                if (playerSideScroll.playerSpriteRenderer.flipX == false)
                {
                    moveDir = new Vector2(playerSideScroll.walkSpeed, playerSideScroll.playerRB.velocity.y);
                }
                else
                {
                    moveDir = new Vector2(-playerSideScroll.walkSpeed, playerSideScroll.playerRB.velocity.y);
                }
            }
            playerSideScroll.playerCollider.offset = playerSideScroll.playerJumpColliderOffset;
            playerSideScroll.playerCollider.size = playerSideScroll.playerJumpColliderSize;
        }
        else
        {
            playerSideScroll.playerCollider.offset = playerSideScroll.playerStandColliderOffset;
            playerSideScroll.playerCollider.size = playerSideScroll.playerStandColliderSize;
        }
        playerSideScroll.playerAnimator.SetBool("Crouch", false);

        if(playerSideScroll.currentCollider != null)
        {
            playerSideScroll.isDash = false;
        }
        else
        {
            playerSideScroll.playerAnimator.SetBool("Jump", true);
        }
        playerSideScroll.currentWalkSpeed = playerSideScroll.walkSpeed;
        playerSideScroll.playerCollider.sharedMaterial = null;
        playerSideScroll.playerRB.sharedMaterial = null;
        
    }
    public override void Update()
    {
        if (playerSideScroll.isGameStart == true)
        {
            playerSideScroll.xDir = playerSideScroll.CheckHorizontalInput() * playerSideScroll.walkSpeed;
            if (playerSideScroll.CheckHorizontalInput() != 0) // Change to Run state
            {
                if (playerSideScroll.playerBulletShooting.isAim == false)
                {
                    playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
                }
            }
            if (Input.GetKeyDown(playerSideScroll.keymapSO.jump/*KeyCode.Z*/) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
            {
                if (playerSideScroll.GetComponent<BulletAiming>().isAimUp == false)
                {
                    playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
                }
                //playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
            }
            if (Input.GetKey(playerSideScroll.keymapSO.crouch/*KeyCode.DownArrow*/) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
            {
                if (playerSideScroll.GetComponent<BulletAiming>().isAimUp == false)
                {
                    playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_CrouchState(playerSideScroll));
                }
            }
            if(Input.GetKeyDown(playerSideScroll.keymapSO.dash/*KeyCode.LeftShift*/) && playerSideScroll.isDash == false)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DashState(playerSideScroll));
            }
            if (playerSideScroll.playerCurrentHP <= 0)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DeadState(playerSideScroll));
            }
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
        if (isRamp == false && playerSideScroll.isPullByVacuum == true && playerSideScroll.isPlayerOnGround == true)
        {
            playerSideScroll.playerRB.velocity = new Vector2(playerSideScroll.externalPullVelocity.x, playerSideScroll.playerRB.velocity.y);
        }
    }
    public override void OntriggerEnter(Collider2D pCollider)
    {
        if(pCollider.gameObject.tag == "VacuumArea")
        {
            playerSideScroll.isPullByVacuum = true;
        }
    }
    public override void OntriggerExit(Collider2D pCollider)
    {
        playerSideScroll.isPullByVacuum = false;
    }
    public override void OnColliderEnter(Collision2D pCollider)
    {
        if(pCollider.gameObject.tag == "Side_Floor" || pCollider.gameObject.tag == "Side_Interactable")
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if(normal.y <= 1 && normal.y > -1 && normal.y != 0)
            {
                if (playerSideScroll.isPlayerHighFall == true)
                {
                    playerSideScroll.playerMovementAudioSource.clip = playerSideScroll.playerAudioClipArr[0];
                    playerSideScroll.playerMovementAudioSource.Play();
                }
                if (pCollider.collider.usedByEffector == false)
                {
                    playerSideScroll.playerCollider.excludeLayers = playerSideScroll.floorLayerMaskExclude;
                }
                playerSideScroll.isFallen = false;
                playerSideScroll.isDash = false;
                playerSideScroll.isPlayerOnGround = true;
                playerSideScroll.playerCollider.enabled = true;
                playerSideScroll.currentCollider = pCollider.collider;
                playerSideScroll.playerAnimator.SetBool("Jump", false);
            }
            if (normal.x != -1 && normal.x != 1 && normal.x != 0)
            {
                isRamp = true;
            }
            playerSideScroll.playerCollider.offset = playerSideScroll.playerStandColliderOffset;
            playerSideScroll.playerCollider.size = playerSideScroll.playerStandColliderSize;
        }
    }
    public override void OnColliderStay(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor" || pCollider.gameObject.tag == "Side_Interactable" && pCollider.collider.usedByEffector == false)
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if (normal.x != -1 && normal.x != 1 && normal.x != 0)
            {
                playerSideScroll.playerRB.velocity = new Vector2(playerSideScroll.playerRB.velocity.x - (normal.x * 0.8f), playerSideScroll.playerRB.velocity.y);
            }
            if (normal == Vector2.left && pCollider.collider.usedByEffector == false)
            {
                //playerSideScroll.currentWalkSpeed = 0f;
                playerSideScroll.playerCollider.sharedMaterial = playerSideScroll.antislipPhysicMat;
                playerSideScroll.playerRB.sharedMaterial = playerSideScroll.antislipPhysicMat;
            }
            else if (normal == Vector2.right && pCollider.collider.usedByEffector == false)
            {
                playerSideScroll.playerCollider.sharedMaterial = playerSideScroll.antislipPhysicMat;
                playerSideScroll.playerRB.sharedMaterial = playerSideScroll.antislipPhysicMat;
            }
        }
    }
    public override void OnColliderExit(Collision2D pCollider)
    {
        playerSideScroll.playerCollider.sharedMaterial = null;
        playerSideScroll.playerRB.sharedMaterial = null;
        if (pCollider.gameObject.tag == "Side_Floor" && pCollider.collider == playerSideScroll.currentCollider)
        {
            playerSideScroll.isPlayerOnGround = false;
            playerSideScroll.currentCollider = null;
            isRamp = false;
        }
    }
    public override void Exit()
    {

    }
}
