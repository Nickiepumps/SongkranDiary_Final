using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_WaterPipe_Rotate : MonoBehaviour
{
    [SerializeField] private EnemyBulletPooler enemyBulletPooler;
    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private PlayerSideScrollStateController playerSideScrollStateController;
    [SerializeField] private float aspd;
    private float currentASPD;
    [SerializeField] private float rotateSpeed;
    private void Start()
    {
        currentASPD = aspd;
    }
    private void Update()
    {
        Vector3 lookDirection = transform.position - playerSideScrollStateController.transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
        currentASPD -= Time.deltaTime;
        if (currentASPD <= 0)
        {
            GameObject enemyBullet = enemyBulletPooler.EnableIncomingBullet();
            if (enemyBullet != null)
            {
                enemyBullet.transform.position = transform.position;
                enemyBullet.transform.localRotation = transform.localRotation;
                enemyBullet.GetComponent<EnemyBullet>().bulletDirection = -lookDirection;
                enemyBullet.SetActive(true);
            }
            currentASPD = aspd;
        }
    }
}
