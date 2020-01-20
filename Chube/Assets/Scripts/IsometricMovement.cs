using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class IsometricMovement : MonoBehaviour
{
    public float speed = 0.5f;

    public bool troopsOn = false;
    private Vector3Int previousTile;

    public Button troopButton;
    public Tilemap tilemap;
    public Tile editTile;
    public Tile neutralTile;
    public GameObject buildMode;
    public GameObject buildCursor;
    public Chube chube;

    // Start is called before the first frame update
    void Start()
    {
        troopButton.onClick.AddListener(onButtonPress);
        transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int target = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (troopsOn && tilemap.HasTile(target))
        {    
            if (Input.GetButtonDown("Fire1"))
            {
                // IMPLEMENT PATHFINDING HERE
                transform.position = tilemap.GetCellCenterWorld(target);
            }

            if (tilemap.GetTile(target) == neutralTile)
            {
                tilemap.SetTile(target, editTile);

                if (previousTile != target)
                {
                    tilemap.SetTile(previousTile, neutralTile);
                    previousTile = target;
                }
            }
        }
    }

    // For future use
    Vector3 cartesianToIsometric(Vector3 coords)
    {
        Vector3 isometric = new Vector3(0, 0, 0);
        isometric.x = (coords.x - coords.y) * 1.25f;
        isometric.y = ((coords.x + coords.y) / 2) * 1.25f;

        return isometric;
    }

    void onButtonPress()
    {
        troopsOn = !troopsOn;

        if (troopsOn)
        {
            chube.turnOff();
            chube.switchedMode = !chube.switchedMode;
        }
        else resetTile();
    }

    public void resetTile()
    {
        tilemap.SetTile(previousTile, neutralTile);
    }
}
