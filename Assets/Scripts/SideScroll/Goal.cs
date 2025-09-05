using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : GameSubject
{
    [SerializeField] private GameType gameType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && gameType == GameType.RunNGun)
        {
            NotifySideScrollGameObserver(SideScrollGameState.WinRunNGun);
        }
        else if(collision.tag == "Player" && gameType == GameType.Tutorial)
        {
            NotifySideScrollGameObserver(SideScrollGameState.FinishTutorial);
        }
    }
}
