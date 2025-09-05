using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FatKid_BossStateController : BossSubject
{
    private BossStateMachine currentBossState;
    [Header("Boss Scriptable Object")]
    public BossScriptableObject bossScriptableObject;

    [Header("Audio Reference")]
    //public AudioClip[] enemyAudioClipArr;
    //public AudioSource enemyAudioSource;

    [Header("Boss Animator")]
    public Animator bossAnimator;    

    [Header("Boss Properties")]
    public BossHealth bossHP;
    public SpriteRenderer bossSpriteRenderer;
    public Rigidbody2D bossRB;
    public Transform destination; // For Boss 1
    public Transform originalPos; // For Boss 1
    public CircleCollider2D ultCollider;
    public BoxCollider2D normalCollider;
    public CircleCollider2D ultHitBox;
    public BoxCollider2D normalHitBox;

    // Hide in inspector
    public bool isGameStart = false;
    public bool startInitIdle = false;
    public bool bossShooting = false;
    public bool bossUlt = false;
    public bool reachLeftSide = false;
    public bool isJump = false;
    public bool isDead = false;
    private void Start()
    {
        BossStateTransition(new FatKid_BossIdleState(this));
    }
    private void Update()
    {
        currentBossState.Update();
    }
    private void FixedUpdate()
    {
        currentBossState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("PlayerBullet"):
                if(isDead == false)
                {
                    //enemyAudioSource.clip = enemyAudioClipArr[0];
                    //enemyAudioSource.Play();
                    NotifyBoss(BossAction.Damaged);
                }
                break;
        }
        currentBossState.OnTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentBossState.OnTriggerExit(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentBossState.OnColliderEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentBossState.OnColliderExit(collision);
    }
    public void BossStateTransition(BossStateMachine newBossState)
    {
        currentBossState = newBossState;
        currentBossState.Start();
    }
}
