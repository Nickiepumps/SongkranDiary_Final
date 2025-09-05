using UnityEngine;

public class EnemyShooterIdleState : EnemyStateMachine
{
    public EnemyShooterIdleState(EnemyShooterStateController shooterEnemy) : base(shooterEnemy) { }
    private float currentASPD;
    public override void Start()
    {
        Debug.Log("Enemy Idle State");
        currentASPD = shooterEnemy.enemyASPD;
    }
    public override void Update()
    {
        Vector2 enemyFacingDirection = shooterEnemy.transform.position - shooterEnemy.player.transform.position;
        shooterEnemy.distanceFromPlayer = Vector2.Distance(shooterEnemy.transform.position, shooterEnemy.player.transform.position);
        Debug.DrawLine(shooterEnemy.transform.position, shooterEnemy.player.transform.position, Color.red);
        if(shooterEnemy.distanceFromPlayer > 3)
        {
            shooterEnemy.EnemyStateTransition(new EnemyShooterRunState(shooterEnemy));
        }
        if (enemyFacingDirection.x < 0)
        {
            shooterEnemy.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else
        {
            shooterEnemy.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        currentASPD -= Time.deltaTime;
        if(currentASPD <= 0)
        {
            shooterEnemy.NotifyNormalEnemy(EnemyAction.Shoot);
            currentASPD = shooterEnemy.enemyASPD;
        }
    }
    public override void FixedUpdate()
    {

    }
    public override void Exit()
    {
        
    }
    public override void OnTriggerEnter(Collider2D eCollider)
    {

    }

    public override void OnTriggerExit(Collider2D eCollider)
    {

    }

    public override void OnColliderEnter(Collision2D pCollider)
    {
        
    }

    public override void OnColliderExit(Collision2D pCollider)
    {
        
    }
}
