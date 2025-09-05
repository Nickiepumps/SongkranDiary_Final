using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSubject : MonoBehaviour
{
    private List<IPlayerObserver> playerObserverLists = new List<IPlayerObserver>();
    public void AddPlayerObserver(IPlayerObserver playerObserver)
    {
        playerObserverLists.Add(playerObserver);
    }
    public void RemovePlayerObserver(IPlayerObserver playerObserver)
    {
        playerObserverLists.Remove(playerObserver);
    }
    public void NotifyPlayerObserver(PlayerAction playerAction)
    {
        playerObserverLists.ForEach((npcObserver) =>
        {
            npcObserver.OnPlayerNotify(playerAction);
        });
    }
}
