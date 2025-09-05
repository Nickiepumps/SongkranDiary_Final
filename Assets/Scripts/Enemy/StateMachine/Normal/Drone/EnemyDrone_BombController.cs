using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone_BombController : MonoBehaviour
{
    [SerializeField] private GameObject droneEnemyParent;
    [SerializeField] private Rigidbody2D bombRB;
    [SerializeField] private BoxCollider2D bombCollider;
    [SerializeField] private Transform bombOriginalPos;
    private float bombResetPos = -4.1f; // Bomb reset its position when reaching this Y position
    private void OnDisable()
    {
        bombRB.gravityScale = 0;
        bombCollider.enabled = false;
        transform.position = bombOriginalPos.position;
    }
    private void Update()
    {
        if (transform.parent != null)
        {
            transform.position = new Vector2(droneEnemyParent.transform.position.x, transform.position.y);
        }
        if (transform.position.y <= bombResetPos)
        {
            transform.parent = droneEnemyParent.transform;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent = droneEnemyParent.transform;
            gameObject.SetActive(false);
        }
    }
}
