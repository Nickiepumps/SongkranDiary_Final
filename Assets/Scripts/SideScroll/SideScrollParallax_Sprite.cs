using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollParallax_Sprite : MonoBehaviour
{
    [Header("Camera and sprite parent references")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform mainSpriteTransform; // Sprite or Sprite parent's transform
    [Header("Sprite scroll properties")]
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] private float connectOffset = 0f; // Sprite position offset to connect the other sprite seamlessly
    [SerializeField] private Vector2 scrollPosOffset = Vector2.zero; // Sprite scroll offset position
    [Space]
    [Header("Midground scroll properties")]
    [SerializeField] private bool isMidGround = false;
    private bool isLeft;
    private int currentSprite = 0;
    private int childAmount;
    private void Start()
    {
        childAmount = mainSpriteTransform.childCount - 1;
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") >= 1)
        {
            isLeft = false;
        }
        else if (Input.GetAxisRaw("Horizontal") <= -1)
        {
            isLeft = true;
        }

        if (isMidGround == false)
        {
            SpriteScroll();
        }
        else
        {
            MidGroundScroll();
        }
    }
    private void MidGroundScroll()
    {
        Vector2 spriteCurrentPos = Camera.main.WorldToViewportPoint(mainSpriteTransform.GetChild(currentSprite).position);
        if (spriteCurrentPos.x <= 0 && isLeft == false)
        {
            mainSpriteTransform.GetChild(currentSprite).transform.position = new Vector2(mainSpriteTransform.GetChild(childAmount - currentSprite).transform.position.x + connectOffset,
                mainSpriteTransform.GetChild(currentSprite).transform.position.y);
            if (currentSprite >= childAmount)
            {
                currentSprite = 0;
            }
            else
            {
                currentSprite++;
            }

        }
        else if (spriteCurrentPos.x >= 1 && isLeft == true)
        {
            mainSpriteTransform.GetChild(childAmount - currentSprite).transform.position = new Vector2(mainSpriteTransform.GetChild(currentSprite).transform.position.x - connectOffset,
                mainSpriteTransform.GetChild(childAmount).transform.position.y);
            if (currentSprite <= 0)
            {
                currentSprite = childAmount;
            }
            else
            {
                currentSprite--;
            }
        }
    }
    private void SpriteScroll()
    {
        Vector2 scrollDir = new Vector2(cameraTransform.position.x, cameraTransform.position.y) * scrollSpeed;

        if (isLeft == true)
        {
            mainSpriteTransform.position = -scrollDir - scrollPosOffset;
        }
        else
        {
            mainSpriteTransform.position = new Vector2(-scrollDir.x - scrollPosOffset.x, -scrollDir.y - scrollPosOffset.y);
        }

        Vector2 spriteCurrentPos = Camera.main.WorldToViewportPoint(mainSpriteTransform.GetChild(currentSprite).position);
        if (spriteCurrentPos.x <= 0 && isLeft == false)
        {
            if (currentSprite == 0)
            {
                mainSpriteTransform.GetChild(currentSprite).position = new Vector2(mainSpriteTransform.GetChild(currentSprite + 1).position.x + connectOffset,
                    mainSpriteTransform.GetChild(currentSprite).position.y);
                currentSprite++;
            }
            else
            {
                mainSpriteTransform.GetChild(currentSprite).position = new Vector2(mainSpriteTransform.GetChild(currentSprite - 1).position.x + connectOffset,
                    mainSpriteTransform.GetChild(currentSprite).position.y);
                currentSprite--;
            }
        }
        else if (spriteCurrentPos.x >= 1 && isLeft == true)
        {
            if (currentSprite == 0)
            {
                mainSpriteTransform.GetChild(currentSprite + 1).position = new Vector2(mainSpriteTransform.GetChild(currentSprite).position.x - connectOffset,
                    mainSpriteTransform.GetChild(currentSprite).position.y);
                currentSprite++;
            }
            else
            {
                mainSpriteTransform.GetChild(currentSprite - 1).position = new Vector2(mainSpriteTransform.GetChild(currentSprite).position.x - connectOffset,
                    mainSpriteTransform.GetChild(currentSprite).position.y);
                currentSprite--;
            }
        }
    }
}
