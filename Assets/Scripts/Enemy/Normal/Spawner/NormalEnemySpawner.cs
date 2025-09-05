using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemySpawner : MonoBehaviour
{
    [Header("Enemy type to spawn")]
    [SerializeField] private bool allowShooterSpawn;
    [SerializeField] private bool allowBomberSpawn;
    [SerializeField] private bool allowStationarySpawn;
    [SerializeField] private bool allowDroneSpawn;

    [Header("Spawn Properties")]
    [SerializeField] private int shooterAmountToSpawn;
    [SerializeField] private int bomberAmountToSpawn;
    [SerializeField] private int droneAmountToSpawn;

    [Header("Spawn Group")]
    [SerializeField] private Transform spawnedShooterGroup; // Prevent too many enemy gameObjects appear in Hierachy
    [SerializeField] private Transform spawnedBomberGroup; // Prevent too many enemy gameObjects appear in Hierachy
    [SerializeField] private Transform spawnedStationaryGroup; // Prevent too many enemy gameObjects appear in Hierachy
    [SerializeField] private Transform spawnedDroneGroup; // Prevent too many enemy gameObjects appear in Hierachy

    [Header("Spawners and Prefab")]
    /*public List<Transform> shooterSpawnerlist;
    public List<Transform> bomberSpawnerlist;
    public List<Transform> droneSpawnerlist;*/
    [SerializeField] private Transform groundRightSpawner;
    [SerializeField] private Transform groundLeftSpawner;
    [SerializeField] private Transform airRightSpawner;
    [SerializeField] private Transform airLeftSpawner;
    [SerializeField] private List<Transform> stationarySpawnerlist;
    [SerializeField] private GameObject shooterEnemyPrefab;
    [SerializeField] private GameObject bomberEnemyPrefab;
    [SerializeField] private GameObject droneEnemyPrefab;
    public List<GameObject> spawnedShooterLists = new List<GameObject>();
    public List<GameObject> spawnedBomberLists = new List<GameObject>();
    public List<GameObject> spawnedDroneLists = new List<GameObject>();

    [Header("Enemy Scriptable Object")]
    [SerializeField] private NormalEnemySO shooterSO;
    [SerializeField] private NormalEnemySO bomberSO;
    [SerializeField] private NormalEnemySO stationarySO;
    [SerializeField] private NormalEnemySO droneSO;
    private void Start()
    {
        CreateNormalEnemy();
    }
    private void CreateNormalEnemy()
    {
        if (allowShooterSpawn == true)
        {
            for (int i = 0; i < shooterAmountToSpawn; i++)
            {
                int spawnElement = Random.Range(0, 2); // Random Spawnpoint
                GameObject shooterEnemy;
                if (spawnElement == 0)
                {
                    shooterEnemy = Instantiate(shooterEnemyPrefab, groundLeftSpawner.position, Quaternion.identity, spawnedShooterGroup);
                    shooterEnemy.GetComponent<EnemyShooterStateController>().startPoint = groundLeftSpawner;
                    shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.flipX = true;
                }
                else
                {
                    shooterEnemy = Instantiate(shooterEnemyPrefab, groundRightSpawner.position, Quaternion.identity, spawnedShooterGroup);
                    shooterEnemy.GetComponent<EnemyShooterStateController>().startPoint = groundRightSpawner;
                    shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.flipX = false;
                }
                spawnedShooterLists.Add(shooterEnemy);
                shooterEnemy.SetActive(false);
            }
        }
        if (allowBomberSpawn == true)
        {
            for (int i = 0; i < bomberAmountToSpawn; i++)
            {
                int spawnElement = Random.Range(0, 2); // Random Spawnpoint
                GameObject bomberEnemy;
                if (spawnElement == 0)
                {
                    bomberEnemy = Instantiate(bomberEnemyPrefab, groundLeftSpawner.position, Quaternion.identity, spawnedBomberGroup);
                    bomberEnemy.GetComponent<EnemyBomberStateController>().startPoint = groundLeftSpawner;
                    bomberEnemy.GetComponent<EnemyBomberStateController>().enemySpriteRenderer.flipX = true;
                }
                else
                {
                    bomberEnemy = Instantiate(bomberEnemyPrefab, groundRightSpawner.position, Quaternion.identity, spawnedBomberGroup);
                    bomberEnemy.GetComponent<EnemyBomberStateController>().startPoint = groundRightSpawner;
                    bomberEnemy.GetComponent<EnemyBomberStateController>().enemySpriteRenderer.flipX = false;
                }
                spawnedBomberLists.Add(bomberEnemy);
                bomberEnemy.SetActive(false);
            }
        }
        if (allowDroneSpawn == true)
        {
            for (int i = 0; i < droneAmountToSpawn; i++)
            {
                int spawnElement = Random.Range(0, 2); // Random Spawnpoint
                GameObject droneEnemy;
                if (spawnElement == 0)
                {
                    droneEnemy = Instantiate(droneEnemyPrefab, airLeftSpawner.position, Quaternion.identity, spawnedDroneGroup);
                    droneEnemy.GetComponent<EnemyDroneStateController>().startPoint = airLeftSpawner;
                    droneEnemy.GetComponent<EnemyDroneStateController>().enemySpriteRenderer.flipX = true;
                }
                else
                {
                    droneEnemy = Instantiate(droneEnemyPrefab, airRightSpawner.position, Quaternion.identity, spawnedDroneGroup);
                    droneEnemy.GetComponent<EnemyDroneStateController>().startPoint = airRightSpawner;
                    droneEnemy.GetComponent<EnemyDroneStateController>().enemySpriteRenderer.flipX = false;
                }
                spawnedDroneLists.Add(droneEnemy);
                droneEnemy.SetActive(false);
            }
        }
        if (allowStationarySpawn == true)
        {

        }
    }
}
