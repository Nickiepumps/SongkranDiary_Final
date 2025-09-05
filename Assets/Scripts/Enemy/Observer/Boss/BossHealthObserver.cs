using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHealthObserver : MonoBehaviour, IBossObserver
{
    [Header("Boss Observer References")]
    [SerializeField] private BossSubject bossSubject;
    [Header("Audio Reference")]
    [SerializeField] private AudioClip[] enemyAudioClipArr;
    [SerializeField] private AudioSource enemyAudioSource;
    [Header("Boss Health Properties")]
    [SerializeField] private BossHealth bossHealth;
    public int bossMaxHP; // Used for game controller to check boss progression
    [Header("Boss Sprite")]
    [SerializeField] private SpriteRenderer bossSpriteRenderer;
    [SerializeField] private Color bossDamagedColor; // Always use this to adjust boss damage color indicator
    [SerializeField] private Color bossHealedColor; // Use this when boss has healing ability
    private void OnEnable()
    {
        bossSubject.AddBossObserver(this);
    }
    private void OnDisable()
    {
        bossSubject.RemoveBossObserver(this);
    }
    private void Start()
    {
        bossMaxHP = bossHealth.bossStats.hp;
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case (BossAction.Damaged):
                StartCoroutine(DamageIndicator());
                if (bossHealth.currentBossArmor <= 0)
                {
                    bossHealth.currentBossHP--;
                    enemyAudioSource.clip = enemyAudioClipArr[0];
                    enemyAudioSource.Play();
                }
                else
                {
                    bossHealth.currentBossArmor--;
                    enemyAudioSource.clip = enemyAudioClipArr[1];
                    enemyAudioSource.Play();
                }
                return;
            case (BossAction.Heal):
                Debug.Log("Boss heal");
                bossHealth.currentBossHP += bossHealth.bossStats.healAmount;
                enemyAudioSource.clip = enemyAudioClipArr[3];
                enemyAudioSource.Play();
                return;
            case (BossAction.Die):
                enemyAudioSource.clip = enemyAudioClipArr[2];
                enemyAudioSource.Play();
                return;
        }
    }
    private IEnumerator DamageIndicator()
    {
        bossSpriteRenderer.color = bossDamagedColor;
        yield return new WaitForSeconds(0.1f);
        bossSpriteRenderer.color = Color.white;
    }
}
