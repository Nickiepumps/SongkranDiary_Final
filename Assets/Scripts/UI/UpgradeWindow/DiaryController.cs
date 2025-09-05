using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryController : MonoBehaviour
{
    [SerializeField] private GameObject leftWindow;
    [SerializeField] private GameObject rightWindow;
    [SerializeField] private Image buttonImage;
    private RectTransform currentWindowButtonTransform;
    private void OnEnable()
    {
        currentWindowButtonTransform = buttonImage.GetComponent<RectTransform>();
        if (currentWindowButtonTransform.anchoredPosition.y <= -94)
        {
            currentWindowButtonTransform.anchoredPosition = new Vector2(currentWindowButtonTransform.anchoredPosition.x, currentWindowButtonTransform.anchoredPosition.y + 28);
        }
        buttonImage.raycastTarget = false;
        leftWindow.SetActive(true);
        rightWindow.SetActive(true);
    }
    private void OnDisable()
    {
        if(currentWindowButtonTransform.anchoredPosition.y >= -66)
        {
            currentWindowButtonTransform.anchoredPosition = new Vector2(currentWindowButtonTransform.anchoredPosition.x, currentWindowButtonTransform.anchoredPosition.y - 28);
        }
        buttonImage.raycastTarget = true;
        leftWindow.SetActive(false);
        rightWindow.SetActive(false);
    }
}
