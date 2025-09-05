using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Special_WaterShoot : MonoBehaviour, IBossObserver
{
    [Header("Boss Observer Reference")]
    [SerializeField] private BossSubject bossSubject;

    [Header("WaterPipe references")]
    [SerializeField] private Obstacle_FireHydrant[] waterPipeObstacleArr;
    [SerializeField] private SpriteRenderer[] waterSpriteArr;
    private void OnEnable()
    {
        bossSubject.AddBossObserver(this);
    }
    private void OnDisable()
    {
        bossSubject.RemoveBossObserver(this);
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case (BossAction.Ult2):
                StartCoroutine(RandomPipeAttack());
                break;
        }
    }
    private IEnumerator RandomPipeAttack()
    {
        int result1 = Random.Range(0, 4);
        //int result2 = Random.Range(2, 4);
        waterPipeObstacleArr[result1].enabled = true;
        waterSpriteArr[result1].enabled = true;
        //waterPipeObstacleArr[result2].enabled = true;
        //waterSpriteArr[result2].enabled = true;
        yield return new WaitForSeconds(waterPipeObstacleArr[result1].waterInitialCooldownTime + 0.8f);
        waterPipeObstacleArr[result1].enabled = false;
        waterSpriteArr[result1].enabled = false;
        //waterPipeObstacleArr[result2].enabled = false;
        //waterSpriteArr[result2].enabled = false;
    }
}
