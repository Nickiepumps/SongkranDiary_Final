using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneDropBombState : EnemyStateMachine
{
    public EnemyDroneDropBombState(EnemyDroneStateController droneEnemy) : base(droneEnemy) { }
    private const float PREPAREDROPTIMER = 0.2f;
    private float currentTime;
    public override void Start()
    {
        currentTime = PREPAREDROPTIMER; // Stop until the timer is up
    }
    public override void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            droneEnemy.isBombDropped = true;
            droneEnemy.droneBombGameObject.transform.SetParent(null);
            droneEnemy.droneBombGameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            droneEnemy.droneBombGameObject.GetComponent<BoxCollider2D>().enabled = true;
            droneEnemy.EnemyStateTransition(new EnemyDroneFlyState(droneEnemy));
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
