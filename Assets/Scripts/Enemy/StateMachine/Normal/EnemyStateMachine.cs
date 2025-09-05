using UnityEngine;

public abstract class EnemyStateMachine
{
    protected EnemyShooterStateController shooterEnemy;
    protected EnemyBomberStateController bomberEnemy;
    protected EnemyStationaryStateController stationaryEnemy;
    protected EnemyDroneStateController droneEnemy;
    public EnemyStateMachine(EnemyShooterStateController shooterEnemy)
    {
        this.shooterEnemy = shooterEnemy;
    }
    public EnemyStateMachine(EnemyBomberStateController bomberEnemy)
    {
        this.bomberEnemy = bomberEnemy;
    }
    public EnemyStateMachine(EnemyStationaryStateController stationaryEnemy)
    {
        this.stationaryEnemy = stationaryEnemy; 
    }
    public EnemyStateMachine(EnemyDroneStateController droneEnemy)
    {
        this.droneEnemy = droneEnemy;
    }
    public abstract void Start();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void OnTriggerEnter(Collider2D eCollider);
    public abstract void OnTriggerExit(Collider2D eCollider);
    public abstract void OnColliderEnter(Collision2D pCollider);
    public abstract void OnColliderExit(Collision2D pCollider);
    public abstract void Exit();
}
