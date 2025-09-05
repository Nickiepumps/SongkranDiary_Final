using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneFlyState : EnemyStateMachine
{
    public EnemyDroneFlyState(EnemyDroneStateController droneEnemy) : base(droneEnemy) { }
    public override void Start()
    {
        droneEnemy.droneEnemyAnimator.SetBool("isFly", true);
        droneEnemy.droneEnemyAnimator.SetBool("isExplode", false);
    }
    public override void Update()
    {
        if (droneEnemy.enemySpriteRenderer.flipX == false)
        {
            droneEnemy.transform.position += new Vector3(droneEnemy.flySpeed, 0, 0) * Time.deltaTime;
        }
        else
        {
            droneEnemy.transform.position += new Vector3(-droneEnemy.flySpeed, 0, 0) * Time.deltaTime;
        }
        RaycastHit2D hit = Physics2D.Raycast(droneEnemy.transform.position, Vector2.down, 10f, LayerMask.GetMask("Player"));
        if(hit == true && droneEnemy.isBombDropped == false)
        {
            droneEnemy.EnemyStateTransition(new EnemyDroneDropBombState(droneEnemy));
        }
        
        if(droneEnemy.currentEnemyHP <= 0)
        {
            droneEnemy.EnemyStateTransition(new EnemyDroneExplodetate(droneEnemy));
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
