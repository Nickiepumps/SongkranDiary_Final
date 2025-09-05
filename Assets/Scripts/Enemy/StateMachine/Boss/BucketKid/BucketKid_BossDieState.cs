using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BucketKid_BossDieState : BossStateMachine
{
    public BucketKid_BossDieState(BucketKid_BossStateController bucketKidBoss) : base(bucketKidBoss) { }
    public override void Start()
    {
        bucketKidBoss.isDead = true;
        bucketKidBoss.NotifyBoss(BossAction.Die);
        bucketKidBoss.gameObject.SetActive(false);
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
