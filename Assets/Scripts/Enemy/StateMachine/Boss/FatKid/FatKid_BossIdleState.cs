using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatKid_BossIdleState : BossStateMachine
{
    public FatKid_BossIdleState(FatKid_BossStateController fatKidBoss) : base(fatKidBoss) { }
    private float currentAspd;
    private float currentIdleTime;
    private float currentUltTime;
    public override void Start()
    {
        fatKidBoss.bossAnimator.enabled = true;
        fatKidBoss.bossAnimator.SetBool("isIdle", true);
        fatKidBoss.bossAnimator.SetBool("isAim", false);

        fatKidBoss.bossSpriteRenderer.sprite = fatKidBoss.bossScriptableObject.idleSprite;
        currentUltTime = fatKidBoss.bossScriptableObject.ultCooldown;
        currentAspd = fatKidBoss.bossScriptableObject.aspd;
        fatKidBoss.normalCollider.enabled = true;
        fatKidBoss.ultCollider.enabled = false;
        fatKidBoss.normalHitBox.enabled = true;
        fatKidBoss.ultHitBox.enabled = false;
        fatKidBoss.isJump = false;

        if (fatKidBoss.startInitIdle == false)
        {
            currentIdleTime = fatKidBoss.bossScriptableObject.initialIdleTime;
            fatKidBoss.startInitIdle = true;
        }
        else
        {
            currentIdleTime = fatKidBoss.bossScriptableObject.idleTime;
        }
        
    }
    public override void Update()
    {
        if(fatKidBoss.bossShooting == false && fatKidBoss.bossUlt == false && fatKidBoss.isGameStart == true)
        {
            currentAspd -= Time.deltaTime;
            //currentIdleTime -= Time.deltaTime;
            currentUltTime -= Time.deltaTime;
            if (currentAspd <= 0)
            {
                fatKidBoss.NotifyBoss(BossAction.Shoot);
                currentAspd = fatKidBoss.bossScriptableObject.aspd;
            }
            else
            {
                fatKidBoss.bossAnimator.SetBool("isIdle", false);
                fatKidBoss.bossAnimator.SetBool("isAim", true);
            }
            if (currentUltTime <= 0)
            {
                int ultVariant = Random.Range(1, 3);
                //int ultVariant = 2;
                if (ultVariant == 1)
                {
                    fatKidBoss.NotifyBoss(BossAction.Ult);
                    currentUltTime = fatKidBoss.bossScriptableObject.ultCooldown;
                    fatKidBoss.BossStateTransition(new FatKid_BossUltState(fatKidBoss));
                }
                else
                {
                    currentUltTime = fatKidBoss.bossScriptableObject.ultCooldown;
                    fatKidBoss.BossStateTransition(new FatKid_BossUlt2State(fatKidBoss));
                }
            }
            if(fatKidBoss.bossHP.currentBossHP <= 0)
            {
                fatKidBoss.BossStateTransition(new FatKid_BossDieState(fatKidBoss));
            }
        }
    }
    public override void FixedUpdate()
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
    public override void Exit()
    {
        
    }
}
