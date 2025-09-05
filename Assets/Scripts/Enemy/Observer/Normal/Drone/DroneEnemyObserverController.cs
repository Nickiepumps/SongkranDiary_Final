using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemyObserverController : MonoBehaviour, INormalEnemyObserver
{
    private NormalEnemySubject droneEnemySubject;
    private EnemyDroneStateController droneEnemyStats;

    [Header("Enemy Sprite")]
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private Color enemyDamagedColor;

    [Header("Audio Reference")]
    public AudioClip[] enemyAudioClipArr;
    public AudioSource enemyAudioSource;
    private void Awake()
    {
        droneEnemySubject = GetComponent<EnemyDroneStateController>();
        droneEnemyStats = GetComponent<EnemyDroneStateController>();
    }
    private void OnEnable()
    {
        droneEnemySubject.AddNormalEnemyObserver(this);
        //droneEnemyStats.droneEnemyAnimator.SetBool("isExplode", false);
        //droneEnemyStats.droneEnemyAnimator.SetBool("isFly", true);
    }
    private void OnDisable()
    {
        droneEnemySubject.RemoveNormalEnemyObserver(this);
    }
    public void OnNormalEnemyNotify(EnemyAction action)
    {
        switch (action)
        {
            case(EnemyAction.Damaged):
                Debug.Log("Notify Hit");
                if(droneEnemyStats.currentEnemyHP > 0)
                {
                    StartCoroutine(DamageIndicator()); // Enable damage flickering effect
                    enemyAudioSource.clip = enemyAudioClipArr[0];
                    enemyAudioSource.Play();
                    droneEnemyStats.currentEnemyHP--;
                }
                return;
            case (EnemyAction.Explode):
                StartCoroutine(EnemyExplode());
                enemyAudioSource.clip = enemyAudioClipArr[1];
                enemyAudioSource.Play();
                enemySpriteRenderer.color = Color.white;
                return;
        }
    }
    private IEnumerator EnemyExplode()
    {
        droneEnemyStats.droneEnemyAnimator.SetBool("isExplode", true);
        droneEnemyStats.droneEnemyAnimator.SetBool("isFly", false);
        droneEnemyStats.enemyCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        enemySpriteRenderer.enabled = false;
        yield return new WaitForSeconds(1f);
        droneEnemySubject.gameObject.SetActive(false);
    }
    private IEnumerator DamageIndicator()
    {
        enemySpriteRenderer.color = enemyDamagedColor;
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = Color.white;
    }
}
