using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlbumImage", menuName = "Album")]
public class AlbumSO : ScriptableObject
{
    public int imageNumber;
    public string imageName;
    public string unlockImageText;
    public string lockImageText;
    [TextArea] public string imageDescription;
    public Sprite image;
}
