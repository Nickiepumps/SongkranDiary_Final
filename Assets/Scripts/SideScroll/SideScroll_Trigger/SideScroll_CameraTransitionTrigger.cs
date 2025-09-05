using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_CameraTransitionTrigger : SideScroll_TriggerEvent
{
    [SerializeField] private PlayerSideScrollStateController player;
    [SerializeField] private Transform camFollowTarget;
    [SerializeField] private Transform previousCamPos;
    [SerializeField] private Transform nextCamPos;
    [SerializeField] private float transitionSpeed = 5f;
    private Vector2 direction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Trigger_CameraTransition(player, camFollowTarget, direction, previousCamPos, nextCamPos, transitionSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Trigger_CameraTransition(player, camFollowTarget, direction, previousCamPos, nextCamPos, transitionSpeed * Time.deltaTime);
        }
    }
}
