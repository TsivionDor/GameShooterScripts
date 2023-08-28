using UnityEngine;

public class FollowPlayerMovement : IMovementBehavior
{
    private Transform playerTransform;

    public FollowPlayerMovement(Transform _playerTransform)
    {
        playerTransform = _playerTransform;
    }

    public void Move(Transform enemyTransform, float speed)
    {
        Vector3 direction = (playerTransform.position - enemyTransform.position).normalized;
        enemyTransform.position += direction * speed * Time.deltaTime;
    }
}