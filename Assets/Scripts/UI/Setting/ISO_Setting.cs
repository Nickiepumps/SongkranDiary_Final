using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISO_Setting : ISO_Window
{
    [Header("Isometric Setting Menu")]
    [SerializeField] private GameObject isoSettingMenu;
    [SerializeField] private GameObject previousMenu;
    private void OnEnable()
    {
        uiAnimation.Play("OpenMenu");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ISOWindowTransition(gameObject, previousMenu));
        }
    }
    public void ToggleWindow(GameObject targetWindow)
    {
        StartCoroutine(ISOWindowTransition(gameObject, targetWindow));
    }
}
