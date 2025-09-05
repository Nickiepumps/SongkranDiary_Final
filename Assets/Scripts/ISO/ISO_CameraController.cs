using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISO_CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTarget; // Player's transform 
    [SerializeField] private float maxCamDistX, maxCamDistY, minCamDistX, minCamDistY; // Min/Max camera distance to follow player and not out of bound
    [SerializeField] private float followSpeed = 3f;
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, 
            new Vector3(playerTarget.position.x,playerTarget.position.y,transform.position.z), followSpeed * Time.deltaTime); // Follow player smoothly
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamDistX, maxCamDistX), Mathf.Clamp(transform.position.y, minCamDistY, maxCamDistY),
            transform.position.z);
    }
}
