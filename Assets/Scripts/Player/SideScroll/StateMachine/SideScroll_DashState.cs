using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_DashState : PlayerSideScrollStateMachine
{
    public SideScroll_DashState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    private float DashTime = 0.3f;
    private float currentDashTime;
    public override void Start()
    {
        playerSideScroll.playerMovementAudioSource.clip = playerSideScroll.playerAudioClipArr[3];
        playerSideScroll.playerMovementAudioSource.Play();
        playerSideScroll.playerAnimator.SetBool("Dash", true);
        playerSideScroll.playerAnimator.SetBool("Idle", false);
        playerSideScroll.playerAnimator.SetBool("Run", false);
        playerSideScroll.playerAnimator.SetBool("Jump", false);
        
        playerSideScroll.playerRB.gravityScale = 0;
        currentDashTime = DashTime;
        playerSideScroll.isDash = true;
        if(playerSideScroll.playerSpriteRenderer.flipX == false)
        {
            playerSideScroll.xDir = playerSideScroll.dashSpeed;
        }
        else
        {
            playerSideScroll.xDir = -playerSideScroll.dashSpeed;
        }
    }
    public override void Update()
    {
        if(currentDashTime > 0)
        {
            currentDashTime -= Time.deltaTime;   
        }
        else
        {
            if (playerSideScroll.xDir != 0)
            {
                playerSideScroll.playerRB.gravityScale = 5;
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
            }
            else
            {
                playerSideScroll.playerRB.gravityScale = 5;
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
            }
            
        }
    }
    public override void FixedUpdate()
    {
        if(currentDashTime > 0)
        {
            playerSideScroll.playerRB.velocity = new Vector2(playerSideScroll.xDir, 0);
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
