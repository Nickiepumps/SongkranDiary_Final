using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSubject : MonoBehaviour
{
    private List<IBossObserver> bossObserverLists = new List<IBossObserver>();
    public void AddBossObserver(IBossObserver bossObserver)
    {
        bossObserverLists.Add(bossObserver);
    }
    public void RemoveBossObserver(IBossObserver bossObserver)
    {
        bossObserverLists.Remove(bossObserver);
    }
    public void NotifyBoss(BossAction action)
    {
        bossObserverLists.ForEach((bossObserver) =>
        {
            bossObserver.OnBossNotify(action);
        });
    }
}
