using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1.0f;
    [SerializeField] private float health = 1f;
    [SerializeField] private int minExpValue = 5;  // Minimum EXP dropped by the enemy.
    [SerializeField] private int maxExpValue = 15; // Maximum EXP dropped by the enemy.
    [SerializeField] private GameObject expOrbPrefab; // This is the individual EXP orb prefab.

    private int totalExpValue;

    private void Start()
    {

        totalExpValue = Random.Range(minExpValue, maxExpValue + 1);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }
    public void ApplyKnockback(float knockbackModifier, Vector3 playerPosition)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 knockbackDirection = (transform.position - playerPosition).normalized;
            rb.AddForce(knockbackDirection * knockbackModifier, ForceMode2D.Impulse);
        }
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    protected virtual void Die()
    {
        DropExpOrbs();
        Destroy(gameObject);
    }

    private void DropExpOrbs()
    {
        int orbsToDrop = totalExpValue;  // Assume 1 EXP per orb for simplicity.
        for (int i = 0; i < orbsToDrop; i++)
        {
            Instantiate(expOrbPrefab, transform.position, Quaternion.identity);
        }
    }
}