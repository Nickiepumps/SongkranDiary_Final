using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_RunState : PlayerSideScrollStateMachine
{
    public SideScroll_RunState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    Vector2 moveDir;
    bool isRamp = false;
    public override void Start()
    {
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Run);
        playerSideScroll.playerAnimator.SetBool("Idle", false);
        playerSideScroll.playerAnimator.SetBool("Dash", false);
        playerSideScroll.playerAnimator.SetBool("Run", true);
        playerSideScroll.playerAnimator.SetBool("Crouch", false);
        if (playerSideScroll.isPlayerOnGround == false)
        {
            playerSideScroll.playerAnimator.SetBool("Jump", true);
            playerSideScroll.playerCollider.offset = playerSideScroll.playerJumpColliderOffset;
            playerSideScroll.playerCollider.size = playerSideScroll.playerJumpColliderSize;
        }
        else
        {
            playerSideScroll.playerCollider.offset = playerSideScroll.playerStandColliderOffset;
            playerSideScroll.playerCollider.size = playerSideScroll.playerStandColliderSize;
        }
        if(playerSideScroll.currentCollider != null)
        {
            playerSideScroll.isDash = false;
        }
        else
        {
            playerSideScroll.playerAnimator.SetBool("Jump", true);
        }
    }
    public override void Update()
    {
        playerSideScroll.xDir = playerSideScroll.CheckHorizontalInput() * playerSideScroll.currentWalkSpeed;
        if (playerSideScroll.CheckHorizontalInput() == 0 || playerSideScroll.playerBulletShooting.isAim == true)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
        }
        else if(Input.GetKeyDown(playerSideScroll.keymapSO.jump/*KeyCode.Z*/) && playerSideScroll.isPlayerOnGround == true)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
        }
        else if (Input.GetKeyDown(playerSideScroll.keymapSO.dash/*KeyCode.LeftShift*/) && playerSideScroll.isDash == false)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DashState(playerSideScroll));
        }
        if (Input.GetKeyDown(playerSideScroll.keymapSO.crouch/*KeyCode.DownArrow*/) && playerSideScroll.isPlayerOnGround == true)
        {
            if (playerSideScroll.GetComponent<BulletAiming>().isAimUp == false)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_CrouchState(playerSideScroll));
            }
        }
        if (playerSideScroll.playerCurrentHP <= 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DeadState(playerSideScroll));
        }

        if(playerSideScroll.isWinRunNGun == true)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_WinRunNGunState(playerSideScroll));
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
        if (isRamp == false)
        {
            moveDir = new Vector2(playerSideScroll.xDir, playerSideScroll.playerRB.velocity.y);
            playerSideScroll.playerRB.velocity = moveDir + playerSideScroll.externalPullVelocity;
            
        }
        else
        {
            playerSideScroll.playerRB.velocity = moveDir;
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
                if (pCollider.collider.usedByEffector == false)
                {
                    playerSideScroll.playerCollider.excludeLayers = playerSideScroll.floorLayerMaskExclude;
                }
                playerSideScroll.isFallen = false;
                playerSideScroll.isDash = false;
                playerSideScroll.isPlayerOnGround = true;
                playerSideScroll.playerCollider.sharedMaterial = null;
                playerSideScroll.playerRB.sharedMaterial = null;
                playerSideScroll.playerCollider.offset = playerSideScroll.playerStandColliderOffset;
                playerSideScroll.playerCollider.size = playerSideScroll.playerStandColliderSize;
                playerSideScroll.currentCollider = pCollider.collider;
                playerSideScroll.playerAnimator.SetBool("Jump", false);
            }
        }
    }
    public override void OnColliderStay(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor" || pCollider.gameObject.tag == "Side_Interactable" && pCollider.collider.usedByEffector == false)
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            
            if (normal != Vector2.right && normal.x > 0)
            {
                isRamp = true;
                // Calculate the speed when running uphil using ramp's x normal then add the remaining speed of player's normal speed
                float rampSpeed = (normal.x * playerSideScroll.xDir) + (playerSideScroll.walkSpeed - (normal.x * playerSideScroll.xDir));
                if (playerSideScroll.CheckHorizontalInput() == 1)
                {
                    moveDir = new Vector2(rampSpeed, -normal.y);
                }
                else if (playerSideScroll.CheckHorizontalInput() == -1)
                {
                    moveDir = new Vector2(-rampSpeed, normal.y);
                }
            }
            else if (normal != Vector2.left && normal.x < 0)
            {
                // To do: fix slow uphill movement
                isRamp = true;
                // Calculate the speed when running uphill using ramp's x normal then add the remaining speed of player's normal speed
                float rampSpeed = (-normal.x * playerSideScroll.xDir) + (playerSideScroll.walkSpeed - (-normal.x * playerSideScroll.xDir));
                if (playerSideScroll.CheckHorizontalInput() == 1)
                {
                    moveDir = new Vector2(rampSpeed, normal.y);
                }
                else if (playerSideScroll.CheckHorizontalInput() == -1)
                {
                    moveDir = new Vector2(-rampSpeed, -normal.y);
                }
            }
            else
            {
                isRamp = false;
            }
            if (normal == Vector2.left && pCollider.collider.usedByEffector == false)
            {
                if (playerSideScroll.CheckHorizontalInput() == 1)
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
                if (playerSideScroll.CheckHorizontalInput() == -1)
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
        }
    }
    public override void OnColliderExit(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor" || pCollider.gameObject.tag == "Side_Interactable")
        {
            isRamp = false;
            if(pCollider.collider == playerSideScroll.currentCollider)
            {
                playerSideScroll.isPlayerOnGround = false;
                playerSideScroll.currentCollider = null;
            }
            playerSideScroll.playerCollider.sharedMaterial = null;
            playerSideScroll.playerRB.sharedMaterial = null;
        }
    }
    public override void Exit()
    {
        
    }
}
