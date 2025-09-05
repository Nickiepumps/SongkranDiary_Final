using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollParallax : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Renderer paralaxMat;
    [SerializeField] private float paralaxXValue, paralaxYValue;
    private void Update()
    {
        paralaxMat.material.mainTextureOffset = new Vector2(cam.position.x * paralaxXValue, cam.position.y * paralaxYValue);
    }
}
