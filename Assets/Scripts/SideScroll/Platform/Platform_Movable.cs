using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Movable : Platform
{
    [Header("Movable Platform Properties")]
    [SerializeField] private float platformSpeed = 1f;
    [SerializeField] private Transform[] platformMovePath;
    private Rigidbody2D platformRB;
    private int platformDestination = 1;
    private void Awake()
    {
        platformRB = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
    private void FixedUpdate()
    {
        Vector2 direction = Vector2.MoveTowards(transform.position, platformMovePath[platformDestination].position, platformSpeed * Time.fixedDeltaTime); 
        platformRB.MovePosition(direction);
        if(Vector2.Distance(transform.position, platformMovePath[1].position) <= 0)
        {
            platformDestination = 0;
        }
        else if(Vector2.Distance(transform.position, platformMovePath[0].position) <= 0)
        {
            platformDestination = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Vector2 normal = collision.GetContact(0).normal;
            if (normal.y == -1)
            {
                collision.gameObject.transform.SetParent(transform);
                platformAnim.Play();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
