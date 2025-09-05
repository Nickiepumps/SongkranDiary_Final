using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatKid_BossObserverController : MonoBehaviour, IBossObserver, IGameObserver
{
    [Header("Observer References")]
    private BossSubject bossSubject;
    private FatKid_BossStateController fatKidStateController;
    [SerializeField] private GameSubject gameUISubject;
    [SerializeField] private GameSubject sideScrollGameSubject;
    [SerializeField] private GameSubject sideScrollIntroGameSubject;

    [Header("Boss BulletPooler Reference")]
    [SerializeField] private EnemyBulletPooler enemyBulletPooler;
    [Header("Bullet Spawn Points")]
    [SerializeField] private Transform bulletLowerSpawn;
    [SerializeField] private Transform bulletMiddleSpawn;
    [SerializeField] private Transform bulletTopSpawn;

    private void Awake()
    {
        bossSubject = GetComponent<FatKid_BossStateController>();
        fatKidStateController = GetComponent<FatKid_BossStateController>();
    }
    private void OnEnable()
    {
        bossSubject.AddBossObserver(this);
        gameUISubject.AddGameObserver(this);
        gameUISubject.AddSideScrollGameObserver(this);
        sideScrollGameSubject.AddSideScrollGameObserver(this);
        sideScrollIntroGameSubject.AddSideScrollGameObserver(this);
    }
    private void OnDisable()
    {
        bossSubject.RemoveBossObserver(this);
        gameUISubject.RemoveGameObserver(this);
        gameUISubject.RemoveSideScrollGameObserver(this);
        sideScrollGameSubject.RemoveSideScrollGameObserver(this);
        sideScrollIntroGameSubject.AddSideScrollGameObserver(this);
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case(BossAction.Idle):
                return;
            case (BossAction.Shoot):
                bossSubject.GetComponent<FatKid_BossStateController>().bossShooting = true;
                if (RandomPattern() <= 5)
                {
                    StartCoroutine(ShootPattern(1));
                }
                else
                {
                    StartCoroutine(ShootPattern(2));
                }
                return;
            case (BossAction.Jump):
                bossSubject.GetComponent<FatKid_BossStateController>().bossRB.AddForce(Vector2.up * 28, ForceMode2D.Impulse);
                return;
            case (BossAction.Ult):
                return;
            case (BossAction.Damaged):
                return;
            case (BossAction.Die):
                gameUISubject.NotifySideScrollGameObserver(SideScrollGameState.WinBoss);
                return;
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        
    }

    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
        switch (sidescrollGameState)
        {
            case (SideScrollGameState.Play):
                fatKidStateController.isGameStart = true;
                return;
            case (SideScrollGameState.Paused):
                fatKidStateController.isGameStart = false;
                return;
            case (SideScrollGameState.StartRound):
                fatKidStateController.isGameStart = true;
                return;
        }
    }
    private int RandomPattern()
    {
        int shootValue = Random.Range(0, 10);
        return shootValue;
    }
    private IEnumerator ShootPattern(int patternValue)
    {
        fatKidStateController.bossAnimator.SetBool("isShoot", true);
        fatKidStateController.bossAnimator.SetBool("isIdle", false);
        fatKidStateController.bossAnimator.SetBool("isAim", false);
        if (patternValue == 1)
        {
            Debug.Log("Boss Shoot Pattern 1");
            BossShoot(bulletLowerSpawn, Vector2.left);

        }
        else
        {
            Debug.Log("Boss Shoot Pattern 2");
            BossShoot(bulletMiddleSpawn, Vector2.left);
        }
        yield return new WaitForSeconds(0.5f);
        fatKidStateController.bossAnimator.SetBool("isShoot", false);
        fatKidStateController.bossAnimator.SetBool("isIdle", false);
        fatKidStateController.bossAnimator.SetBool("isAim", true);
        bossSubject.GetComponent<FatKid_BossStateController>().bossShooting = false;
    }
    private void BossShoot(Transform spawnPos, Vector2 bulletDirection)
    {
        GameObject enemyBullet = enemyBulletPooler.EnableEnemyBullet();
        if (enemyBullet != null)
        {
            enemyBullet.transform.position = spawnPos.position;
            enemyBullet.transform.rotation = spawnPos.rotation;
            enemyBullet.GetComponent<EnemyBullet>().bulletDirection = bulletDirection;
            enemyBullet.SetActive(true);
        }
    }
}
