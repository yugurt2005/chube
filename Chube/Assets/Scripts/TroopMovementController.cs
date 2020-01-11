using UnityEngine;
using UnityEngine.Tilemaps;

public class TroopMovementController : MonoBehaviour
{
    public float movementSpeed = 1f;

    public Tilemap tilemap;
    Rigidbody2D rbody;
    IsometricCharacterRenderer isoRenderer;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3Int destination = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector3Int origin = tilemap.WorldToCell(transform.position);

            Pathfinder pathfinder = new Pathfinder(null, destination, origin);
<<<<<<< HEAD
            
=======
>>>>>>> troops
        }
    }
}
