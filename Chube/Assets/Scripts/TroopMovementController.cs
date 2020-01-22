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

    void Start()
    {
        transform.position = tilemap.CellToWorld(new Vector3Int(0, -1, 0));
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3Int destination = new Vector3Int(0, 1, 1);
            Vector3Int origin = tilemap.WorldToCell(transform.position);
            Debug.Log(origin + " : " + destination);

            Pathfinder pathfinder = new Pathfinder(tilemap, destination, origin);
            
        }
    }
}
