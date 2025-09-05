using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserverController : MonoBehaviour, IGameObserver, IPlayerObserver, IShootingObserver
{
    [SerializeField] private GameType gameType;

    [Header("GameMode Type")]
    [SerializeField] private bool isISO; // Isometric mode
    [SerializeField] private bool isSideScroll; // Side-Scroll mode

    [Header("Player's StateMachine (Isometric level)")]
    [SerializeField] private PlayerStateController playerStateController;

    [Header("Player's StateMachine (SideScroll level)")]
    [SerializeField] private PlayerSideScrollStateController playerSideScrollStateController;

    [Header("Player Observer Subject")]
    [SerializeField] private PlayerSubject playerSubject;

    [Header("Game Observer Subject")]
    [SerializeField] private GameSubject gameUISubject;
    [SerializeField] private GameSubject sidescrollGameSubject;
    [SerializeField] private GameSubject sidescrollIntroWindowSubject;

    [Header("RunNGun Goal Observer Subject")]
    [SerializeField] private GameSubject goalSubject;

    [Header("Shooting Observer Subject")]
    [SerializeField] private ShootingSubject shootingSubject;

    [Space]
    [Space]
    [Header("Shooting Related References")]
    [Header("BulletShooting Reference")]
    [SerializeField] private BulletShooting bulletShooting;

    [Header("Bullet Pooler Referecne")]
    [SerializeField] private BulletPooler bulletPooler;

    [Space]
    [Space]
    [Header("UI Related References")]
    [SerializeField] private PlayerHealthDisplay healthDisplay;
    private void OnEnable()
    {
        gameUISubject = GameObject.Find("GameUIController").GetComponent<GameSubject>();
        gameUISubject.AddGameObserver(this);
        gameUISubject.AddSideScrollGameObserver(this);
        playerSubject.AddPlayerObserver(this);
        if (isSideScroll == true)
        {
            shootingSubject.AddShootingObserver(this);
            sidescrollGameSubject.AddSideScrollGameObserver(this);
            sidescrollIntroWindowSubject.AddSideScrollGameObserver(this); // Find a way to use this line inside GameUIControllerScript
        }
        if (gameType == GameType.RunNGun)
        {
            goalSubject.AddSideScrollGameObserver(this);
        }
    }
    private void OnDisable()
    {
        gameUISubject.RemoveGameObserver(this);
        gameUISubject.RemoveSideScrollGameObserver(this);
        playerSubject.RemovePlayerObserver(this);
        if (isSideScroll == true)
        {
            shootingSubject.RemoveShootingObserver(this);
            sidescrollGameSubject.RemoveSideScrollGameObserver(this);
            sidescrollIntroWindowSubject.RemoveSideScrollGameObserver(this); // Find a way to use this line inside GameUIControllerScript
        }
        if (gameType == GameType.RunNGun)
        {
            goalSubject.RemoveSideScrollGameObserver(this);
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        switch (isoGameState)
        {
            case (IsometricGameState.Play):
                Debug.Log("Transition from interact to idle");
                playerStateController.enabled = true;
                playerStateController.PlayerStateTransition(new PlayerIdleState(playerStateController));
                return;
            case (IsometricGameState.Paused):
                playerStateController.PlayerStateTransition(new PlayerIdleState(playerStateController));
                playerStateController.enabled = false;
                return;
        }
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
        switch (sidescrollGameState)
        {
            case (SideScrollGameState.Play):
                return;
            case (SideScrollGameState.StartRound):
                playerSideScrollStateController.isGameStart = true;
                return;
            case(SideScrollGameState.WinRunNGun):
                playerSideScrollStateController.enabled = false;
                playerSideScrollStateController.isWinRunNGun = true;
                return;
            case (SideScrollGameState.WinBoss):
                playerSideScrollStateController.enabled = false;
                playerSideScrollStateController.gameObject.GetComponent<BulletShooting>().enabled = false;
                return;
            case (SideScrollGameState.Paused):
                return;
        }
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        if (isISO == true)
        {
            switch (playerAction)
            {
                case (PlayerAction.Idle):
                    return;
            }
        }
        else
        {
            switch (playerAction)
            {
                case (PlayerAction.Side_Idle):
                    playerSideScrollStateController.isCrouch = false;
                    break;
                case (PlayerAction.Run):
                    playerSideScrollStateController.isCrouch = false;
                    break;
                case (PlayerAction.Crouch):
                    playerSideScrollStateController.isCrouch = true;
                    break;
                case (PlayerAction.Jump):
                    playerSideScrollStateController.isCrouch = false;
                    break;
                case (PlayerAction.Damaged):
                    if (playerSideScrollStateController.playerCurrentHP > 0)
                    {
                        playerSideScrollStateController.playerStatusAudioSource.clip = playerSideScrollStateController.playerAudioClipArr[1];
                        playerSideScrollStateController.playerStatusAudioSource.Play();
                        playerSideScrollStateController.playerCurrentHP--;
                        healthDisplay.DecreaseHealth(playerSideScrollStateController.playerCurrentHP);
                        playerSideScrollStateController.isDamaged = true;
                    }
                    return;
                case (PlayerAction.Heal):
                    playerSideScrollStateController.playerStatusAudioSource.clip = playerSideScrollStateController.playerAudioClipArr[2];
                    playerSideScrollStateController.playerStatusAudioSource.Play();
                    if (playerSideScrollStateController.playerCurrentHP < playerSideScrollStateController.playerMaxHP)
                    {
                        playerSideScrollStateController.playerCurrentHP++;
                        healthDisplay.IncreaseHealth(playerSideScrollStateController.playerCurrentHP);
                    }
                    return;
                case (PlayerAction.Blind):
                    if (playerSideScrollStateController.playerCurrentHP > 0)
                    {
                        playerSideScrollStateController.playerCurrentHP--;
                        healthDisplay.DecreaseHealth(playerSideScrollStateController.playerCurrentHP);
                        playerSideScrollStateController.isDamaged = true;
                    }
                    return;
                case (PlayerAction.Dead):
                    healthDisplay.DecreaseHealth(playerSideScrollStateController.playerCurrentHP);
                    gameUISubject.NotifySideScrollGameObserver(SideScrollGameState.Lose);
                    return;
                case (PlayerAction.win):
                    return;
            }
        }
    }
    public void OnShootingNotify(ShootingAction shootingAction)
    {
        switch (shootingAction)
        {
            case (ShootingAction.aimright):
                playerSubject.GetComponentInChildren<SpriteRenderer>().flipX = false;
                return;
            case (ShootingAction.aim45topright):
                playerSubject.GetComponentInChildren<SpriteRenderer>().flipX = false;
                return;
            case (ShootingAction.aimleft):
                playerSubject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                return;
            case (ShootingAction.aim45topleft):
                playerSubject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                return;
        }
    }
}
