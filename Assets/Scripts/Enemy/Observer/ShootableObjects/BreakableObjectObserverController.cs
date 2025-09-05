using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectObserverController : MonoBehaviour, INormalEnemyObserver
{
    [Header("Observer Reference")]
    [SerializeField] private NormalEnemySubject breakableObjectSubject;
    
    [Header("Enemy Sprite")]
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private Color enemyDamagedColor;
    private void OnEnable()
    {
        breakableObjectSubject.AddNormalEnemyObserver(this);
    }
    private void OnDisable()
    {
        breakableObjectSubject.RemoveNormalEnemyObserver(this);
    }
    public void OnNormalEnemyNotify(EnemyAction action)
    {
        switch (action)
        {
            case (EnemyAction.Damaged):
                StartCoroutine(DamageIndicator()); // Enable damage flickering effect
                break;
            case (EnemyAction.Dead):
                enemySpriteRenderer.color = Color.white;
                StopAllCoroutines();
                StartCoroutine(EnemyDead());
                break;
        }
    }
    private IEnumerator DamageIndicator()
    {
        enemySpriteRenderer.color = enemyDamagedColor;
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = Color.white;
    }
    private IEnumerator EnemyDead()
    {
        enemySpriteRenderer.enabled = false;
        breakableObjectSubject.GetComponent<BreakableObjectEnemy>().objectHitBox.enabled = false;
        yield return new WaitForSeconds(0.3f);
        breakableObjectSubject.gameObject.SetActive(false);
    }
}
