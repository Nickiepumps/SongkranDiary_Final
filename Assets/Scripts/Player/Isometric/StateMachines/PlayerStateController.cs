using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStateController : PlayerSubject
{
    [Header("Control Keymap")]
    public KeyMapSO keymapSO;

    public Rigidbody2D playerRB;
    public Vector2 playerVelocity;
    private PlayerState currentState;
    public float walkSpeed = 5;
    public bool playerTalking = false;
    public Collider2D currentColHit;
    public Animator ISOAnimator;
    public SpriteRenderer ISOPlayerSpriteRenderer;
    private void Start()
    {
        PlayerStateTransition(new PlayerIdleState(this));
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        currentState.Update();
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnEnterTrigger(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.OnExitTrigger(collision);
    }
    public void PlayerStateTransition(PlayerState newState)
    {
        currentState = newState;
        currentState.Start();
    }
    public int CheckHorizontalInput()
    {
        if (Input.GetKey(keymapSO.right))
        {
            return 1;
        }
        else if (Input.GetKeyUp(keymapSO.right))
        {
            return 0;
        }
        else if (Input.GetKey(keymapSO.left))
        {
            return -1;
        }
        else if (Input.GetKeyUp(keymapSO.left))
        {
            return 0;
        }
        else
        {
            return 0;
        }
    }
    public int CheckVerticalInput()
    {
        if (Input.GetKey(keymapSO.top))
        {
            return 1;
        }
        else if (Input.GetKeyUp(keymapSO.top))
        {
            return 0;
        }
        else if (Input.GetKey(keymapSO.bottom))
        {
            return -1;
        }
        else if (Input.GetKeyUp(keymapSO.bottom))
        {
            return 0;
        }
        else
        {
            return 0;
        }
    }
}
