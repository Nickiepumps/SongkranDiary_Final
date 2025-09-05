using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossBulletType
{
    straight,
    homing,
    boomerang
}
public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Type")]
    public BossBulletType bulletType;

    [Header("General Properties")]
    public float travelSpeed;
    public Vector2 bulletDirection;
    public Collider2D damageCollider;
    public Collider2D healCollider;
    public bool isHealBullet = false;
    public bool isIncomingBullet = false;
    public bool canBulletBeDestroy = false;

    [Header("Boomerang Bullet Properties")]
    public Transform[] spawnPos; // All bullet spawn pos
    [HideInInspector] public Transform curerntSpawnPos; // Current bullet spawn pos
    [HideInInspector] public Transform curerntDestinationPos; // Current bullet spawn pos
    private float holdTime = 2f;
    private float currentHoldTime;
    private Rigidbody2D enemyBulletRB;

    [Header("Bullet Sprites")]
    public SpriteRenderer bulletSpriteRenderer;
    [SerializeField] public Sprite bulletSprite;
    [SerializeField] public Sprite healSprite;

    [Header("Bullet Animator")]
    public Animator bulletAnimator;
    private void OnEnable()
    {
        if(isHealBullet == false)
        {
            damageCollider.enabled = true;
            healCollider.enabled = false;
            bulletAnimator.SetBool("isHit", false);
            bulletAnimator.SetFloat("Variant", 0);
        }
        else
        {
            damageCollider.enabled = false;
            healCollider.enabled = true;
            bulletAnimator.SetBool("isHit", false);
            bulletAnimator.SetFloat("Variant", 1);
        }
        if(bulletType == BossBulletType.boomerang)
        {
            currentHoldTime = holdTime;
            bulletAnimator.SetBool("isHit", false);
            bulletAnimator.SetFloat("Variant", 1);
        }
    }
    private void OnDisable()
    {
        isHealBullet = false;
        damageCollider.enabled = true;
        healCollider.enabled = false;
        bulletAnimator.SetBool("isHit", false);
        bulletAnimator.SetFloat("Variant", 0);
    }
    private void Start()
    {
        enemyBulletRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 bulletPosition = transform.position;
        bulletPosition += Vector2.ClampMagnitude(bulletDirection,1) * travelSpeed * Time.deltaTime;
        transform.position = bulletPosition;
        Vector2 camToViewportPoint = Camera.main.WorldToViewportPoint(bulletPosition);
        if (isIncomingBullet == false)
        {
            if(bulletType != BossBulletType.boomerang)
            {
                if (camToViewportPoint.x <= 0 || camToViewportPoint.x >= 1.1f || camToViewportPoint.y <= 0 || camToViewportPoint.y >= 1.1f)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (camToViewportPoint.x <= -0.15f)
                {
                    Bullet_BoomerangTravel();
                }
                else if (camToViewportPoint.x >= 1.1f)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "B_Boundary" && isIncomingBullet == true)
        {
            gameObject.SetActive(false);
        }
        if(collision.tag == "Player")
        {
            StartCoroutine(DeactivateBullet()); // Play bullet splash anim
        }
        if (collision.tag == "PlayerBullet" && canBulletBeDestroy == true)
        {
            StartCoroutine(DeactivateBullet()); // Play bullet splash anim
        }
    }
    private void Bullet_BoomerangTravel()
    {
        if(curerntSpawnPos == spawnPos[0])
        {
            curerntSpawnPos = spawnPos[1];
            transform.position = new Vector2(transform.position.x, spawnPos[1].position.y);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, -90));
        }
        else
        {
            curerntSpawnPos = spawnPos[0];
            transform.position = new Vector2(transform.position.x, spawnPos[0].position.y);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, -90));
        }
        bulletDirection = Vector2.right;
    }
    // Play splash animation and deactivate bullet
    private IEnumerator DeactivateBullet()
    {
        bulletAnimator.SetBool("isHit", true);
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
    }
}
