using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterEnemyObserverController : MonoBehaviour, INormalEnemyObserver
{
    private NormalEnemySubject normalEnemySubject;
    private EnemyBulletPooler enemyBulletPooler;
    private EnemyShooterStateController enemyStats;

    [Header("Bullet Spawner")]
    [SerializeField] private Transform bulletRightSpawn;
    [SerializeField] private Transform bulletLeftSpawn;

    [Header("Enemy Sprite")]
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private Color enemyDamagedColor;
    private void Awake()
    {
        normalEnemySubject = GetComponent<EnemyShooterStateController>();
        enemyStats = GetComponent<EnemyShooterStateController>();
    }
    private void Start()
    {
        enemyBulletPooler = GameObject.Find("EnemyBulletPooler").GetComponent<EnemyBulletPooler>();
    }
    private void OnEnable()
    {
        normalEnemySubject.AddNormalEnemyObserver(this);
    }
    private void OnDisable()
    {
        normalEnemySubject.RemoveNormalEnemyObserver(this);
    }
    public void OnNormalEnemyNotify(EnemyAction action)
    {
        switch (action)
        {
            case(EnemyAction.Damaged):
                Debug.Log("Notify Hit");
                if(enemyStats.currentEnemyHP > 0)
                {
                    StartCoroutine(DamageIndicator()); // Enable damage flickering effect
                    enemyStats.currentEnemyHP--;
                }
                return;
            case(EnemyAction.Shoot):
                if(enemySpriteRenderer.flipX == true)
                {
                    EnemyShoot(bulletLeftSpawn, Vector2.left, true);
                }
                else
                {
                    EnemyShoot(bulletRightSpawn, Vector2.right, false);
                }
                return;
            case(EnemyAction.Jump):
                return;
            case (EnemyAction.Dead):
                enemySpriteRenderer.color = Color.white;
                StopAllCoroutines();
                StartCoroutine(EnemyDead());
                return;
        }
    }
    private void EnemyShoot(Transform spawnPos, Vector2 bulletDirection, bool isLeft)
    {
        GameObject enemyBullet = enemyBulletPooler.EnableEnemyBullet();
        int changeToHealPlayer = Random.Range(1, 10);
        EnemyBullet component = enemyBullet.GetComponent<EnemyBullet>();
        if (changeToHealPlayer >= 8) // Switch to heal bullet
        {
            component.isHealBullet = true;
        }
        else // Switch to normal bullet
        {
            component.isHealBullet = false;
        }
        if (enemyBullet != null)
        {
            enemyBullet.transform.position = spawnPos.position;
            enemyBullet.transform.rotation = spawnPos.rotation;
            component.bulletDirection = bulletDirection;
            if(isLeft == true)
            {
                enemyBullet.GetComponentInChildren<SpriteRenderer>().flipY = true;
            }
            else
            {
                enemyBullet.GetComponentInChildren<SpriteRenderer>().flipY = false;
            }
            enemyBullet.SetActive(true);
        }
    }
    private IEnumerator DamageIndicator()
    {
        enemySpriteRenderer.color = enemyDamagedColor;
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = Color.white;
    }
    private IEnumerator EnemyDead()
    {
        enemySpriteRenderer.enabled = false;
        normalEnemySubject.GetComponent<EnemyShooterStateController>().enemyHitBox.enabled = false;
        yield return new WaitForSeconds(0.3f);
        normalEnemySubject.gameObject.SetActive(false);
    }
}
