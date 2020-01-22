using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsoController : MonoBehaviour {
	[SerializeField]
	public Tilemap tilemap;
	IsoPathfinder isoPathfinder;

	void Start () {
		isoPathfinder = new IsoPathfinder(tilemap);
	}

	void FixedUpdate () {
		if (Input.GetMouseButtonUp(0))
			StartCoroutine (Move());
		if (Input.GetMouseButtonUp (1))
			StopCoroutine (Move ());
	}

	IEnumerator Move () {
		isoPathfinder.originLocation = new Vector3Int(2, 5, 0); //SHOULD BE THE DESTINATION FROM MOUSE CLICK :(
		isoPathfinder.destinationLocation = tilemap.WorldToCell (transform.position);
		Debug.Log (isoPathfinder.originLocation + " : " + isoPathfinder.destinationLocation);
		IEnumerable<Vector3Int> path = isoPathfinder.BackPropagatePath ();
		foreach (Vector3Int location in path) {
			Vector3 target = tilemap.CellToWorld (location);
			while (Vector3.Distance(transform.position, target) > 0.075f) {
				transform.position = Vector3.MoveTowards (transform.position, target, Time.deltaTime);
				yield return new WaitForFixedUpdate();
			}
		}
	}
}
