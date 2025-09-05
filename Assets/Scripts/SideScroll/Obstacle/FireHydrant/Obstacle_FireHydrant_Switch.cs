using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_FireHydrant_Switch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            gameObject.transform.parent.GetComponent<Obstacle_FireHydrant>().isShootingIndefinitely = false;
            gameObject.SetActive(false);
        }
    }
}
