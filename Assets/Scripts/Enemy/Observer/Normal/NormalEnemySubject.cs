using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalEnemySubject : MonoBehaviour
{
    private List<INormalEnemyObserver> normalEnemyObserverLists = new List<INormalEnemyObserver>();
    public void AddNormalEnemyObserver(INormalEnemyObserver normalEnemyObserver)
    {
        normalEnemyObserverLists.Add(normalEnemyObserver);
    }
    public void RemoveNormalEnemyObserver(INormalEnemyObserver normalEnemyObserver)
    {
        normalEnemyObserverLists.Remove(normalEnemyObserver);
    }
    public void NotifyNormalEnemy(EnemyAction action)
    {
        normalEnemyObserverLists.ForEach((normalEnemyObserver) =>
        {
            normalEnemyObserver.OnNormalEnemyNotify(action);
        });
    }
}
