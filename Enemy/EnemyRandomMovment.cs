using UnityEngine;

public class EnemyRandomMovment : IMovementBehavior
{
    private Vector3 targetPosition;
    private bool needsNewTarget = true;

    public void Move(Transform enemyTransform, float speed)
    {
        if (needsNewTarget)
        {
            targetPosition = GetRandomPositionOnScreen(enemyTransform);
            needsNewTarget = false;
        }

        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(enemyTransform.position, targetPosition) < 0.1f)
        {
            needsNewTarget = true;
        }
    }

    private Vector3 GetRandomPositionOnScreen(Transform enemyTransform)
    {
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        float randomX = Random.Range(screenBottomLeft.x, screenTopRight.x);
        float randomY = Random.Range(screenBottomLeft.y, screenTopRight.y);

        return new Vector3(randomX, randomY, enemyTransform.position.z);
    }
}