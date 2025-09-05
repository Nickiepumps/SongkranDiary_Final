using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Obstacle_WaterPipe : MonoBehaviour
{
    [Header("Enemy Bullet Pooler References")]
    private EnemyBulletPooler enemyBulletPooler;
    [Header("component References")]
    [SerializeField] private Transform bulletSpawner;
    [Header("Obstacle Properties")]
    [SerializeField] private float cooldownTime = 2f;
    [SerializeField] private bool isHealingPipe = false;
    private float currentCooldownTime;
    private bool isActivate = false;
    
    private void Start()
    {
        enemyBulletPooler = GameObject.Find("EnemyBulletPooler").GetComponent<EnemyBulletPooler>();
        currentCooldownTime = 0;
    }
    private void Update()
    {
        currentCooldownTime -= Time.deltaTime;
        if(currentCooldownTime <= 0 && isActivate == true)
        {
            ShootBullet(bulletSpawner, isHealingPipe);
            currentCooldownTime = cooldownTime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.GetComponent<PlayerSideScrollStateController>().playerCurrentHP == 1 && isHealingPipe == false)
            {
                isActivate = false;
            }
            else
            {
                isActivate = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isActivate = false;
        }
    }
    private void ShootBullet(Transform spawnPos, bool isHealing)
    {
        GameObject pooledbullet = enemyBulletPooler.EnableObstacleBullet();
        if(pooledbullet != null)
        {
            if(isHealing == true)
            {
                pooledbullet.GetComponent<EnemyBullet>().isHealBullet = true;
            }
            else
            {
                pooledbullet.GetComponent<EnemyBullet>().isHealBullet = false;
            }
            pooledbullet.transform.position = spawnPos.position;
            pooledbullet.transform.localRotation = spawnPos.localRotation;
            pooledbullet.GetComponent<EnemyBullet>().bulletDirection = spawnPos.localRotation * Vector2.up;
            pooledbullet.SetActive(true);
        }
    }
}
