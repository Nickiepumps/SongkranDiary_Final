using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class MainMenu_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button Properties")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite clickSprite;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Image buttonImage;
    private Color textColor;
    private void OnDisable()
    {
        buttonImage.sprite = normalSprite;
        textColor = buttonText.color;
    }
    private void Start()
    {
        textColor = buttonText.color;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
        buttonText.color = Color.white;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = clickSprite;
        buttonText.color = textColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite;
        buttonText.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
        buttonText.color = textColor;
    }
}
