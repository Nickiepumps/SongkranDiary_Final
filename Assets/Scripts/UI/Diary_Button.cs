using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Diary_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    [Header("Game UI Controller Reference")]
    [SerializeField] private GameUIController gameUIController;

    [Header("Button Properties")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite clickSprite;
    private Image buttonImage;
    private RectTransform buttonTransform;

    [Header("Diary window changing properties")]
    [SerializeField] private GameObject targetWindow;
    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonTransform = GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(buttonTransform.position.y != -94)
        {
            buttonTransform.anchoredPosition = new Vector2(buttonTransform.anchoredPosition.x, -66);
        }
        buttonImage.sprite = hoverSprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        buttonImage.sprite = clickSprite;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
        gameUIController.diaryAudioPlayer.clip = gameUIController.diaryAudioClipArr[0];
        gameUIController.diaryAudioPlayer.Play();
        OpenDiaryWindow();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonTransform.position.y != -66 && buttonImage.raycastTarget == true)
        {
            buttonTransform.anchoredPosition = new Vector2(buttonTransform.anchoredPosition.x, -94);
        }
        buttonImage.sprite = normalSprite;
    }
    public void OpenDiaryWindow()
    {
        gameUIController.OpenDiaryWindow();
        targetWindow.SetActive(true);
    }
}
