using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElephantKid_BossUlt2State : BossStateMachine
{
    public ElephantKid_BossUlt2State(ElephantKid_BossStateController elephantKidBoss) : base(elephantKidBoss) { }
    private int shotCount = 0;
    private float aspd = 0.3f;
    public override void Start()
    {
        elephantKidBoss.NotifyBoss(BossAction.Ult2);
        if(elephantKidBoss.bossSpriteRenderer == true)
        {
            elephantKidBoss.vacuumTrigger.GetComponent<SideScroll_VacuumTrigger>().pullDirection = Vector2.right;
        }
        else
        {
            elephantKidBoss.vacuumTrigger.GetComponent<SideScroll_VacuumTrigger>().pullDirection = Vector2.left;
        }
        elephantKidBoss.StartCoroutine(elephantKidBoss.StartBossVacuumAnimation());
    }
    public override void Update()
    {
        aspd -= Time.deltaTime;
        if(aspd <= 0)
        {
            elephantKidBoss.NotifyBoss(BossAction.Shoot);
            shotCount++;
            aspd = 0.3f;
        }
        if(shotCount >= 3)
        {
            aspd = 1f;
            shotCount = 0;
        }
    }
    public override void FixedUpdate(){}
    public override void OnTriggerEnter(Collider2D eCollider){}
    public override void OnTriggerExit(Collider2D eCollider){}
    public override void OnColliderEnter(Collision2D pCollider){}
    public override void OnColliderExit(Collision2D pCollider){}
    public override void Exit(){}
}
