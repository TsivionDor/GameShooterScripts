using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Vector3 _movementDirection;
    private float damage;

    public float Damage => damage;

    public void SetDirection(Vector3 direction)
    {
        _movementDirection = direction.normalized;
    }

    private void Update()
    {
        MoveBullet();
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    private void MoveBullet()
    {
        transform.Translate(_movementDirection * Time.deltaTime * bulletSpeed, Space.World);
    }

    public void SetRotation(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}