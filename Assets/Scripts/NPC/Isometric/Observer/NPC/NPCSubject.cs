using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCSubject : MonoBehaviour
{
    private List<INPCObserver> npcObserverLists = new List<INPCObserver>(); // NPC Observer
    public void AddNPCObserver(INPCObserver npcObserver)
    {
        npcObserverLists.Add(npcObserver);
    }
    public void RemoveNPCObserver(INPCObserver npcObserver)
    {
        npcObserverLists.Remove(npcObserver);
    }
    public void NotifyNPCObserver(NPCAction npcAction)
    {
        npcObserverLists.ForEach((npcObserver) =>
        {
            npcObserver.OnNPCNotify(npcAction);
        });
    }
}
