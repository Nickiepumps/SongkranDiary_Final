using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public Vector2 ultDirection;
    public float ultTravelSpeed;
    private void Update()
    {
        Vector2 bulletPosition = transform.position;
        bulletPosition += ultDirection * ultTravelSpeed * Time.deltaTime;
        transform.position = bulletPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "B_Boundary")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            /*if (collision.gameObject.GetComponent<NormalEnemyObserverController>())
            {
                collision.gameObject.GetComponent<NormalEnemyObserverController>().OnNormalEnemyNotify(EnemyAction.Damaged);
            }
            else
            {
                collision.gameObject.GetComponent<BossHealthObserver>().OnBossNotify(BossAction.Damaged);
            }*/
            gameObject.SetActive(false);
        }
    }
}
