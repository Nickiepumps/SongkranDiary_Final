using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISO_PauseWindow : ISO_Window
{
    [Header("Isometric Pause menu")]
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject pauseMenu;
    private void OnEnable()
    {
        //transform.eulerAngles = new Vector3(0, -90, 0);
        uiAnimation.Play("OpenMenu");
    }
    private void Update()
    {
        // Close isometric pause window
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(false);
            if(gameUIController.isNPCTalking == false)
            {
                gameUIController.gameObject.GetComponent<IsometricGameObserverContoller>().NotifyGameObserver(IsometricGameState.Play);
            }
        }
    }
    public void ToggleWindow(GameObject targetWindow)
    {
        StartCoroutine(ISOWindowTransition(gameObject, targetWindow));
    }
}
