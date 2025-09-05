using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletType bulletType; // Bullet type
    public float travelSpeed;
    public SpriteRenderer bulletSprite; // Bullet's sprite
    public Vector2 bulletDirection;
    public Transform laserBoundary;
    private BulletPooler bulletPooler;
    private BoxCollider2D bulletCollider;
    private EdgeCollider2D bulletEdgeCollider;
    private bool isHit = false;
    private Camera cam;

    [Header("Bullet Animator")]
    public Animator bulletAnimator;
    private void OnEnable()
    {
        if(bulletCollider == null && bulletPooler == null)
        {
            bulletCollider = GetComponent<BoxCollider2D>();
            bulletEdgeCollider = GetComponent<EdgeCollider2D>();
            bulletPooler = GameObject.Find("BulletPooler").GetComponent<BulletPooler>();
        }
        if (bulletType == BulletType.laser)
        {
            StartCoroutine(LaserBullet());
        }
    }
    private void OnDisable()
    {
        isHit = false;
        bulletAnimator.SetBool("isHit", false);
        if (bulletPooler == null)
        {
            bulletPooler = GameObject.Find("BulletPooler").GetComponent<BulletPooler>();
        }
        bulletSprite.sprite = bulletPooler.currentBulletSprite; // Set the bullet sprite to match the current bullet type 
        bulletType = bulletPooler.currentBulletType; // Set the bullet behavior to match the current bullet type
        //bulletAnimator.runtimeAnimatorController = bulletPooler.currentAnimator; // Set the bullet animator to match the current bullet type
    }
    private void Start()
    {
        cam = Camera.main;
        bulletCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if(bulletType == BulletType.normalBullet)
        {
            NormalBullet();
        }
        else if(bulletType == BulletType.spreadBullet)
        {
            SpreadBullet();
        }

        // To Do: Deactivate Bullet when it go pass the camera
        Vector2 bulletPos = cam.WorldToViewportPoint(transform.position);
        if(bulletPos.x < -0.1f || bulletPos.x > 1.1f || bulletPos.y < -0.1f || bulletPos.y > 1.1f)
        {
            if (bulletType != BulletType.laser)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void NormalBullet()
    {
        bulletCollider.enabled = true;
        Vector2 bulletPosition = transform.position;
        if(isHit == false)
        {
            bulletPosition += bulletDirection * travelSpeed * Time.deltaTime;
        }
        transform.position = bulletPosition;
    }
    private void SpreadBullet()
    {
        bulletCollider.enabled = true;
        Vector2 bulletPosition = transform.position;
        if(isHit == false)
        {
            bulletPosition += bulletDirection * travelSpeed * Time.deltaTime;
        }
        transform.position = bulletPosition;
    }
    private IEnumerator LaserBullet()
    {
        bulletAnimator.SetBool("isLaser", true);
        bulletCollider.enabled = false; // Disable bullet collider
        bulletEdgeCollider.enabled = true;
        float laserDist = Vector2.Distance(transform.position, laserBoundary.position); // Check distance from laser boundary to bullet spawn
        bulletEdgeCollider.SetPoints(new List<Vector2> { Vector2.zero, new Vector2(0, laserDist) }); // Get the points of the edge collider
        bulletSprite.size = new Vector2(laserDist, 1); // Scale the object in local space to match the laser distance
        Vector2 bulletPosition = transform.position;
        /*RaycastHit2D[] laserHit = Physics2D.RaycastAll(transform.position, laserBoundary.position);
        foreach(RaycastHit2D raycastHit in laserHit)
        {
            if (raycastHit == true && raycastHit.collider.gameObject.tag == "Enemy")
            {
                if(raycastHit.transform.GetComponent<NormalEnemyObserverController>() != null)
                {
                    raycastHit.transform.GetComponent<NormalEnemyObserverController>().OnNormalEnemyNotify(EnemyAction.Damaged);
                }
                else
                {
                    raycastHit.transform.GetComponent<BossHealthObserver>().OnBossNotify(BossAction.Damaged);
                }
            }
        }*/
        yield return new WaitForSeconds(0.3f); // Appear only for 0.15 secs
        //laserHit = null; // Clear all the hit object
        bulletEdgeCollider.enabled = false;
        bulletAnimator.SetBool("isLaser", false);
        gameObject.SetActive(false); // Disable bullet
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyHitBox" && bulletType != BulletType.laser)
        {
            isHit = true;
            StartCoroutine(DeactivateBullet());
        }
        else if(collision.gameObject.tag == "EnemyBullet" && bulletType != BulletType.laser)
        {
            StartCoroutine(DeactivateBullet());
        }
        else if(collision.gameObject.tag == "Side_Interactable" && bulletType != BulletType.laser)
        {
            StartCoroutine(DeactivateBullet());
        }
    }
    // Play splash animation and deactivate bullet
    private IEnumerator DeactivateBullet()
    {
        if(bulletType == BulletType.normalBullet)
        {
            bulletAnimator.SetBool("isHit", true);
            yield return new WaitForSeconds(0.15f);
        }
        else if(bulletType == BulletType.spreadBullet)
        {
            bulletAnimator.SetBool("isHit", true);
            yield return new WaitForSeconds(0.15f);
        }
        gameObject.SetActive(false);
    }
}
