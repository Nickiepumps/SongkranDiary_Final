using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(PlayerStateController player) : base(player) { }
    private float xDir, yDir;
    private float keyReleaseTime = 0.1f;
    private float currentKeyReleaseTime;
    private bool isUpKeyReleased = false;
    private bool isDownKeyReleased = false;
    public override void Start()
    {
        player.ISOAnimator.SetBool("IsWalk", true);
        currentKeyReleaseTime = keyReleaseTime;
    }
    public override void FixedUpdate()
    {
        Vector2 moveDir = new Vector2(xDir, yDir);
        moveDir = Vector2.ClampMagnitude(moveDir, player.walkSpeed);
        player.playerRB.velocity = moveDir;
    }

    public override void Update()
    {
        player.NotifyPlayerObserver(PlayerAction.Walk);
        xDir = player.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ * player.walkSpeed;
        yDir = player.CheckVerticalInput()/*Input.GetAxisRaw("Vertical")*/ * player.walkSpeed;

        if (player.CheckHorizontalInput()/*Input.GetAxis("Horizontal")*/ == 0 && player.CheckVerticalInput()/*Input.GetAxis("Vertical")*/ == 0)
        {
            player.PlayerStateTransition(new PlayerIdleState(player));
        }
        if (Input.GetKeyDown(player.keymapSO.interact/*KeyCode.E*/) && player.currentColHit != null)
        {
            player.PlayerStateTransition(new PlayerInteractState(player));
        }
        #region Walk Animation Blend tree
        if (player.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == 0 && player.CheckVerticalInput()/*Input.GetAxisRaw("Vertical")*/ == 1) // Up
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", 0f);
            player.ISOAnimator.SetFloat("PosY", 1f);
        }
        else if (player.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == 1 && player.CheckVerticalInput()/*Input.GetAxisRaw("Vertical")*/ == 1) // Up Right
        {
            player.ISOPlayerSpriteRenderer.flipX = true;
            player.ISOAnimator.SetFloat("PosX", -1f);
            player.ISOAnimator.SetFloat("PosY", 1f);
        }
        else if (player.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == -1 && player.CheckVerticalInput()/*Input.GetAxisRaw("Vertical")*/ == 1) // Up Left
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", -1f);
            player.ISOAnimator.SetFloat("PosY", 1f);
        }
        else if(player.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == 0 && player.CheckVerticalInput()/*Input.GetAxisRaw("Vertical")*/ == -1) // Down
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", 0f);
            player.ISOAnimator.SetFloat("PosY", -1f);
        }
        else if (player.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == 1 && player.CheckVerticalInput()/*Input.GetAxisRaw("Vertical")*/ == -1) // Down Right
        {
            player.ISOPlayerSpriteRenderer.flipX = true;
            player.ISOAnimator.SetFloat("PosX", -1f);
            player.ISOAnimator.SetFloat("PosY", -1f);
        }
        else if (player.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == -1 && player.CheckVerticalInput()/*Input.GetAxisRaw("Vertical")*/ == -1) // Down Left
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", -1f);
            player.ISOAnimator.SetFloat("PosY", -1f);
        }
        else if (player.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == 1 && player.CheckVerticalInput()/*Input.GetAxisRaw("Vertical")*/ == 0) // Right
        {
            player.ISOPlayerSpriteRenderer.flipX = true;
            player.ISOAnimator.SetFloat("PosX", -1f);
            player.ISOAnimator.SetFloat("PosY", 0f);
        }
        else if (player.CheckHorizontalInput()/*Input.GetAxisRaw("Horizontal")*/ == -1 && player.CheckVerticalInput()/*Input.GetAxisRaw("Vertical")*/ == 0) // Left
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", -1f);
            player.ISOAnimator.SetFloat("PosY", 0f);
        }
        #endregion

        #region DoubleKeyRelease (To Do: Fix the flicker sprite later)
        if (Input.GetKeyUp(player.keymapSO.top/*KeyCode.UpArrow*/))
        {
            isUpKeyReleased = true;
        }
        else if (Input.GetKeyUp(player.keymapSO.bottom/*KeyCode.DownArrow*/))
        {
            isDownKeyReleased = true;
        }
        if (isUpKeyReleased == true)
        {
            currentKeyReleaseTime -= Time.deltaTime;
            if (currentKeyReleaseTime >= 0f && Input.GetKeyUp(player.keymapSO.right/*KeyCode.RightArrow*/))
            {
                player.ISOPlayerSpriteRenderer.flipX = true;
                player.ISOAnimator.SetFloat("PosX", -1f);
                player.ISOAnimator.SetFloat("PosY", 1f);
                isUpKeyReleased = false;
            }
            else if (currentKeyReleaseTime >= 0f && Input.GetKeyUp(player.keymapSO.left/*KeyCode.LeftArrow*/))
            {
                player.ISOPlayerSpriteRenderer.flipX = false;
                player.ISOAnimator.SetFloat("PosX", -1f);
                player.ISOAnimator.SetFloat("PosY", 1f);
                isUpKeyReleased = false;
            }
        }
        else if(isDownKeyReleased == true)
        {
            currentKeyReleaseTime -= Time.deltaTime;
            if (currentKeyReleaseTime >= 0f && Input.GetKeyUp(player.keymapSO.right/*KeyCode.RightArrow*/))
            {
                player.ISOPlayerSpriteRenderer.flipX = true;
                player.ISOAnimator.SetFloat("PosX", -1f);
                player.ISOAnimator.SetFloat("PosY", -1f);
                isDownKeyReleased = false;
            }
            else if (currentKeyReleaseTime >= 0f && Input.GetKeyUp(player.keymapSO.left/*KeyCode.LeftArrow*/))
            {
                player.ISOPlayerSpriteRenderer.flipX = false;
                player.ISOAnimator.SetFloat("PosX", -1f);
                player.ISOAnimator.SetFloat("PosY", -1f);
                isDownKeyReleased = false;
            }
        }
        #endregion
    }
    public override void Exit()
    {

    }

    public override void OnEnterTrigger(Collider2D pCollision)
    {
        if(player.currentColHit == null)
        {
            player.currentColHit = pCollision;
        }
    }

    public override void OnExitTrigger(Collider2D pCollision)
    {
        player.currentColHit = null;
    }
}
