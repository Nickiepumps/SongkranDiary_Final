using System.Collections.Generic;

[System.Serializable]
public class AlbumData
{
    public List<AlbumSO> albumImagesSO = new List<AlbumSO>();
    public List<bool> imageUnlockStatus = new List<bool>();
}
