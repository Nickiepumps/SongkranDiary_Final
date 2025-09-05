using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberStateController : NormalEnemySubject
{
    [Header("Player Reference")]
    public PlayerSideScrollStateController player;

    [Header("Enemy State Machine")]
    private EnemyStateMachine enemyCurrentState;

    [Header("Enemy General Properties")]
    [SerializeField] private NormalEnemySO enemyStats;
    public Rigidbody2D enemyRB;
    public NormalEnemyType normalEnemyType;
    public Animator enemyAnimator;
    public int currentEnemyHP;
    public float walkSpeed;
    public float jumpForce;
    public int damage;
    public SpriteRenderer enemySpriteRenderer;
    public Transform startPoint;
    public float distanceFromPlayer;

    // Hide from inspector
    public bool isDead = false;
    public bool isOnGround = true;
    private Camera cam;
    private void OnEnable()
    {
        isDead = false;
        normalEnemyType = enemyStats.NormalEnemyType;
        currentEnemyHP = enemyStats.hp;
        walkSpeed = enemyStats.movementSpeed;
        damage = enemyStats.damage;
        enemySpriteRenderer.sprite = enemyStats.normalSprite;
    }
    private void Start()
    {
        cam = Camera.main;
        player = GameObject.Find("Player_SideScroll").GetComponent<PlayerSideScrollStateController>();
        EnemyStateTransition(new EnemyBomberRunState(this));
    }
    private void Update()
    {
        enemyCurrentState.Update();
        Vector2 worldToViewportPos = cam.WorldToViewportPoint(transform.position);
        if (worldToViewportPos.x > 1.2f || worldToViewportPos.x < -0.2f)
        {
            Debug.Log("Reached Destination");
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        enemyCurrentState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("PlayerBullet"):
                NotifyNormalEnemy(EnemyAction.Damaged);
                break;
            case ("E_Boundary"):
                gameObject.SetActive(false);
                return;
        }
        enemyCurrentState.OnTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyCurrentState.OnTriggerExit(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Side_Floor")
        {
            isOnGround = true;
        }
        enemyCurrentState.OnColliderEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Side_Floor")
        {
            isOnGround = false;
        }
        enemyCurrentState.OnColliderExit(collision);
    }
    public void EnemyStateTransition(EnemyStateMachine newEnemyState)
    {
        enemyCurrentState = newEnemyState;
        enemyCurrentState.Start();
    }
}
