using UnityEngine;

public class ExpDrop : MonoBehaviour
{
    [SerializeField] private GameObject expPrefab;
    [SerializeField] private int minExp = 1;
    [SerializeField] private int maxExp = 5;

    private void OnDestroy()
    {
        int expAmount = Random.Range(minExp, maxExp);
        for (int i = 0; i < expAmount; i++)
        {
            // Instantiate individual EXP items. You can adjust the position if you need.
            Instantiate(expPrefab, transform.position, Quaternion.identity);
        }
    }
}