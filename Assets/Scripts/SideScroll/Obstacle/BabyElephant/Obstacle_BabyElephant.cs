using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_BabyElephant : MonoBehaviour
{
    [Header("General Properties")]
    public float travelSpeed;
    public Transform destination;
    private Rigidbody2D rb;

    [Header("Baby elephant Sprites")]
    public SpriteRenderer babyElephantSpriteRenderer;

    [Header("Baby elephant Animator")]
    public Animator babyElephantAnimator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 obstaclePosition = transform.position;
        obstaclePosition += Vector2.ClampMagnitude(destination.localRotation * Vector2.up, 1) * travelSpeed * Time.deltaTime;
        transform.position = obstaclePosition;
        Vector2 camToViewportPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (camToViewportPoint.x <= -0.2f || camToViewportPoint.x >= 1.3f || camToViewportPoint.y <= 0 || camToViewportPoint.y >= 1.1f)
        {
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        //Vector2 direction = Vector2.MoveTowards(transform.position, destination.position, travelSpeed * Time.fixedDeltaTime);
        //rb.MovePosition(direction);
        //rb.velocity = Vector2.left * travelSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && transform.GetChild(2).GetComponent<BoxCollider2D>().usedByEffector == true)
        {
            Vector2 normal = collision.GetContact(0).normal;
            if(normal == Vector2.down && collision.transform.GetComponent<PlayerSideScrollStateController>().isHitWall == false)
            {
                collision.transform.SetParent(transform);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
