using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElephantKid_BossIdleState : BossStateMachine
{
    public ElephantKid_BossIdleState(ElephantKid_BossStateController elephantKidBoss) : base (elephantKidBoss){}
    private float currentIdleTime;
    public override void Start()
    {
        elephantKidBoss.bossAnimator.SetBool("isIdle", true);
        elephantKidBoss.bossAnimator.SetBool("isAttack", false);
        elephantKidBoss.bossAnimator.SetBool("isUlt", false);
        elephantKidBoss.bossAnimator.SetBool("isPrepareToAttack", false);
        elephantKidBoss.bossAnimator.SetBool("isShoot", false);
        elephantKidBoss.bossAnimator.SetBool("isHeal", false);
        elephantKidBoss.bossAnimator.SetFloat("prepareVariant", 0);
        elephantKidBoss.bossAnimator.SetFloat("attackIdleVariant", 0);
        elephantKidBoss.bossAnimator.SetFloat("ultVariant", 0);

        elephantKidBoss.isNormalIdle = true;
        currentIdleTime = elephantKidBoss.bossScriptableObject.idleTime;
    }
    public override void Update()
    {
        if(elephantKidBoss.isGameStart == true)
        {
            currentIdleTime -= Time.deltaTime;
        }
        
        if (currentIdleTime <= 0)
        {
            elephantKidBoss.isNormalIdle = false;
            if (elephantKidBoss.normalAttackCount >= 3)
            {
                elephantKidBoss.normalAttackCount = 0;
                int attackType = Random.Range(0, 3);
                if(attackType == 0)
                {
                    elephantKidBoss.StartCoroutine(elephantKidBoss.Boss_ElephantKidNormalAtk_Outro(new ElephantKid_UnderGroundlAttackState(elephantKidBoss)));
                    currentIdleTime = elephantKidBoss.bossScriptableObject.idleTime;
                }
                else if(attackType == 1)
                {
                    elephantKidBoss.StartCoroutine(elephantKidBoss.Boss_ElephantKidNormalAtk_Outro(new ElephantKid_BossUlt1State(elephantKidBoss)));
                    currentIdleTime = elephantKidBoss.bossScriptableObject.idleTime;
                }
                else
                {
                    elephantKidBoss.StartCoroutine(elephantKidBoss.Boss_ElephantKidNormalAtk_Outro(new ElephantKid_BossUlt2State(elephantKidBoss)));
                    currentIdleTime = elephantKidBoss.bossScriptableObject.idleTime;
                }
            }
            else
            {
                elephantKidBoss.normalAttackCount++;
                elephantKidBoss.BossStateTransition(new ElephantKid_NormalAttack1State(elephantKidBoss));
                currentIdleTime = elephantKidBoss.bossScriptableObject.idleTime;
            }
        }
        if (elephantKidBoss.bossHP.currentBossHP <= elephantKidBoss.bossHP.bossMaxHP - ((elephantKidBoss.bossHP.bossMaxHP * 25) / 100) * elephantKidBoss.healCount
            && elephantKidBoss.healCount < 4)
        {
            elephantKidBoss.healCount++;
            elephantKidBoss.StartCoroutine(elephantKidBoss.StartBossHealAnimation());
        }
        if(elephantKidBoss.bossHP.currentBossHP <= 0)
        {
            elephantKidBoss.BossStateTransition(new ElephantKid_BossDieState(elephantKidBoss));
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
