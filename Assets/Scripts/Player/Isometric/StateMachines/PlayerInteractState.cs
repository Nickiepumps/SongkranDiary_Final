using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerState
{
    public PlayerInteractState(PlayerStateController player) : base(player) { }
    public override void Start()
    {
        if(player.currentColHit.tag == "NPC")
        {
            Debug.Log("Interact with NPC");
            player.playerTalking = true;
            player.NotifyPlayerObserver(PlayerAction.Talk);
        }
        else if(player.currentColHit.tag == "LevelLandMark")
        {
            Debug.Log("Interact with Landmark");
            player.NotifyPlayerObserver(PlayerAction.Stop);
        }
        player.playerVelocity = Vector3.zero;
        player.playerRB.velocity = Vector3.zero;
    }
    public override void FixedUpdate()
    {
        
    }
    public override void Update()
    {
        
    }
    public override void Exit()
    {
        
    }
    public override void OnEnterTrigger(Collider2D pCollision)
    {

    }
    public override void OnExitTrigger(Collider2D pCollision)
    {
        
    }
}
