<<<<<<< HEAD
﻿using System.Collections.Generic;
=======
﻿/* using System.Collections.Generic;
>>>>>>> troops
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

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

<<<<<<< HEAD
    void Start()
    {
        transform.position = tilemap.CellToWorld(new Vector3Int(0, -1, 0));
    }

=======
>>>>>>> troops
    void Update()
    {
        Vector3Int target = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetButtonDown("Fire1"))
        {
<<<<<<< HEAD
            Vector3Int destination = new Vector3Int(0, 1, 1);
=======
>>>>>>> troops
            Vector3Int origin = tilemap.WorldToCell(transform.position);
            Debug.Log(origin + " : " + destination);

<<<<<<< HEAD
            Pathfinder pathfinder = new Pathfinder(tilemap, destination, origin);
            
=======
            Pathfinder pathfinder = new Pathfinder(tilemap, target, origin);
            Debug.Log("Origin: " + origin + " Destination: " + target + " Path: " + pathfinder.path);

            cartesianPath = pathfinder.path;                
        }
        

        if (cartesianPath.Count > 0)
        {
            transform.position = new Vector2((2 * cartesianPath[0].y + cartesianPath[0].x) / 2, 
                (2 * cartesianPath[0].y - cartesianPath[0].x) / 2);
>>>>>>> troops
        }
    }
}
*/