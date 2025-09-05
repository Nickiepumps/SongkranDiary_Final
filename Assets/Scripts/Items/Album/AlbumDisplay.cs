using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlbumDisplay : MonoBehaviour
{
    public AlbumSO albumImageSO;
    public TMP_Text imageText;
    public Image image;
    public Button fullImageButton;
    public bool isUnlock = false;
    private void OnEnable()
    {
        if(isUnlock == true)
        {
            image.sprite = albumImageSO.image;
            imageText.text = albumImageSO.unlockImageText;
            fullImageButton.gameObject.SetActive(true);
        }
        else
        {
            imageText.text = albumImageSO.lockImageText;
            fullImageButton.gameObject.SetActive(false);
        }
    }
}
