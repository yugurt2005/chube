using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

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

public class Pathfinder : MonoBehaviour {
	public Tilemap tilemap;

    // NOTE: THIS IS TEMPORARY, just so that it can walk on highlighted and currently being built tiles. Later we can use an array lol

    public TileBase[] walkableTiles = new TileBase[4];
    
	public Vector3Int originLocation;
	public Vector3Int destinationLocation;

	public IEnumerable<Vector3Int> BackPropagatePath () {
		State state = ForwardPropagatePath ();
		while (state != null) {
			yield return state.position;
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
		while(open.Count > 0 && iterations <= 1000) {
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

					if (Mathf.Abs(deltaX) == Mathf.Abs(deltaY))
						continue;


                    TileBase branchTile = tilemap.GetTile(branch.position);
                    bool walkable = true;
                    foreach (TileBase tile in walkableTiles)
                    {
                        if (branchTile == tile)
                        {
                            walkable = false;
                            break;
                        }
                        
                    }
                    if (walkable) continue;

                    List<State> states = new List<State>();
					states.AddRange(open);
					states.AddRange(closed);					
					IEnumerable<float> optimal = 
						states.Where(
						state => branch.position == state.position).Select(
						state => state.fCost);
					if ((optimal.Count() > 1 ? optimal.Min() : float.MaxValue) <= branch.fCost)
						continue;
										
					if (branch.position == destinationLocation)
						return branch;
					
					open.Add (branch);
				}
			}
            iterations++;
		}
		return null;
	}
}