using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemySpawnerController : MonoBehaviour
{
    [Header("Spawner Reference")]
    [SerializeField] private NormalEnemySpawner normalEnemySpawner;

    [Header("Camera Follow reference")]
    [SerializeField] private SideScroll_PlayerCamera playerCam;

    [Header("Enemy Spawner")]
    [SerializeField] private Transform groundRightSpawn;
    [SerializeField] private Transform groundLeftSpawn;
    [SerializeField] private Transform airRightSpawn;
    [SerializeField] private Transform airLeftSpawn;

    [Header("Spawn Setting")]
    public bool spawnShooter;
    public bool spawnBomber;
    public bool spawnDrone;

    [Header("All Shooter and Bomber Enemies")]
    private List<GameObject> shooterLists = new List<GameObject>();
    private List<GameObject> bomberLists = new List<GameObject>();
    private List<GameObject> droneLists = new List<GameObject>();

    [Header("Enemy Spawn Rate")]
    [Header("Shooter")]
    [SerializeField] private float minShooterSpawnRate;
    [SerializeField] private float maxShooterSpawnRate;
    [Header("Bomber")]
    [SerializeField] private float minBomberSpawnRate;
    [SerializeField] private float maxBomberSpawnRate;
    [Header("Drone")]
    [SerializeField] private float minDroneSpawnRate;
    [SerializeField] private float maxDroneSpawnRate;

    private float spawnRate;
    private float currentTimeToSpawnShooter;
    private float currentTimeToSpawnBomber;
    private float currentTimeToSpawnDrone;
    private void Start()
    {
        currentTimeToSpawnShooter = 5;
        currentTimeToSpawnBomber = 10;
        currentTimeToSpawnDrone = 10;
    }
    private void Update()
    {
        /*// Spawn all enemy type if player is on low elevation
        if(playerCam.camYTarget == playerCam.midCamY)
        {
            spawnShooter = true;
            spawnBomber = true;
            spawnDrone = true;
        }
        // Spawn all enemy type if player is on high elevation
        else if (playerCam.camYTarget == playerCam.maxCamDistY)
        {
            spawnShooter = false;
            spawnBomber = false;
            spawnDrone = true;
        }*/
        SpawnNormalEnemy();
    }
    private void SpawnNormalEnemy()
    {
        currentTimeToSpawnShooter -= Time.deltaTime;
        currentTimeToSpawnBomber -= Time.deltaTime;
        currentTimeToSpawnDrone -= Time.deltaTime;
        if (spawnShooter == true && currentTimeToSpawnShooter <= 0)
        {
            for (int i = 0; i < normalEnemySpawner.spawnedShooterLists.Count; i++)
            {
                if (normalEnemySpawner.spawnedShooterLists[i].activeSelf == false)
                {
                    GameObject enemyGO = normalEnemySpawner.spawnedShooterLists[i];
                    EnemyShooterStateController enemyStateController = enemyGO.GetComponent<EnemyShooterStateController>();
                    SpriteRenderer spriteRenderer = enemyStateController.enemySpriteRenderer;
                    Transform path = NewSpawnAndDestination(enemyGO, spriteRenderer, true);
                    enemyStateController.startPoint = path;
                    enemyGO.SetActive(true);
                    currentTimeToSpawnShooter = Random.Range(minShooterSpawnRate, maxShooterSpawnRate);
                    return;
                }
            }
        }
        if (spawnBomber == true && currentTimeToSpawnBomber <= 0)
        {
            for (int i = 0; i < normalEnemySpawner.spawnedBomberLists.Count; i++)
            {
                if (normalEnemySpawner.spawnedBomberLists[i].activeSelf == false)
                {
                    GameObject enemyGO = normalEnemySpawner.spawnedBomberLists[i];
                    EnemyBomberStateController enemyBomberStateController = enemyGO.GetComponent<EnemyBomberStateController>();
                    SpriteRenderer spriteRenderer = enemyBomberStateController.enemySpriteRenderer;
                    Transform path = NewSpawnAndDestination(enemyGO, spriteRenderer, true);
                    enemyBomberStateController.startPoint = path;
                    enemyGO.SetActive(true);
                    currentTimeToSpawnBomber = Random.Range(minBomberSpawnRate, maxBomberSpawnRate);
                    return;
                }
            }
        }
        if (spawnDrone == true && currentTimeToSpawnDrone <= 0)
        {
            for (int i = 0; i < normalEnemySpawner.spawnedDroneLists.Count; i++)
            {
                if (normalEnemySpawner.spawnedDroneLists[i].activeSelf == false)
                {
                    GameObject enemyGO = normalEnemySpawner.spawnedDroneLists[i];
                    EnemyDroneStateController enemyDroneStateController = enemyGO.GetComponent<EnemyDroneStateController>();
                    SpriteRenderer spriteRenderer = enemyDroneStateController.enemySpriteRenderer;
                    Transform path = NewSpawnAndDestination(enemyGO, spriteRenderer, false);
                    enemyDroneStateController.startPoint = path;
                    enemyGO.SetActive(true);
                    currentTimeToSpawnDrone = Random.Range(minDroneSpawnRate, maxDroneSpawnRate);
                    return;
                }
            }
        }
    }
    private Transform[] NewSpawnAndDestination(GameObject enemyGO, Transform enemyStartPoint, Transform enemyDestination, SpriteRenderer spriteRenderer)
    {
        Transform[] newPath = new Transform[2];
        int spawnValue = Random.Range(0, 2);
        float spawnXOffset = 0f;
        Transform previousStart = enemyStartPoint;
        Transform previousDestination = enemyDestination;
        Transform newStart;
        Transform newDestination;

        if(spawnValue == 0)
        {
            newStart = previousDestination;
            newDestination = previousStart;
            RaycastHit2D holeRayCheck = Physics2D.Raycast(newStart.position, Vector2.down, Mathf.Infinity);
            if(holeRayCheck.collider.tag != "E_Boundary")
            {
                Debug.Log("There is a hole under the spawn, spawn the enemy at 15 units beside the spawn point");
                spawnXOffset = 3f;
            }
            else
            {
                spawnXOffset = 0f;
            }
        }
        else
        {
            newStart = previousStart;
            newDestination = previousDestination;
        }

        // Flip the sprite if the starting point is facing left
        // Check if there is a hole underneath the spawn point; If there is, spawn the enemy at 3 units beside the spawn point
        if (newStart.transform.eulerAngles == new Vector3(0, 0, 90))
        {
            spriteRenderer.flipX = false;
            RaycastHit2D holeRayCheck = Physics2D.Raycast(newStart.position, Vector2.down, Mathf.Infinity);
            if (holeRayCheck.collider.tag != "Side_Floor")
            {
                Debug.Log("There is a hole under the spawn, spawn the enemy at 15 units beside the spawn point");
                spawnXOffset = 3f;
            }
            else
            {
                spawnXOffset = 0f;
            }
        }
        else
        {
            spriteRenderer.flipX = true;
            RaycastHit2D holeRayCheck = Physics2D.Raycast(newStart.position, Vector2.down, Mathf.Infinity);
            if (holeRayCheck.collider.tag != "Side_Floor")
            {
                Debug.Log("There is a hole under the spawn, spawn the enemy at 15 units beside the spawn point");
                spawnXOffset = -3f;
            }
            else
            {
                spawnXOffset = 0f;
            }
        }
        newPath[0] = newStart;
        newPath[1] = newDestination;
        enemyGO.transform.position = new Vector2(newPath[0].position.x + spawnXOffset, newPath[0].position.y);

        return newPath;
    }
    private Transform NewSpawnAndDestination(GameObject enemyGO, SpriteRenderer spriteRenderer, bool isGroundType)
    {
        int spawnValue = Random.Range(0, 2);
        float spawnXOffset = 0f;
        Transform newStart;

        // Random spawnpoint
        // Check if there is a hole underneath the spawn point; If there is, spawn the enemy at 3 units beside the spawn point
        if(isGroundType == true)
        {
            if (spawnValue == 0)
            {
                newStart = groundLeftSpawn;
                RaycastHit2D holeRayCheck = Physics2D.Raycast(newStart.position, Vector2.down, Mathf.Infinity);
                if (holeRayCheck.collider.tag != "E_Boundary" && holeRayCheck.collider.tag != "Side_Floor")
                {
                    Debug.Log("There is a hole or obstacle under the spawn, spawn the enemy at 3 units behind the spawn point");
                    spawnXOffset = -3f;
                }
                else
                {
                    spawnXOffset = 0f;
                }
            }
            else
            {
                newStart = groundRightSpawn;
                RaycastHit2D holeRayCheck = Physics2D.Raycast(newStart.position, Vector2.down, Mathf.Infinity);
                if (holeRayCheck.collider.tag != "E_Boundary" && holeRayCheck.collider.tag != "Side_Floor")
                {
                    Debug.Log("There is a hole or obstacle under the spawn, spawn the enemy at 3 units behind the spawn point");
                    spawnXOffset = 3f;
                }
                else
                {
                    spawnXOffset = 0f;
                }
            }
        }
        else
        {
            if (spawnValue == 0)
            {
                newStart = airLeftSpawn;
            }
            else
            {
                newStart = airRightSpawn;
            }
        }

        // Flip the sprite if the starting point is facing left
        if (newStart.transform.eulerAngles == new Vector3(0, 0, 90))
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        enemyGO.transform.position = new Vector2(newStart.position.x + spawnXOffset, newStart.position.y);

        return newStart;
    }
}
