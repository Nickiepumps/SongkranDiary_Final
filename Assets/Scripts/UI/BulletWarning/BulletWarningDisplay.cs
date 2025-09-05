using UnityEngine;
using UnityEngine.UI;

public class BulletWarningDisplay : MonoBehaviour
{
    [SerializeField] private Image[] warningSignArr;
    private void OnEnable()
    {
        foreach(Image image in warningSignArr)
        {
            image.enabled = true;
        }
    }
    private void OnDisable()
    {
        foreach (Image image in warningSignArr)
        {
            image.enabled = false;
        }
    }
    public void ShowBulletWarningSign(Transform bulletSpawner, int spawnerIndex)
    {
        Vector2 spawnerViewportPosToScreenPos = Camera.main.WorldToScreenPoint(bulletSpawner.transform.position);

        // Offset image to appear near the screen border
        // Image will appear at correct position on any screen resolution
        if (spawnerViewportPosToScreenPos.y >= Screen.height)
        {
            spawnerViewportPosToScreenPos = new Vector2(spawnerViewportPosToScreenPos.x, Screen.height-80);
        }
        else if (spawnerViewportPosToScreenPos.x < 0)
        {
            spawnerViewportPosToScreenPos = new Vector2(80, spawnerViewportPosToScreenPos.y);
        }
        else if (spawnerViewportPosToScreenPos.x > 0)
        {
            spawnerViewportPosToScreenPos = new Vector2(Screen.width - 80, spawnerViewportPosToScreenPos.y);
        }
        warningSignArr[spawnerIndex].transform.position = spawnerViewportPosToScreenPos;
        warningSignArr[spawnerIndex].gameObject.SetActive(true);
    }
    public void DisableWarningSign()
    {
        for (int i = 0; i < warningSignArr.Length; i++)
        {
            warningSignArr[i].gameObject.SetActive(false);
        }
    }
}
