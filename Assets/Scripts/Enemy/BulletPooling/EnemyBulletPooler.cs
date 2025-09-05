using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPooler : MonoBehaviour
{
    [Header("Enemy Bullet Prefab")]
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject incomingBulletPrefab;
    [SerializeField] private GameObject obstacleBulletPrefab;

    [Header("Pooling Property")]
    [SerializeField] private int amountTopool; // Amount of bullet for enemy
    [SerializeField] private int incomingBulletamountTopool; // Amount of bullet for periodic attack
    [SerializeField] private int obstacleBulletamountTopool; // Amount of bullet for any obstacle that can shoot
    [SerializeField] private Transform pooledEnemyBulletGroup; // Prevent too many enemy bullet gameObjects appear in Hierachy
    [SerializeField] private Transform pooledIncomingBulletGroup; // Pooled incoming bullet group
    [SerializeField] private Transform pooledObstacleBulletGroup; // Pooled obstacle bullet group
    private List<GameObject> pooledEnemyBulletList = new List<GameObject>();
    private List<GameObject> pooledIncomingBulletList = new List<GameObject>();
    private List<GameObject> pooledObstacleBulletList = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < amountTopool; i++)
        {
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, pooledEnemyBulletGroup);
            pooledEnemyBulletList.Add(enemyBullet);
            enemyBullet.SetActive(false);
        }
        for(int i = 0; i < incomingBulletamountTopool; i++)
        {
            GameObject incomingBullet = Instantiate(incomingBulletPrefab, pooledIncomingBulletGroup);
            pooledIncomingBulletList.Add(incomingBullet);
            incomingBullet.SetActive(false);
        }
        if(obstacleBulletPrefab != null)
        {
            for (int i = 0; i < obstacleBulletamountTopool; i++)
            {
                GameObject obstacleBullet = Instantiate(obstacleBulletPrefab, pooledObstacleBulletGroup);
                pooledObstacleBulletList.Add(obstacleBullet);
                obstacleBullet.SetActive(false);
            }
        }
    }
    public GameObject EnableEnemyBullet()
    {
        for(int i = 0; i < pooledEnemyBulletList.Count; i++)
        {
            if (pooledEnemyBulletList[i].activeSelf == false)
            {
                return pooledEnemyBulletList[i];
            }
        }
        return null;
    }
    public GameObject EnableIncomingBullet()
    {
        for(int i = 0; i < pooledIncomingBulletList.Count; i++)
        {
            if (pooledIncomingBulletList[i].activeSelf == false)
            {
                return pooledIncomingBulletList[i];
            }
        }
        return null;
    }
    public GameObject EnableObstacleBullet()
    {
        for (int i = 0; i < pooledObstacleBulletList.Count; i++)
        {
            if (pooledObstacleBulletList[i].activeSelf == false)
            {
                return pooledObstacleBulletList[i];
            }
        }
        return null;
    }
}
