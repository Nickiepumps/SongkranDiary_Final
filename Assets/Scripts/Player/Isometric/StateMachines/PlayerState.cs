using UnityEngine;
public abstract class PlayerState
{
    protected PlayerStateController player;
    public PlayerState(PlayerStateController player)
    {
        this.player = player;
    }
    public abstract void Start();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void OnEnterTrigger(Collider2D pCollision);
    public abstract void OnExitTrigger(Collider2D pCollision);
    public abstract void Exit();
}
