using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public enum Direction
    {
        Up, Down, Left, Right, TopRight, TopLeft, BottomRight, BottomLeft
    }

    [System.Serializable]
    public class AreaConnection
    {
        public Direction direction;
        public GameObject connectedAreaPrefab;
    }

    [SerializeField] private GameObject startingAreaPrefab;
    [SerializeField] private float maxDistanceToKeep = 3f; // Adjust based on your needs

    private Transform playerTransform;
    private GameObject currentArea;
    private List<GameObject> activeAreas = new List<GameObject>();

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Check if player's transform is found
        if (playerTransform == null)
        {
            Debug.LogError("Player transform not found!");
            return;
        }

        // Check if starting area prefab is assigned
        if (startingAreaPrefab == null)
        {
            Debug.LogError("Starting area prefab is not assigned!");
            return;
        }

        currentArea = Instantiate(startingAreaPrefab, playerTransform.position, Quaternion.identity);

        // Check if starting area was successfully instantiated
        if (currentArea == null)
        {
            Debug.LogError("Failed to instantiate starting area!");
            return;
        }

        activeAreas.Add(currentArea);
        LoadSurroundingAreas(currentArea);

        Debug.Log("Starting area instantiated successfully.");
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.down, Mathf.Infinity);

        if (hit.collider != null && hit.collider.gameObject.tag == "Area") // Check the tag here
        {
            if (hit.collider.gameObject != currentArea)
            {
                currentArea = hit.collider.gameObject;
                UpdateSurroundingAreas();
            }
        }
    }
    private void UpdateSurroundingAreas()
    {
        if (currentArea == null)
        {
            Debug.LogError("currentArea is null in UpdateSurroundingAreas!");
            return;
        }

        AreaData areaData = currentArea.GetComponent<AreaData>();

        if (areaData == null)
        {
            Debug.LogError("AreaData component not found on currentArea: " + currentArea.name);
            return; // Exit early since we can't proceed without AreaData
        }

        List<Vector3> requiredPositions = new List<Vector3>
    {
        currentArea.transform.position // Ensure current position is part of required positions.
    };

        foreach (Direction direction in System.Enum.GetValues(typeof(Direction)))
        {
            requiredPositions.Add(GetEndPoint(currentArea, direction));
        }

        // Remove outdated areas
        foreach (var area in new List<GameObject>(activeAreas))
        {
            if (!requiredPositions.Contains(area.transform.position))
            {
                activeAreas.Remove(area);
                Destroy(area);
            }
        }

        // Add missing areas
        foreach (var position in requiredPositions)
        {
            if (!AreaExistsAt(position))
            {
                Direction direction = GetDirectionFromOffset(currentArea.transform.position, position);

                GameObject prefabToInstantiate = areaData.connections.Find(x => x.direction == direction)?.connectedAreaPrefab; // use existing areaData variable

                if (prefabToInstantiate == null)
                {
                    Debug.LogError("No prefab found for direction " + direction + " on currentArea: " + currentArea.name);
                    continue;
                }

                GameObject newArea = Instantiate(prefabToInstantiate, position, Quaternion.identity);
                activeAreas.Add(newArea);
            }
        }

        // Add missing areas
        foreach (var position in requiredPositions)
        {
            if (!AreaExistsAt(position))
            {
                Direction direction = GetDirectionFromOffset(currentArea.transform.position, position);

                // using existing areaData variable
                GameObject prefabToInstantiate = areaData.connections.Find(x => x.direction == direction)?.connectedAreaPrefab;

                if (prefabToInstantiate == null)
                {
                    Debug.LogError("No prefab found for direction " + direction + " on currentArea: " + currentArea.name);
                    continue;
                }

                GameObject newArea = Instantiate(prefabToInstantiate, position, Quaternion.identity);
                activeAreas.Add(newArea);
            }
        }
    }

    private Direction GetDirectionFromOffset(Vector3 from, Vector3 to)
    {
        Vector3 offset = to - from;
        if (offset == Vector3.up) return Direction.Up;
        if (offset == Vector3.down) return Direction.Down;
        if (offset == Vector3.left) return Direction.Left;
        if (offset == Vector3.right) return Direction.Right;
        if (offset == Vector3.up + Vector3.right) return Direction.TopRight;
        if (offset == Vector3.up + Vector3.left) return Direction.TopLeft;
        if (offset == Vector3.down + Vector3.right) return Direction.BottomRight;
        if (offset == Vector3.down + Vector3.left) return Direction.BottomLeft;
        return Direction.Up;  // Default case, but ideally should never hit.
    }
    private void LoadSurroundingAreas(GameObject centerArea)
    {
        AreaData areaData = centerArea.GetComponent<AreaData>();
        if (areaData)
        {
            foreach (var connection in areaData.connections)
            {
                Vector3 position = GetEndPoint(centerArea, connection.direction);

                // Improved check for existing areas
                if (!AreaExistsAt(position))
                {
                    GameObject newArea = Instantiate(connection.connectedAreaPrefab, position, Quaternion.identity);
                    activeAreas.Add(newArea);
                }
            }
        }
    }
    private bool AreaExistsAt(Vector3 position)
    {
        Collider2D hit = Physics2D.OverlapPoint(position);
        return hit != null;
    }

    private Vector3 GetEndPoint(GameObject area, Direction direction)
    {
        Bounds bounds = area.GetComponent<SpriteRenderer>().bounds;
        float height = bounds.size.y;
        float width = bounds.size.x;
        float buffer = 0.5f;  // Adjust this value if needed

        switch (direction)
        {
            case Direction.Up:
                return area.transform.position + Vector3.up * (height - buffer);
            case Direction.Down:
                return area.transform.position - Vector3.up * (height - buffer);
            case Direction.Left:
                return area.transform.position - Vector3.right * (width - buffer);
            case Direction.Right:
                return area.transform.position + Vector3.right * (width - buffer);
            case Direction.TopRight:
                return area.transform.position + Vector3.right * (width - buffer) + Vector3.up * (height - buffer);
            case Direction.TopLeft:
                return area.transform.position - Vector3.right * (width - buffer) + Vector3.up * (height - buffer);
            case Direction.BottomRight:
                return area.transform.position + Vector3.right * (width - buffer) - Vector3.up * (height - buffer);
            case Direction.BottomLeft:
                return area.transform.position - Vector3.right * (width - buffer) - Vector3.up * (height - buffer);
            default:
                return Vector3.zero;
        }
    }
}