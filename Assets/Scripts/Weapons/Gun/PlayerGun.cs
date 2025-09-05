using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private BulletPooler bulletPooler;
    [SerializeField] private BulletShooting bulletShooting;
    [SerializeField] private SpriteRenderer playerSpriteDirection; // Player's sprite facing direction
    [SerializeField] private Transform aimPivot;
    [SerializeField] private Transform laserBoundary;
    private void Start()
    {
        bulletPooler = GameObject.Find("BulletPooler").GetComponent<BulletPooler>();
    }
    public void ShootBullet(BulletType bulletType)
    {
        GameObject bullet = bulletPooler.EnableBullet();
        if (bullet != null)
        {
            if(bulletType == BulletType.normalBullet)
            {
                bullet.GetComponent<Bullet>().bulletDirection = aimPivot.localRotation * Vector2.up;
                bullet.GetComponent<Bullet>().travelSpeed = bulletShooting.weaponData.currentWeaponTravelSpeed.travelSpeed; // Change bullet's travel speed
                bullet.transform.position = new Vector2(transform.position.x, Random.Range(transform.position.y - 0.2f, transform.position.y + 0.2f));
            }
            else if(bulletType == BulletType.spreadBullet)
            {
                if (playerSpriteDirection.flipX == false)
                {
                    bullet.GetComponent<Bullet>().bulletDirection = transform.localRotation * aimPivot.localRotation * Vector2.left; // Rotate bullet's travel direction to match the gun's rotation
                }
                else
                {
                    bullet.GetComponent<Bullet>().bulletDirection = transform.localRotation * aimPivot.localRotation * Vector2.left; // Rotate bullet's travel direction to match the gun's rotation
                }
                bullet.GetComponent<Bullet>().travelSpeed = bulletShooting.weaponData.currentWeaponSprdCount.travelSpeed; // Change bullet's travel speed
                bullet.transform.position = new Vector2(transform.position.x, transform.position.y);
            }
            else
            {
                if(playerSpriteDirection.flipX == false)
                {
                    bullet.GetComponent<Bullet>().bulletDirection = transform.localRotation * Vector2.up;
                    bullet.GetComponent<Bullet>().laserBoundary = laserBoundary;
                }
                else
                {
                    bullet.GetComponent<Bullet>().bulletDirection = transform.localRotation * Vector2.up;
                    bullet.GetComponent<Bullet>().laserBoundary = laserBoundary;
                }
                bullet.transform.position = new Vector2(transform.position.x, transform.position.y);
            }
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }
    public void ShootUltimate()
    {
        GameObject ultBullet = bulletPooler.EnableUltimate();
        if(ultBullet != null)
        {
            ultBullet.GetComponent<Ultimate>().ultDirection = aimPivot.localRotation * Vector2.up;
            ultBullet.GetComponent<Ultimate>().ultTravelSpeed = bulletShooting.weaponData.maxUltimateTravelSpeed.travelSpeed;
            ultBullet.transform.position = transform.position;
            ultBullet.transform.rotation = transform.rotation;
            ultBullet.SetActive(true);
        }
    }
}
