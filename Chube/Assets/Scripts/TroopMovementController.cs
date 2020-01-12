using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TroopMovementController : MonoBehaviour
{
    public float movementSpeed = 1f;

    public Tilemap tilemap;
    Rigidbody2D rbody;
    IsometricCharacterRenderer isoRenderer;

    List<Vector3Int> cartesianPath = new List<Vector3Int>();

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3Int destination = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector3Int origin = tilemap.WorldToCell(transform.position);

            Pathfinder pathfinder = new Pathfinder(tilemap, destination, origin);
            cartesianPath = pathfinder.path;
        }

        if (cartesianPath.Count > 0)
        {
            rbody.position = new Vector2((2 * cartesianPath[0].y + cartesianPath[0].x) / 2, 
                (2 * cartesianPath[0].y - cartesianPath[0].x) / 2);
        }
    }
}
