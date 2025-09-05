using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class GameSubject : MonoBehaviour
{
    private List<IGameObserver> gameObserverLists = new List<IGameObserver>();
    private List<IGameObserver> sidescrollGameObserverLists = new List<IGameObserver>();
    public void AddGameObserver(IGameObserver isoGameObserver)
    {
        gameObserverLists.Add(isoGameObserver);
    }
    public void RemoveGameObserver(IGameObserver isoGameObserver)
    {
        gameObserverLists.Remove(isoGameObserver);
    }
    public void AddSideScrollGameObserver(IGameObserver sidescrollGameObserver)
    {
        sidescrollGameObserverLists.Add(sidescrollGameObserver);
    }
    public void RemoveSideScrollGameObserver(IGameObserver sidescrollGameObserver)
    {
        sidescrollGameObserverLists.Remove(sidescrollGameObserver);
    }
    public void NotifyGameObserver(IsometricGameState isoGameState)
    {
        for(int i = 0; i < gameObserverLists.Count; i++)
        {
            gameObserverLists[i].OnGameNotify(isoGameState);
        }
        /*gameObserverLists.ForEach((isoGameObserver) =>
        {
            isoGameObserver.OnGameNotify(isoGameState);
        });*/
    }
    public void NotifySideScrollGameObserver(SideScrollGameState sideScrollGameState)
    {
        sidescrollGameObserverLists.ForEach((sidescrollGameObserver) =>
        {
            sidescrollGameObserver.OnSideScrollGameNotify(sideScrollGameState);
        });
    }
}
