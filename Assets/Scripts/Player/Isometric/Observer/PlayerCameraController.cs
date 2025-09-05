using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour, IPlayerObserver
{
    [Header("Player Observer Reference")]
    [SerializeField] private PlayerSubject player; // Player's observer subject
    private Camera playerCam; // Player's camera

    [Header("Camera targets")]
    [SerializeField] private Transform playerTarget; // Player's transform 
    [SerializeField] private Transform bossLevelTarget; // Player's transform 
    [SerializeField] private Transform runLevelTarget; // Player's transform
    [HideInInspector] public Transform currentFocusTarget;

    [Header("Camera follow properties")]
    [SerializeField] private float maxCamDistX, maxCamDistY, minCamDistX, minCamDistY; // Min/Max camera distance to follow player and not out of bound
    [SerializeField] private float followSpeed = 3f; // Camera follow speed
    private float camZoomInSize = 1.5f;
    private float camZoomOutSize = 3f;
    private float focusTime = 2f;
    private float currentFocusTime;
    private bool zoomIn, zoomOut;
    public bool isFocusNextLevel = false;
    public bool isReachFocusTarget = false;
    private bool startFocusLevel = false;
    private StageClearData stageClearData;
    private void OnEnable()
    {
        playerCam = Camera.main;
        player.AddPlayerObserver(this);
    }
    private void OnDisable()
    {
        player.RemovePlayerObserver(this);
    }
    private void Start()
    {
        /*stageClearData = SideScroll_StageClearDataHandler.instance.LoadSideScrollStageClear();
        if(stageClearData != null)
        {
            CheckMapToFocus(stageClearData.mapName);
        }
        currentFocusTime = focusTime;*/
    }
    private void LateUpdate()
    {
        if (isFocusNextLevel == false)
        {
            if (zoomIn)
            {
                Transform npcTransform = player.GetComponent<PlayerStateController>().currentColHit.gameObject.transform;
                playerCam.orthographicSize = Mathf.Lerp(playerCam.orthographicSize, camZoomInSize, 3f * Time.deltaTime);
                playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, new Vector3(npcTransform.position.x,
                    npcTransform.position.y, playerCam.transform.position.z), followSpeed * Time.deltaTime); // Camera focus on the NPC
            }
            else if (zoomOut)
            {
                playerCam.orthographicSize = Mathf.Lerp(playerCam.orthographicSize, camZoomOutSize, 3f * Time.deltaTime);
                playerCam.transform.position = Vector3.Lerp(playerCam.transform.position,
                new Vector3(playerTarget.position.x, playerTarget.position.y, playerCam.transform.position.z), followSpeed * Time.deltaTime); // Follow player smoothly
                /*playerCam.transform.position = new Vector3(Mathf.Clamp(playerCam.transform.position.x, minCamDistX, maxCamDistX), Mathf.Clamp(playerCam.transform.position.y, minCamDistY, maxCamDistY),
                    playerCam.transform.position.z); // Camera focus on the player*/
            }
        }
        else
        {
            Vector2 movedir = Vector2.MoveTowards(playerCam.transform.position, currentFocusTarget.position, followSpeed * Time.deltaTime);
            if (movedir.x == currentFocusTarget.position.x && movedir.y == currentFocusTarget.position.y)
            {
                isReachFocusTarget = true;
                /*currentFocusTime -= Time.deltaTime;
                if (currentFocusTime <= 0)
                {
                    isFocusNextLevel = false;
                }*/
            }
            else
            {
                isReachFocusTarget = false;
                playerCam.transform.position = new Vector3(movedir.x, movedir.y, playerCam.transform.position.z);
            }
        }
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case (PlayerAction.Idle):
                zoomIn = false;
                zoomOut = true;
                return;
            case (PlayerAction.Talk):
                zoomOut = false;
                zoomIn = true;
                return;
            case (PlayerAction.Walk):
                zoomIn = false;
                zoomOut = true;
                return;
        }
    }
}
