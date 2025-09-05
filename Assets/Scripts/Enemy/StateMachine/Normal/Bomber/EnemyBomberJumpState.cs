using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberJumpState : EnemyStateMachine
{
    public EnemyBomberJumpState(EnemyBomberStateController enemyBomber) : base(enemyBomber) { }
    private float prepareTime = 1f;
    private float currentTime;
    private bool isJumping = false;
    public override void Start()
    {
        currentTime = prepareTime;
    }
    public override void Update()
    {
        currentTime -= Time.deltaTime;
        if (bomberEnemy.currentEnemyHP <= 0)
        {
            bomberEnemy.EnemyStateTransition(new EnemyBomberExplodeState(bomberEnemy));
        }
        if (currentTime <= 0 && isJumping == false)
        {
            bomberEnemy.enemyRB.AddForce(Vector2.up * bomberEnemy.jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
        if (isJumping == true)
        {
            if (bomberEnemy.enemySpriteRenderer.flipX == false)
            {
                bomberEnemy.transform.position += new Vector3(-6, 0, 0) * Time.deltaTime;
            }
            else
            {
                bomberEnemy.transform.position += new Vector3(6, 0, 0) * Time.deltaTime;
            }
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
        if (eCollider.tag == "Player")
        {
            bomberEnemy.currentEnemyHP = 0;
        }
    }
    public override void OnTriggerExit(Collider2D eCollider)
    {
        if (eCollider.tag == "EnemyJump")
        {
            bomberEnemy.EnemyStateTransition(new EnemyBomberRunState(bomberEnemy));
        }
    }
    public override void Exit()
    {

    }
}
