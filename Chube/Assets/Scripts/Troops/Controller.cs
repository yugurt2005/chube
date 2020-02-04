using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : MonoBehaviour {
	public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;

    public Pathfinder pathfinder;
	public float speed;

    private void Start()
    {
        transform.position = tilemap.CellToWorld(new Vector3Int(1, 0, tilemapRenderer.sortingOrder));
    }

    void Awake () {
		pathfinder = GetComponent<Pathfinder>();
		pathfinder.tilemap = tilemap;
	}
	
	void FixedUpdate () {
        if (Input.GetMouseButtonDown(0))
		{
			StopAllCoroutines();
            StartCoroutine(Move(Camera.main.ScreenToWorldPoint(Input. mousePosition)));
		}
	}

	IEnumerator Move(Vector3 target) {
		pathfinder.destinationLocation = tilemap.WorldToCell(transform.position);
        pathfinder.destinationLocation.z = tilemapRenderer.sortingOrder;
		pathfinder.originLocation = tilemap.WorldToCell(target);
        pathfinder.originLocation.z = tilemapRenderer.sortingOrder;
		IEnumerable<Vector3Int> path = pathfinder.BackPropagatePath ();

		foreach (Vector3Int location in path) {
			Vector3 localTarget = tilemap.GetCellCenterWorld(location);
			while (transform.position != localTarget) {
				transform.position = Vector3.MoveTowards (transform.position, localTarget, Time.deltaTime * speed);
				yield return new WaitForFixedUpdate();
			}
		}
	}
}