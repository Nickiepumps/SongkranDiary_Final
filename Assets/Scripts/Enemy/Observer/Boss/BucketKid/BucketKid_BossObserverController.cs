using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BucketKid_BossObserverController : MonoBehaviour, IBossObserver, IGameObserver
{
    [Header("Observer References")]
    private BossSubject bossSubject;
    private BucketKid_BossStateController bucketKidStateController;
    [SerializeField] private GameSubject gameUISubject;
    [SerializeField] private GameSubject sideScrollGameSubject;
    [SerializeField] private GameSubject sideScrollIntroGameSubject;

    [Header("Player Reference")]
    [SerializeField] private PlayerSideScrollStateController playerSideScrollStateController;

    [Header("Boss BulletPooler Reference")]
    [SerializeField] private EnemyBulletPooler enemyBulletPooler;
    [SerializeField] private GameObject bossBalloonPrefab;
    [SerializeField] private GameObject bossBoomerangPrefab;

    [Header("Boss Ult2 Reference (WaterPipe)")]
    [SerializeField] private Obstacle_WaterPipe_Rotate rotateableWaterPipe;

    [Header("Bullet Spawn Points")]
    [SerializeField] private Transform boomerangLowerSpawn;
    [SerializeField] private Transform boomerangLowerDestination;
    [SerializeField] private Transform boomerangTopSpawn;
    [SerializeField] private Transform boomerangTopDestination;
    [SerializeField] private Transform balloonSpawn;
    [SerializeField] private Transform bulletRainMinSpawnPos;
    [SerializeField] private Transform bulletRainMaxSpawnPos;
    private void Awake()
    {
        bossSubject = GetComponent<BucketKid_BossStateController>();
        bucketKidStateController = GetComponent<BucketKid_BossStateController>();
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
        sideScrollIntroGameSubject.RemoveSideScrollGameObserver(this);
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case (BossAction.Idle):
                rotateableWaterPipe.enabled = false;
                return;
            case (BossAction.Shoot):
                if(bucketKidStateController.normalAttackPattern == 1)
                {
                    StartCoroutine(BossThrowBalloon());
                }
                else
                {
                    int pattern = RandomPattern();
                    StartCoroutine(BoomerangPattern(pattern));
                }
                return;
            case (BossAction.Jump):
                return;
            case (BossAction.Ult):
                if(bucketKidStateController.isBossGoOutFromBarrel == false)
                {
                    StartBulletRain(bulletRainMinSpawnPos, bulletRainMaxSpawnPos);
                }
                else
                {
                    rotateableWaterPipe.enabled = true;
                }
                return;
            case(BossAction.Damaged):
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
            case(SideScrollGameState.Play):
                bucketKidStateController.isGameStart = true;
                return;
            case (SideScrollGameState.StartRound):
                bucketKidStateController.isGameStart = true;
                return;
            case (SideScrollGameState.Paused):
                bucketKidStateController.isGameStart = false;
                return;
        }
    }
    private int RandomPattern()
    {
        int shootValue = Random.Range(0, 2);
        return shootValue;
    }
    private IEnumerator BoomerangPattern(int patternValue)
    {
        bossSubject.GetComponent<BucketKid_BossStateController>().isBossThrowingBoomerang = true;
        if (patternValue == 1)
        {
            Debug.Log("Boss Shoot Pattern 1");
            bucketKidStateController.bossAnimator.SetBool("isIdle", false);
            bucketKidStateController.bossAnimator.SetBool("isAttack", true);
            bucketKidStateController.bossAnimator.SetBool("isThrowingBoomerang", true);
            bucketKidStateController.bossAnimator.SetFloat("attackVariant", 0);
            yield return new WaitForSeconds(1.3f);
            BossThrowBoomerang(boomerangLowerSpawn, boomerangLowerDestination, Vector2.left);

        }
        else
        {
            Debug.Log("Boss Shoot Pattern 2");
            bucketKidStateController.bossAnimator.SetBool("isIdle", false);
            bucketKidStateController.bossAnimator.SetBool("isAttack", true);
            bucketKidStateController.bossAnimator.SetBool("isThrowingBoomerang", true);
            bucketKidStateController.bossAnimator.SetFloat("attackVariant", 1);
            yield return new WaitForSeconds(1.3f);
            BossThrowBoomerang(boomerangTopSpawn, boomerangTopDestination, Vector2.left);
        }
        bucketKidStateController.bossAnimator.SetBool("isIdle", true);
        bucketKidStateController.bossAnimator.SetBool("isAttack", true);
        bucketKidStateController.bossAnimator.SetBool("isThrowingBoomerang", false);
        bucketKidStateController.bossAnimator.SetFloat("attackVariant", 0);
        bossSubject.GetComponent<BucketKid_BossStateController>().isBossThrowingBoomerang = false;
    }
    private void BossThrowBoomerang(Transform spawnPos, Transform destination, Vector2 bulletDirection)
    {
        GameObject enemyBullet = enemyBulletPooler.EnableEnemyBullet();
        if (enemyBullet != null)
        {
            enemyBullet.transform.localRotation = spawnPos.rotation;
            enemyBullet.transform.position = spawnPos.position;
            //enemyBullet.GetComponent<EnemyBullet>().bulletSpriteRenderer.sprite = bossBoomerangPrefab.GetComponent<EnemyBullet>().bulletSpriteRenderer.sprite;
            //enemyBullet.GetComponent<EnemyBullet>().bulletAnimator = bossBoomerangPrefab.GetComponent<EnemyBullet>().bulletAnimator;
            enemyBullet.GetComponent<EnemyBullet>().bulletSpriteRenderer.gameObject.transform.localPosition = Vector3.zero;
            //enemyBullet.GetComponent<EnemyBullet>().bulletSpriteRenderer.gameObject.transform.localScale = bossBoomerangPrefab.GetComponent<EnemyBullet>().bulletSpriteRenderer.gameObject.transform.localScale;
            enemyBullet.GetComponent<EnemyBullet>().damageCollider = bossBoomerangPrefab.GetComponent<EnemyBullet>().damageCollider;
            // To Do: Find a way to copy any collider properties, Why do this
            enemyBullet.transform.GetChild(1).GetComponent<CircleCollider2D>().offset = bossBoomerangPrefab.GetComponent<EnemyBullet>().damageCollider.GetComponent<CircleCollider2D>().offset;
            enemyBullet.transform.GetChild(1).GetComponent<CircleCollider2D>().radius = bossBoomerangPrefab.GetComponent<EnemyBullet>().damageCollider.GetComponent<CircleCollider2D>().radius;
            enemyBullet.transform.GetChild(2).GetComponent<CircleCollider2D>().offset = bossBoomerangPrefab.GetComponent<EnemyBullet>().damageCollider.GetComponent<CircleCollider2D>().offset;
            enemyBullet.transform.GetChild(2).GetComponent<CircleCollider2D>().radius = bossBoomerangPrefab.GetComponent<EnemyBullet>().damageCollider.GetComponent<CircleCollider2D>().radius;

            enemyBullet.GetComponent<EnemyBullet>().healCollider = bossBoomerangPrefab.GetComponent<EnemyBullet>().healCollider;
            enemyBullet.GetComponent<EnemyBullet>().bulletType = bossBoomerangPrefab.GetComponent<EnemyBullet>().bulletType;
            enemyBullet.GetComponent<EnemyBullet>().travelSpeed = bossBoomerangPrefab.GetComponent<EnemyBullet>().travelSpeed;
            enemyBullet.GetComponent<EnemyBullet>().bulletDirection = bulletDirection;
            enemyBullet.GetComponent<EnemyBullet>().curerntSpawnPos = spawnPos;
            enemyBullet.GetComponent<EnemyBullet>().spawnPos = new Transform[] { boomerangTopSpawn, boomerangLowerSpawn };
            enemyBullet.SetActive(true);
        }
    }
    private IEnumerator BossThrowBalloon()
    {
        bucketKidStateController.bossAnimator.SetBool("isIdle", false);
        bucketKidStateController.bossAnimator.SetBool("isAttack", true);
        bucketKidStateController.bossAnimator.SetBool("isThrowingBalloon", true);
        bucketKidStateController.bossAnimator.SetFloat("attackVariant", 0);
        bucketKidStateController.isBossThrowingBalloon = true;
        yield return new WaitForSeconds(1f);
        BossThrowBalloonSpawner(balloonSpawn);
        bucketKidStateController.bossAnimator.SetBool("isIdle", true);
        bucketKidStateController.bossAnimator.SetBool("isAttack", true);
        bucketKidStateController.bossAnimator.SetBool("isThrowingBalloon", false);
        bucketKidStateController.bossAnimator.SetFloat("attackVariant", 0);
        bucketKidStateController.isBossThrowingBalloon = false;
    }
    private void BossThrowBalloonSpawner(Transform spawnPos)
    {
        GameObject enemyBullet = enemyBulletPooler.EnableEnemyBullet();
        if (enemyBullet != null)
        {
            Vector3 lookDirection = playerSideScrollStateController.transform.position - spawnPos.position;
            float rotAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
            spawnPos.transform.localRotation = Quaternion.Euler(0, 0, rotAngle);
            enemyBullet.GetComponent<EnemyBullet>().bulletType = bossBalloonPrefab.GetComponent<EnemyBullet>().bulletType;
            //enemyBullet.GetComponent<EnemyBullet>().bulletAnimator = bossBalloonPrefab.GetComponent<EnemyBullet>().bulletAnimator;
            enemyBullet.GetComponent<EnemyBullet>().bulletSpriteRenderer.gameObject.transform.localPosition = new Vector3(0, 0.8f, 0);
            enemyBullet.GetComponent<EnemyBullet>().bulletSpriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //enemyBullet.GetComponent<EnemyBullet>().bulletSpriteRenderer.gameObject.transform.localScale = bossBalloonPrefab.GetComponent<EnemyBullet>().bulletSpriteRenderer.gameObject.transform.localScale;
            //enemyBullet.GetComponent<EnemyBullet>().bulletSpriteRenderer.sprite = bossBalloonPrefab.GetComponent<EnemyBullet>().bulletSpriteRenderer.sprite;
            enemyBullet.GetComponent<EnemyBullet>().damageCollider = bossBalloonPrefab.GetComponent<EnemyBullet>().damageCollider;
            enemyBullet.GetComponent<EnemyBullet>().healCollider = bossBalloonPrefab.GetComponent<EnemyBullet>().healCollider;
            // To Do: Find a way to copy any collider properties, Why do this
            enemyBullet.transform.GetChild(1).GetComponent<CircleCollider2D>().offset = bossBalloonPrefab.GetComponent<EnemyBullet>().damageCollider.GetComponent<CircleCollider2D>().offset;
            enemyBullet.transform.GetChild(1).GetComponent<CircleCollider2D>().radius = bossBalloonPrefab.GetComponent<EnemyBullet>().damageCollider.GetComponent<CircleCollider2D>().radius;
            enemyBullet.transform.GetChild(2).GetComponent<CircleCollider2D>().offset = bossBalloonPrefab.GetComponent<EnemyBullet>().damageCollider.GetComponent<CircleCollider2D>().offset;
            enemyBullet.transform.GetChild(2).GetComponent<CircleCollider2D>().radius = bossBalloonPrefab.GetComponent<EnemyBullet>().damageCollider.GetComponent<CircleCollider2D>().radius;

            enemyBullet.GetComponent<EnemyBullet>().travelSpeed = bossBalloonPrefab.GetComponent<EnemyBullet>().travelSpeed;
            enemyBullet.GetComponent<EnemyBullet>().bulletDirection = lookDirection;
            enemyBullet.transform.position = spawnPos.position;
            enemyBullet.transform.rotation = spawnPos.rotation;
            enemyBullet.SetActive(true);
        }
    }
    private void StartBulletRain(Transform minSpawnPos, Transform maxSpawnPos)
    {
        GameObject rainBullet = enemyBulletPooler.EnableIncomingBullet();
        if(rainBullet != null)
        {
            Vector3 camPos = Camera.main.WorldToViewportPoint(Camera.main.transform.position);
            rainBullet.transform.position = new Vector2(Random.Range(minSpawnPos.position.x, maxSpawnPos.position.x), minSpawnPos.position.y);
            rainBullet.transform.localRotation = Quaternion.Euler(Vector3.zero);
            rainBullet.GetComponent<EnemyBullet>().bulletDirection = Vector2.down;
            rainBullet.GetComponent<EnemyBullet>().travelSpeed = 4.5f;
            rainBullet.SetActive(true);
        }
    }
}
