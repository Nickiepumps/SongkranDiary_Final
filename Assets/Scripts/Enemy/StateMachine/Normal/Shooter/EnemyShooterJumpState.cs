using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterJumpState : EnemyStateMachine
{
    public EnemyShooterJumpState(EnemyShooterStateController shooterEnemy) : base(shooterEnemy) { }
    private float prepareTime = 1f;
    private float currentTime;
    private bool isJumping = false;
    Vector2 moveDir;
    public override void Start()
    {
        currentTime = prepareTime;
        //shooterEnemy.shooterEnemyAnimator.SetBool("isJump", true); Uncomment this when animation is ready
    }
    public override void Update()
    {
        currentTime -= Time.deltaTime;
        if (shooterEnemy.currentEnemyHP <= 0)
        {
            shooterEnemy.EnemyStateTransition(new EnemyShooterDeadState(shooterEnemy));
        }
        if (currentTime <= 0 && isJumping == false)
        {
            shooterEnemy.enemyRB.AddForce(Vector2.up * shooterEnemy.triggerJumpForce, ForceMode2D.Impulse);
            shooterEnemy.NotifyNormalEnemy(EnemyAction.Jump);
            isJumping = true;
        }
        if(isJumping == true)
        {
            if (shooterEnemy.enemySpriteRenderer.flipX == false)
            {
                shooterEnemy.transform.position += new Vector3(6, 0, 0) * Time.deltaTime;
            }
            else
            {
                shooterEnemy.transform.position += new Vector3(-6, 0, 0) * Time.deltaTime;
            }
        }
    }
    public override void FixedUpdate()
    {
        
    }
    public override void OnTriggerEnter(Collider2D eCollider)
    {

    }
    public override void OnTriggerExit(Collider2D eCollider)
    {
        if(eCollider.tag == "EnemyJump")
        {
            shooterEnemy.triggerJumpForce = shooterEnemy.jumpForce;
            shooterEnemy.EnemyStateTransition(new EnemyShooterRunState(shooterEnemy));
        }
    }
    public override void OnColliderEnter(Collision2D pCollider)
    {

    }
    public override void OnColliderExit(Collision2D pCollider)
    {

    }
    public override void Exit()
    {

    }
}
