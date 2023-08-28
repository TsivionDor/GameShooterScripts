using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Damage Configuration")]
    [SerializeField] public int minPlayerDamage = 10;
    [SerializeField] public int maxPlayerDamage = 20;
    

    public enum EnemyType
    {
        RandomMovement,
        FollowPlayer
    }





    public EnemyType enemyType; // Set this in the Unity Editor for each prefab

    private EnemyBase enemyBase;
    private IMovementBehavior movementBehavior;
    private static Transform playerTransform; // cached player transform

  
    private void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();

        // Cache the player's transform if it hasn't been already
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        switch (enemyType)
        {
            case EnemyType.FollowPlayer:
                movementBehavior = new FollowPlayerMovement(playerTransform);
                break;

            case EnemyType.RandomMovement:
            default:
                movementBehavior = new EnemyRandomMovment(); // corrected the name here
                break;
        }
    }

    private void Update()
    {
        movementBehavior.Move(transform, enemyBase.moveSpeed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet)
            {
                enemyBase.TakeDamage(bullet.Damage); // Assuming GetDamage() is a method in the Bullet script
                
            }
        }
    }

}