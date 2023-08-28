using UnityEngine;

public interface IMovementBehavior
{
    void Move(Transform enemyTransform, float speed);
}
