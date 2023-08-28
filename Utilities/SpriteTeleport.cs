using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteTeleport : MonoBehaviour
{
    [SerializeField]
    private float boundsXRight = 40f;
    [SerializeField]
    private float boundsXLeft = -40f;
    [SerializeField]
    private float boundsYTop = 40f;
    [SerializeField]
    private float boundsYBottom = -40f;

    private Transform playerTransform;
    private Transform mainCamera;
    private List<Transform> enemyTransforms = new List<Transform>();

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        GetPlayer();
        GetEnemies();
        mainCamera = Camera.main.transform;
    }

    private void GetPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found in the scene.");
        }
    }

    private void GetEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemyTransforms.Add(enemy.transform);
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        TeleportIfOutOfBounds();
    }

    private void TeleportIfOutOfBounds()
    {
        Vector3 playerPreviousPosition = playerTransform.position;

        playerTransform.position = GetTeleportPosition(playerTransform.position);

        Vector3 deltaPosition = playerTransform.position - playerPreviousPosition;

        UpdatePosition(mainCamera, deltaPosition);
        foreach (Transform enemy in enemyTransforms)
        {
            UpdatePosition(enemy, deltaPosition);
        }
    }

    private Vector3 GetTeleportPosition(Vector3 currentPosition)
    {
        float x = currentPosition.x;
        float y = currentPosition.y;

        if (x > boundsXRight) x = boundsXLeft;
        else if (x < boundsXLeft) x = boundsXRight;

        if (y > boundsYTop) y = boundsYBottom;
        else if (y < boundsYBottom) y = boundsYTop;

        return new Vector3(x, y, currentPosition.z);
    }

    private void UpdatePosition(Transform transformToUpdate, Vector3 delta)
    {
        transformToUpdate.position += delta;
    }

    public void OnDrawGizmos()
    {
        DrawBoundaries(Application.isPlaying);
    }

    private void OnDrawGizmosSelected()
    {
        DrawBoundaries(true);
    }

    private void DrawBoundaries(bool forceDraw = false)
    {
        if (!forceDraw && !Application.isPlaying) return;

        Gizmos.color = Color.red;
        Vector3 topLeft = new Vector3(boundsXLeft, boundsYTop, 0);
        Vector3 topRight = new Vector3(boundsXRight, boundsYTop, 0);
        Vector3 bottomLeft = new Vector3(boundsXLeft, boundsYBottom, 0);
        Vector3 bottomRight = new Vector3(boundsXRight, boundsYBottom, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}