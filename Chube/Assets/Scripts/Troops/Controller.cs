using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : MonoBehaviour {
	public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;

    public Pathfinder pathfinder;

    private void Awake()
    {
        pathfinder.tilemap = tilemap;
    }

    public IEnumerator Move(Transform character, Vector3 origin, Vector3 destination) {
		pathfinder.destinationLocation = tilemap.WorldToCell(origin);
        pathfinder.destinationLocation.z = tilemapRenderer.sortingOrder;
		pathfinder.originLocation = tilemap.WorldToCell(destination);
        pathfinder.originLocation.z = tilemapRenderer.sortingOrder;

		IEnumerable<Vector3Int> path = pathfinder.BackPropagatePath ();

		foreach (Vector3Int location in path) {
			Vector3 localTarget = tilemap.GetCellCenterWorld(location);
			while (character.position != localTarget) {
				character.position = Vector3.MoveTowards (character.position, localTarget, Time.deltaTime);
				yield return new WaitForFixedUpdate();
			}
		}
	}
}