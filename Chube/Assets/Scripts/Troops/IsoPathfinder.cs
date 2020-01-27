using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


// SEE THE BOTTOM OF THE SCRIPT

public class State {
	public Vector3Int position;

	public State parent;

	// gCost = distance (start -> location)
	// hCost = heuristic (location -> destination)
	public float gCost, hCost;
	public float fCost {
		get { return gCost + hCost; }
	}
}

public class IsoPathfinder : MonoBehaviour {
	Tilemap tilemap;

	public Vector3Int originLocation;
	public Vector3Int destinationLocation;

	public IsoPathfinder(Tilemap _tilemap) {
		tilemap = _tilemap;
	}

	public IEnumerable<Vector3Int> BackPropagatePath () {
		State state = ForwardPropagatePath ();
		while (state != null) {
			yield return state.position;
			Debug.Log (state.position);
			state = state.parent;
		}
	}

	public State ForwardPropagatePath () {
		State start = new State ();
		start.position = originLocation;
		start.gCost = 0f;
		start.hCost = 0f;

		List<State> open = new List<State> () { start };
		List<State> closed = new List<State> () { };

		int iterations = 0;
		while(open.Count > 0 && iterations <= 100) {
			State root = open.Aggregate(open[0], (optimal, next) => next.fCost < optimal.fCost ? next : optimal);

			open.Remove (root);
			closed.Add (root);

			for (int deltaX = -1; deltaX <= 1; deltaX++) {
				for (int deltaY = -1; deltaY <= 1; deltaY++) {
					State branch = new State ();
					branch.position = root.position - new Vector3Int (deltaX, deltaY, 0);
					branch.gCost = (branch.position - root.position).magnitude + root.gCost;
					branch.hCost = (branch.position - destinationLocation).magnitude;
					branch.parent = root;

					if (branch.position == destinationLocation)
						return branch;

					try {
						if (deltaX == 0 && deltaY == 0)
							continue;
						if (tilemap.GetTile(branch.position).name != "BuildingTile")
							continue;
						if (open.Where (state => state.position == branch.position).Select(state => state.fCost).Max() > branch.fCost)
							continue;
						if (closed.Where (state => state.position == branch.position).Select (state => state.fCost).Max () > branch.fCost)
							continue;
					} catch { }

					open.Add (branch);
				}
			}

			iterations++;
		}

		return null;
	}

    /*
    public Vector3Int[] getPath() {
        if it's possible,
        returns a Vector3Int[] containing all the positions in the path
        so then it'd be really nice to implement into scripts like IsometricMovement
        and we won't need a buncha extra stuff
    }
    */
}
