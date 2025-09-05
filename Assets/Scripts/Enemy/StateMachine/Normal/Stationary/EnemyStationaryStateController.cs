using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationaryStateController : NormalEnemySubject
{
    [Header("Player Reference")]
    public PlayerSideScrollStateController player;

    [Header("Enemy State Machine")]
    private EnemyStateMachine enemyCurrentState;

    [Header("Enemy General Properties")]
    public Rigidbody2D enemyRB;
    public NormalEnemyType normalEnemyType;
    public int currentEnemyHP;
    public float walkSpeed;
    public float enemyASPD;
    public int damage;
    public SpriteRenderer enemySpriteRenderer;
    public Transform destination;
    public Transform startPoint;
    public float distanceFromPlayer;

    // Hide from inspector
    public bool isDead = false;
    public int enemyHP;
    private void OnEnable()
    {
        currentEnemyHP = enemyHP;
    }
    private void Start()
    {
        player = GameObject.Find("Player_SideScroll").GetComponent<PlayerSideScrollStateController>();
        EnemyStateTransition(new EnemyStationaryIdleState(this));
    }
    private void Update()
    {
        enemyCurrentState.Update();
    }
    private void FixedUpdate()
    {
        enemyCurrentState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyCurrentState.OnTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyCurrentState.OnTriggerExit(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyCurrentState.OnColliderEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        enemyCurrentState.OnColliderExit(collision);
    }
    public void EnemyStateTransition(EnemyStateMachine newEnemyState)
    {
        enemyCurrentState = newEnemyState;
        enemyCurrentState.Start();
    }
}
