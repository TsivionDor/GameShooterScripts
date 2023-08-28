using Unity.VisualScripting;
using UnityEngine;

public class EXPPickup : MonoBehaviour
{
    [SerializeField] private int orbExpValue = 1; 
    [SerializeField] private float magnetRange = 3f; 
    [SerializeField] private float magnetSpeed = 5f; 

    private GameObject player;
    private bool isBeingAttracted = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
      
            float effectiveMagnetRange = magnetRange + player.GetComponent<Player>().GetMagnetRangeBoost();

            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= effectiveMagnetRange)
            {
                isBeingAttracted = true;
            }

            if (isBeingAttracted)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, magnetSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddExperience(orbExpValue);
            Destroy(gameObject);
        }
    }
}

