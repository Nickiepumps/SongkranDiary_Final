using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostcardWindow : MonoBehaviour, IGameObserver
{
    [Header("Observer References")]
    [SerializeField] private GameSubject gameSubject;

    [Header("Postcard Text References")]
    public TMP_Text postcardNameText;
    public TMP_Text levelTypeText;

    [Header("Scene Controller (For Keyboard Only)")]
    [SerializeField] SceneController sceneController;
    public string sceneName;

    [Header("Game UI Controller (For Keyboard Only)")]
    [SerializeField] private GameUIController gameUIController;

    [HideInInspector] public bool isTransitionToNewISOArea = false;
    [HideInInspector] public Transform mapStartPoint;

    private void OnEnable()
    {
        gameSubject.AddGameObserver(this);
    }
    private void OnDisable()
    {
        gameSubject.RemoveGameObserver(this);
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(isTransitionToNewISOArea == false)
            {
                sceneController.ChangeScene(sceneName);
            }
            else
            {
                gameUIController.StartCoroutine(gameUIController.StartTransitionISOScene(mapStartPoint));
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            gameSubject.NotifyGameObserver(IsometricGameState.Play);
        }
    }
}
