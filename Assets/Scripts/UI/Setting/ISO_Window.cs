using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ISO_Window : MonoBehaviour
{
    [Header("Game UI Controller Reference")]
    public GameUIController gameUIController;
    [Header("Switch Animation Reference")]
    public Animation uiAnimation;
    public IEnumerator ISOWindowTransition(GameObject currentWindow, GameObject targetWindow)
    {
        uiAnimation.Play("CloseMenu");
        yield return new WaitUntil(() => uiAnimation.isPlaying == false);
        currentWindow.SetActive(false);
        targetWindow.SetActive(true);
    }
}
