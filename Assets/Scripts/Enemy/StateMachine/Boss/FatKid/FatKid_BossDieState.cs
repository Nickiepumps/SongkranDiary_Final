using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class FatKid_BossDieState : BossStateMachine
{
    public FatKid_BossDieState(FatKid_BossStateController fatKidBoss) : base(fatKidBoss) { }

    public override void Start()
    {
        fatKidBoss.isDead = true;
        /*fatKidBoss.enemyAudioSource.clip = fatKidBoss.enemyAudioClipArr[1];
        fatKidBoss.enemyAudioSource.Play();*/
        fatKidBoss.NotifyBoss(BossAction.Die);
        fatKidBoss.gameObject.SetActive(false);
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
