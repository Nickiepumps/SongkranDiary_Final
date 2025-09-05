using UnityEngine;
public abstract class PlayerSideScrollStateMachine
{
    protected PlayerSideScrollStateController playerSideScroll;
    public PlayerSideScrollStateMachine(PlayerSideScrollStateController playerSideScroll)
    {
        this.playerSideScroll = playerSideScroll;
    }
    public abstract void Start();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void OntriggerEnter(Collider2D pCollider);
    public abstract void OntriggerExit(Collider2D pCollider);
    public abstract void OnColliderEnter(Collision2D pCollider);
    public abstract void OnColliderStay(Collision2D pCollider);
    public abstract void OnColliderExit(Collision2D pCollider);
    public abstract void Exit();
}
