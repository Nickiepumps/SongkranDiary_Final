using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SideScroll_PlayerCamera : MonoBehaviour, IPlayerObserver
{
    [Header("Observer Reference")]
    [SerializeField] private PlayerSubject playerSubject;

    [Header("Camera Target Properties")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float followSpeed;
    [SerializeField] private float transitionSpeed = 3f;
    public Transform minCamDistX, minCamDistY, maxCamDistX, height1CamDistY, height2CamDistY, midCamY;
    [HideInInspector] public Transform camYTarget;
    private bool isCamShake = false;

    [Header("Camera Shake Properties")]
    [SerializeField] private Transform cameraParent;
    [SerializeField] private float minShake;
    [SerializeField] private float maxShake;
    private Camera playerCam;

    [Header("Post Processing")]
    [SerializeField] private Volume blurVolumeObject;
    [SerializeField] private VolumeProfile blurProfile;
    private DepthOfField depthOfField;
    private void OnEnable()
    {
        playerSubject.AddPlayerObserver(this);   
    }
    private void OnDisable()
    {
        playerSubject.RemovePlayerObserver(this);
    }
    private void Start()
    {
        playerCam = Camera.main;
        blurVolumeObject.profile.TryGet<DepthOfField>(out depthOfField);
        camYTarget = midCamY.transform;
    }
    private void FixedUpdate()
    {
        if (isCamShake == true)
        {
            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position,
                new Vector3(playerTarget.position.x, camYTarget.position.y, playerCam.transform.position.z), followSpeed * Time.fixedDeltaTime); // Follow player smoothly
            playerCam.transform.position = new Vector3(Mathf.Clamp(playerCam.transform.position.x, minCamDistX.transform.position.x, maxCamDistX.transform.position.x),
                camYTarget.transform.position.y, playerCam.transform.position.z); // Camera focus on the player
        }
        
    }
    private void LateUpdate()
    {
        if(isCamShake == false)
        {
            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position,
                new Vector3(playerTarget.position.x, camYTarget.position.y, playerCam.transform.position.z), followSpeed * Time.deltaTime); // Follow player smoothly
            playerCam.transform.position = new Vector3(Mathf.Clamp(playerCam.transform.position.x, minCamDistX.transform.position.x, maxCamDistX.transform.position.x),
                camYTarget.transform.position.y, playerCam.transform.position.z); // Camera focus on the player
        }
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch(playerAction)
        {
            case (PlayerAction.Damaged):
                StopAllCoroutines();
                StartCoroutine(CameraShake(1f, minShake, maxShake));
                return;
            case(PlayerAction.Blind):
                StopAllCoroutines();
                StartCoroutine(CameraShake(1f, minShake, maxShake));
                StartCoroutine(Blur());
                return;
            case (PlayerAction.Dead):
                StopAllCoroutines();
                StartCoroutine(CameraShake(1f, minShake, maxShake));
                return;
        }
    }
    private IEnumerator CameraShake(float shakeDuration, float minShake, float maxShake)
    {
        isCamShake = true;
        float elapsed = 0f;
        float currentMagnitude = 1f;
        while (elapsed < shakeDuration)
        {
            float x = (Random.Range(minShake, maxShake)) * currentMagnitude;
            float y = (Random.Range(minShake, maxShake)) * currentMagnitude;
            cameraParent.localPosition = new Vector3(x, y, cameraParent.localPosition.z);
            elapsed += Time.deltaTime;
            currentMagnitude = (1 - (elapsed / shakeDuration)) * (1 - (elapsed / shakeDuration));
            yield return null;
        }
        cameraParent.localPosition = new Vector3(0,0,cameraParent.localPosition.z);
        isCamShake = false;
    }
    private IEnumerator Blur()
    {
        depthOfField.focalLength.value = 300;
        while (depthOfField.focalLength.value > 10)
        {
            depthOfField.focalLength.value = Mathf.Lerp(depthOfField.focalLength.value, 10, 0.5f * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }
}
