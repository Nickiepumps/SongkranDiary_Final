using UnityEngine;

public abstract class BossStateMachine
{
    protected FatKid_BossStateController fatKidBoss;
    protected BucketKid_BossStateController bucketKidBoss;
    protected ElephantKid_BossStateController elephantKidBoss;
    public BossStateMachine(FatKid_BossStateController fatKidBoss)
    {
        this.fatKidBoss = fatKidBoss;
    }
    public BossStateMachine(BucketKid_BossStateController bucketKidBoss)
    {
        this.bucketKidBoss = bucketKidBoss;
    }
    public BossStateMachine(ElephantKid_BossStateController elephantKidBoss)
    {
        this.elephantKidBoss = elephantKidBoss;
    }
    public abstract void Start();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void OnTriggerEnter(Collider2D eCollider);
    public abstract void OnTriggerExit(Collider2D eCollider);
    public abstract void OnColliderEnter(Collision2D pCollider);
    public abstract void OnColliderExit(Collision2D pCollider);
    public abstract void Exit();
}
