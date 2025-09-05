using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterDeadState : EnemyStateMachine
{
    public EnemyShooterDeadState(EnemyShooterStateController shooterEnemy) : base(shooterEnemy) { }
    public override void Start()
    {
        shooterEnemy.isDead = true;
        shooterEnemy.enemyAudioSource.clip = shooterEnemy.enemyAudioClipArr[1];
        shooterEnemy.enemyAudioSource.Play();
        shooterEnemy.NotifyNormalEnemy(EnemyAction.Dead);
    }
    public override void Update()
    {
        if(shooterEnemy.currentEnemyHP > 0)
        {
            shooterEnemy.EnemyStateTransition(new EnemyShooterRunState(shooterEnemy));
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
        
    }
    public override void OnTriggerExit(Collider2D eCollider)
    {
        
    }
    public override void Exit()
    {
        
    }
}
