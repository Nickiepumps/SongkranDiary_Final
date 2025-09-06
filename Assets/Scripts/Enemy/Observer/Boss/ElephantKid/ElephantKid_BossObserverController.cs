using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantKid_BossObserverController : MonoBehaviour, IBossObserver, IGameObserver
{
    [Header("Observer References")]
    private BossSubject bossSubject;
    private ElephantKid_BossStateController elephantKidStateController;
    [SerializeField] private GameSubject gameUISubject;
    [SerializeField] private GameSubject sideScrollGameSubject;
    [SerializeField] private GameSubject sideScrollIntroGameSubject;

    [Header("Boss Animator Component")]
    [SerializeField] private Animator elephantKidBossAnimator;

    [Header("Player Reference")]
    [SerializeField] private PlayerSideScrollStateController playerSideScrollStateController;

    [Header("Boss BulletPooler Reference")]
    [SerializeField] private EnemyBulletPooler enemyBulletPooler;

    [Header("Kid bullet spawner")]
    [SerializeField] private Transform kidBulletSpawner;

    [Header("Baby elephant spawner and destination")]
    [SerializeField] private Transform[] babyElephantSpawnerArr = new Transform[2]; 
    [SerializeField] private Transform[] babyElephantDestinationArr = new Transform[2]; 
    private void Awake()
    {
        bossSubject = GetComponent<ElephantKid_BossStateController>();
        elephantKidStateController = GetComponent<ElephantKid_BossStateController>();
    }
    private void OnEnable()
    {
        bossSubject.AddBossObserver(this);
        gameUISubject.AddGameObserver(this);
        sideScrollGameSubject.AddSideScrollGameObserver(this);
        sideScrollIntroGameSubject.AddSideScrollGameObserver(this);
    }
    private void OnDisable()
    {
        bossSubject.RemoveBossObserver(this);
        gameUISubject.RemoveGameObserver(this);
        sideScrollGameSubject.RemoveSideScrollGameObserver(this);
        sideScrollIntroGameSubject.RemoveSideScrollGameObserver(this);
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case (BossAction.Idle):
                return;
            case (BossAction.Shoot):
                StartCoroutine(Boss_KidShoot());
                return;
            case (BossAction.Ult):
                Boss_BabyElephantSpawn();
                return;
            case (BossAction.Ult2):
                return;
            case(BossAction.Damaged):
                return;
            case(BossAction.Heal):
                return;
            case (BossAction.Die):
                return;
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState) { }

    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
        switch (sidescrollGameState)
        {
            case(SideScrollGameState.StartRound):
                elephantKidStateController.isGameStart = true;
                return;
            case (SideScrollGameState.Play):
                elephantKidStateController.isGameStart = true;
                return;
            case(SideScrollGameState.Paused):
                elephantKidStateController.isGameStart = false;
                return;
        }
    }
    private IEnumerator Boss_KidShoot()
    {
        elephantKidStateController.isShoot = true;
        elephantKidBossAnimator.SetBool("isPrepareToAttack", false);
        elephantKidBossAnimator.SetBool("isIdle", false);
        elephantKidBossAnimator.SetBool("isAttack", true);
        elephantKidBossAnimator.SetBool("isShoot", true);
        elephantKidBossAnimator.SetFloat("attackIdleVariant", 0);
        elephantKidBossAnimator.SetFloat("prepareVariant", 0);
        yield return new WaitForSeconds(0.4f);
        Boss_KidShootBulletSpawner(kidBulletSpawner);
        yield return new WaitForSeconds(0.3f);
        elephantKidBossAnimator.SetBool("isPrepareToAttack", false);
        elephantKidBossAnimator.SetBool("isIdle", true);
        elephantKidBossAnimator.SetBool("isAttack", true);
        elephantKidBossAnimator.SetBool("isShoot", false);
        elephantKidBossAnimator.SetFloat("attackIdleVariant", 0);
        elephantKidBossAnimator.SetFloat("prepareVariant", 0);
        elephantKidStateController.isShoot = false;
    }
    private void Boss_KidShootBulletSpawner(Transform spawnPos)
    {
        GameObject enemyBullet = enemyBulletPooler.EnableEnemyBullet();
        if (enemyBullet != null)
        {
            Vector3 lookDirection = playerSideScrollStateController.transform.position - spawnPos.position;
            float rotAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
            rotAngle = Random.Range(rotAngle - 20, rotAngle + 21);
            spawnPos.transform.localRotation = Quaternion.Euler(0, 0, rotAngle);
            //enemyBullet.GetComponent<EnemyBullet>().bulletDirection = lookDirection;
            enemyBullet.GetComponent<EnemyBullet>().bulletDirection = spawnPos.transform.localRotation * Vector2.up;
            enemyBullet.transform.position = spawnPos.position;
            enemyBullet.transform.rotation = spawnPos.rotation;
            enemyBullet.SetActive(true);
        }
    }
    private void Boss_BabyElephantSpawn()
    {
        GameObject enemyBullet = enemyBulletPooler.EnableObstacleBullet();
        if (enemyBullet != null)
        {
            if(elephantKidStateController.bossSpriteRenderer.flipX == false)
            {
                enemyBullet.GetComponent<Obstacle_BabyElephant>().destination = babyElephantDestinationArr[0];
                enemyBullet.transform.position = babyElephantSpawnerArr[1].position;
            }
            else
            {
                enemyBullet.GetComponent<Obstacle_BabyElephant>().destination = babyElephantDestinationArr[1];
                enemyBullet.transform.position = babyElephantSpawnerArr[0].position;
            }

            //enemyBullet.transform.rotation = spawnPos.rotation;
            enemyBullet.SetActive(true);
        }
    }
}
