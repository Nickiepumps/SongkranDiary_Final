using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneExplodetate : EnemyStateMachine
{
    public EnemyDroneExplodetate(EnemyDroneStateController droneEnemy) : base(droneEnemy) { }
    public override void Start()
    {
        droneEnemy.droneBombGameObject.SetActive(false);
        droneEnemy.NotifyNormalEnemy(EnemyAction.Explode);
    }
    public override void Update()
    {
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
