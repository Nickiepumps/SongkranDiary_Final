using UnityEngine;
public abstract class SideScroll_TriggerEvent : MonoBehaviour
{
    /// <summary>
    /// Start bullet raining when the player enter this trigger
    /// </summary>
    /// <param name="controller"></param>
    public void Trigger_RainingBullet(IncomingBulletController controller, bool rainingStatus, int startPatternRange, int endPatternRange, float newSpawnTime)
    {
        if(rainingStatus == true)
        {
            controller.enabled = true;
            controller.startRainingBullet = true;
            controller.startPatternRange = startPatternRange;
            controller.endPatternRange = endPatternRange;
            controller.fireCooldown = newSpawnTime;
        }
        else
        {
            controller.bulletwarningDisplay.DisableWarningSign();
            controller.enabled = false;
            controller.startRainingBullet = false;
        }
    }

    /// <summary>
    /// Spawn enemy base on enemy type when the player enter this trigger
    /// </summary>
    /// <param name="normalEnemyType"></param>
    /// <param name="controller"></param>
    public void Trigger_StartSpawnEnemy(NormalEnemyType normalEnemyType, NormalEnemySpawnerController controller)
    {
        switch (normalEnemyType)
        {
            case (NormalEnemyType.Shooter):
                controller.spawnShooter = true;
                break;
            case (NormalEnemyType.Bomber):
                controller.spawnBomber = true;
                break;
            case (NormalEnemyType.Drone):
                controller.spawnDrone = true;
                break;
        }
    }

    /// <summary>
    /// Stop spawning enemy base on enemy type when the player enter this trigger
    /// </summary>
    /// <param name="normalEnemyType"></param>
    /// <param name="controller"></param>
    public void Trigger_StopSpawnEnemy(NormalEnemyType normalEnemyType, NormalEnemySpawnerController controller)
    {
        switch (normalEnemyType)
        {
            case (NormalEnemyType.Shooter):
                controller.spawnShooter = false;
                break;
            case (NormalEnemyType.Bomber):
                controller.spawnBomber = false;
                break;
            case (NormalEnemyType.Drone):
                controller.spawnDrone = false;
                break;
        }
    }

    /// <summary>
    /// Stop spawning all type enemy when the player enter this trigger
    /// </summary>
    /// <param name="controller"></param>
    public void Trigger_StopSpawnEnemy(NormalEnemySpawnerController controller)
    {
        controller.spawnShooter = false;
        controller.spawnBomber = false;
        controller.spawnDrone = false;
    }

    /// <summary>
    /// Use for camera transition from point A to Point B in vertical axis
    /// </summary>
    /// <param name="player"></param>
    /// <param name="camFollowTarget"></param>
    /// <param name="virtualDirection"></param>
    /// <param name="previousCamPosition"></param>
    /// <param name="nextCamPosition"></param>
    /// <param name="transitionSpeed"></param>
    public void Trigger_CameraTransition(PlayerSideScrollStateController player, Transform camFollowTarget, Vector2 virtualDirection, Transform previousCamPosition, Transform nextCamPosition, float transitionSpeed)
    {
        Vector2 velRef = Vector2.zero;
        if(player.xDir > 0)
        {
            virtualDirection = Vector2.MoveTowards(new Vector2(0, camFollowTarget.position.y), new Vector2(0, nextCamPosition.position.y), transitionSpeed);
            camFollowTarget.position = new Vector2(camFollowTarget.position.x, virtualDirection.y);
        }
        else if(player.xDir < 0)
        {
            virtualDirection = Vector2.MoveTowards(new Vector2(0, camFollowTarget.position.y), new Vector2(0, previousCamPosition.position.y), transitionSpeed);
            camFollowTarget.position = new Vector2(camFollowTarget.position.x, virtualDirection.y);
        }
    }
    public void Trigger_Vacuum(Rigidbody2D rb, float vacuumValue, Vector2 vacuumDirection)
    {
        if(rb != null)
        {
            rb.GetComponent<PlayerSideScrollStateController>().externalPullVelocity = vacuumDirection * vacuumValue;
        }
    }
}
