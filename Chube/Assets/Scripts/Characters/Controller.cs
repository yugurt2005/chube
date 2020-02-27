using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : MonoBehaviour {
	public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;

    public Pathfinder pathfinder;

    public float speed;

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
                character.position = Vector3.MoveTowards(character.position, localTarget, Time.deltaTime * speed);
				yield return new WaitForFixedUpdate();
			}
		}
	}

    //public IEnumerator Fight(float perception, float range, string layer, float damage)
    //{
    //    for (int x = -1; x <= 1; x++)
    //    {
    //        for (int y = -1; y <= 1; y++)
    //        {
    //            RaycastHit2D hit = Physics2D.Raycast(
    //                (Vector2)transform.position,
    //                new Vector2(x, y),
    //                perception,
    //                LayerMask.GetMask(layer));

    //            if (hit.collider != null)
    //            {
    //                StopAllCoroutines();

    //                Properties properties = hit.collider.gameObject.GetComponent<Properties>();
    //                while (true)
    //                {
    //                    transform.position = Vector3.MoveTowards(transform.position, hit.collider.gameObject.transform.position, speed);

    //                    float distance = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
    //                    if (distance < range)
    //                    {
    //                        properties.health -= damage;
    //                    }
    //                    if (distance > perception)
    //                    {
    //                        break;
    //                    }
    //                    if (properties.health < 0)
    //                    {
    //                        Destroy(hit.collider.gameObject);
    //                        break;
    //                    }

    //                    yield return new WaitForFixedUpdate();
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}
}