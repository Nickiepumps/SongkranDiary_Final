using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingSubject : MonoBehaviour
{
    private List<IShootingObserver> shootingObserverLists = new List<IShootingObserver>();
    public void AddShootingObserver(IShootingObserver shootingObserver)
    {
        shootingObserverLists.Add(shootingObserver);
    }
    public void RemoveShootingObserver(IShootingObserver shootingObserver)
    {
        shootingObserverLists.Remove(shootingObserver);
    }
    public void NotifyShootingObserver(ShootingAction shootingAction)
    {
        shootingObserverLists.ForEach((shootingObserver) =>
        {
            shootingObserver.OnShootingNotify(shootingAction);
        });
    }
}
