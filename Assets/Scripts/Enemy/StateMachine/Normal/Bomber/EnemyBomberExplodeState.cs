using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberExplodeState : EnemyStateMachine
{
    public EnemyBomberExplodeState(EnemyBomberStateController bomberEnemy) : base(bomberEnemy) { }
    public override void Start()
    {
        bomberEnemy.isDead = true;
        bomberEnemy.NotifyNormalEnemy(EnemyAction.Explode); // Play explode animation
    }
    public override void Update()
    {
        if(bomberEnemy.currentEnemyHP > 0)
        {
            bomberEnemy.EnemyStateTransition(new EnemyBomberRunState(bomberEnemy));
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
