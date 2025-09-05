using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BomberEnemyObserverController : MonoBehaviour, INormalEnemyObserver
{
    [Header("Bomber Observer References")]
    private NormalEnemySubject bomberEnemySubject;
    private EnemyBomberStateController bomberEnemyStat;
    [Header("Enemy Sprite")]
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private Color enemyDamagedColor;
    private void Awake()
    {
        bomberEnemySubject = GetComponent<EnemyBomberStateController>();
        bomberEnemyStat = GetComponent<EnemyBomberStateController>();
    }
    private void OnEnable()
    {
        bomberEnemySubject.AddNormalEnemyObserver(this);
        bomberEnemyStat.enemyAnimator.SetBool("isDead", false);
        bomberEnemyStat.enemyAnimator.SetBool("isRun", true);
    }
    private void OnDisable()
    {
        bomberEnemySubject.RemoveNormalEnemyObserver(this);
    }
    public void OnNormalEnemyNotify(EnemyAction action)
    {
        switch(action)
        {
            case (EnemyAction.Damaged):
                Debug.Log("Notify Hit");
                if(bomberEnemyStat.currentEnemyHP > 0)
                {
                    StartCoroutine(DamageIndicator());
                    bomberEnemyStat.currentEnemyHP--;
                }
                return;
            case (EnemyAction.Explode):
                enemySpriteRenderer.color = Color.white; // Reset sprite color
                StartCoroutine(EnemyExplode());
                return;
        }
    }
    private IEnumerator EnemyExplode()
    {
        bomberEnemyStat.enemyAnimator.SetBool("isDead", true);
        bomberEnemyStat.enemyAnimator.SetBool("isRun", false);
        yield return new WaitForSeconds(0.5f);
        bomberEnemySubject.gameObject.SetActive(false);
    }
    private IEnumerator DamageIndicator()
    {
        enemySpriteRenderer.color = enemyDamagedColor;
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = Color.white;
    }
}
