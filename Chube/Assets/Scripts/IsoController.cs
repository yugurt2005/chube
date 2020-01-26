using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsoController : MonoBehaviour {
	public Tilemap tilemap;
	public IsoPathfinder isoPathfinder;

	void Start () {
	}

	void FixedUpdate () {
        if (Input.GetButtonDown("Fire1"))
            StartCoroutine(Move(tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition))));
	}

	IEnumerator Move(Vector3Int target) {
		isoPathfinder.originLocation = tilemap.WorldToCell(transform.position);
		isoPathfinder.destinationLocation = tilemap.WorldToCell(target);
		Debug.Log (isoPathfinder.originLocation + " : " + isoPathfinder.destinationLocation);
		IEnumerable<Vector3Int> path = isoPathfinder.BackPropagatePath ();
        

		foreach (Vector3Int location in path) {
			Vector3 miniTarget = tilemap.GetCellCenterWorld(location);
			while (transform.position != miniTarget) {
				transform.position = Vector3.MoveTowards (transform.position, miniTarget, Time.deltaTime);
				yield return new WaitForFixedUpdate();
			}
		}
	}
}
