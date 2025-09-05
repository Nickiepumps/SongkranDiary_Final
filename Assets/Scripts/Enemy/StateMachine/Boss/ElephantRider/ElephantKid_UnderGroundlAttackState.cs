using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantKid_UnderGroundlAttackState : BossStateMachine
{
    public ElephantKid_UnderGroundlAttackState(ElephantKid_BossStateController elephantKidBoss) : base(elephantKidBoss) { }
    int patternToUse;
    public override void Start()
    {
        elephantKidBoss.StartCoroutine(elephantKidBoss.Boss_ElephantKidUndergroundAtk_Intro());
        /*elephantKidBoss.bossAnimator.SetBool("isPrepareToAttack", false);
        elephantKidBoss.bossAnimator.SetBool("isIdle", true);
        elephantKidBoss.bossAnimator.SetBool("isAttack", true);
        elephantKidBoss.bossAnimator.SetBool("isShoot", false);
        elephantKidBoss.bossAnimator.SetFloat("attackIdleVariant", 1);

        patternToUse = Random.Range(0, elephantKidBoss.undergroundUltPatternList.PatternList.Count);
        for(int i = 0; i < elephantKidBoss.undergroundUltPatternList.PatternList[patternToUse].incomingBullet.Count; i++)
        {
            Obstacle_FireHydrant component = elephantKidBoss.undergroundUltPatternList.PatternList[patternToUse].incomingBullet[i].GetComponent<Obstacle_FireHydrant>();
            component.waterInitialCooldownTime = elephantKidBoss.patternTimerList.PatternList[patternToUse].patternTimer[i];
            component.enabled = true;
            elephantKidBoss.undergroundUltPatternList.PatternList[patternToUse].incomingBullet[i].gameObject.SetActive(true);
        }*/
    }
    public override void Update()
    {
        Obstacle_FireHydrant component = elephantKidBoss.undergroundUltPatternList.PatternList[elephantKidBoss.undergroundPattern].incomingBullet[elephantKidBoss.undergroundUltPatternList.PatternList[elephantKidBoss.undergroundPattern].incomingBullet.Count - 1].GetComponent<Obstacle_FireHydrant>();
        if (component.isShoot == true)
        {
            elephantKidBoss.StartCoroutine(elephantKidBoss.Boss_ElephantKidUndergroundAtk_Outro());
            elephantKidBoss.BossStateTransition(new ElephantKid_BossIdleState(elephantKidBoss));
        }
        if (elephantKidBoss.bossHP.currentBossHP <= 0)
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
