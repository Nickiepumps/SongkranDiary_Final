using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberRunState : EnemyStateMachine
{
    public EnemyBomberRunState(EnemyBomberStateController bomberEnemy) : base(bomberEnemy) { }
    private Vector2 moveDir;
    public override void Start()
    {
    }
    public override void Update()
    {
        if(bomberEnemy.currentEnemyHP <= 0)
        {
            bomberEnemy.EnemyStateTransition(new EnemyBomberExplodeState(bomberEnemy));
        }
        if (bomberEnemy.enemySpriteRenderer.flipX == false)
        {
            bomberEnemy.transform.position += new Vector3(bomberEnemy.walkSpeed, 0, 0) * Time.deltaTime;
        }
        else
        {
            bomberEnemy.transform.position += new Vector3(-bomberEnemy.walkSpeed, 0, 0) * Time.deltaTime;
        }
    }
    public override void FixedUpdate()
    {
        
    }
    public override void OnColliderEnter(Collision2D pCollider)
    {

    }
    public override void OnColliderExit(Collision2D pCollider)
    {

    }
    public override void OnTriggerEnter(Collider2D eCollider)
    {
        if(eCollider.tag == "Player")
        {
            bomberEnemy.currentEnemyHP = 0;   
        }
        if (eCollider.tag == "EnemyJump" && bomberEnemy.isOnGround == true)
        {
            bomberEnemy.EnemyStateTransition(new EnemyBomberJumpState(bomberEnemy));
        }
    }
    public override void OnTriggerExit(Collider2D eCollider)
    {

    }
    public override void Exit()
    {

    }
}
