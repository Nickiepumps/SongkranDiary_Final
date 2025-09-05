using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectEnemy : NormalEnemySubject
{
    [Header("Object Properties")]
    [SerializeField] private int objectMaxHealth;
    private int objectCurrentHealth;

    [Header("Audio Reference")]
    public AudioClip[] objectAudioClipArr;
    public AudioSource objectAudioSource;

    [Header("Collider")]
    public BoxCollider2D objectHitBox;
    private void OnEnable()
    {
        objectCurrentHealth = objectMaxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("PlayerBullet"):
                objectCurrentHealth--;
                if (objectCurrentHealth > 0)
                {
                    objectAudioSource.clip = objectAudioClipArr[0];
                    objectAudioSource.Play();
                    NotifyNormalEnemy(EnemyAction.Damaged);
                }
                else
                {
                    objectAudioSource.clip = objectAudioClipArr[1];
                    objectAudioSource.Play();
                    NotifyNormalEnemy(EnemyAction.Dead);
                }
                break;
        }
    }
}
