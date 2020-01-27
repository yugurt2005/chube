using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

// This is the basic code for movement: go to where it says "IMPLEMENT PATHFINDING HERE" to fill in animation for movement
// (right now it just teleports there)

public class IsometricMovement : MonoBehaviour
{
    // TODO:
    // - check if spot is available (no obstacle or other troops there)
    // - change what spots you can move to based on animal (like chess)

    public float speed = 0.5f;
    public bool troopsOn = false;

    private Vector3Int previousTile;
    private bool firstTouch = false;

    public Button troopButton;
    public Tilemap tilemap;
    public Tile editTile;
    public Tile neutralTile;
    public TilemapRenderer tilemapRenderer;
    public GameObject buildMode;
    public GameObject buildCursor;
    public Chube chube;

    public IsoPathfinder pathfinder;

    void Start()
    {
        troopButton.onClick.AddListener(onButtonPress);
        transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position));
    }

    void Update()
    {
        Vector3Int mouseTile = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseTile.z = tilemapRenderer.sortingOrder;

        if (troopsOn && tilemap.HasTile(mouseTile))
        {          
            if (Input.GetButtonDown("Fire1"))
            {
                // IMPLEMENT PATHFINDING HERE
            }

            if (tilemap.GetTile(mouseTile) == neutralTile)
            {
                
                tilemap.SetTile(mouseTile, editTile);

                if (previousTile != mouseTile)
                {
                    if (firstTouch) tilemap.SetTile(previousTile, neutralTile);
                    previousTile = mouseTile;
                    firstTouch = true;                
                }
            }
        }
    }

    // For future use (so if you input (0, 1, 0) it'll move right ISOMETRICALLY (aka down-right)
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
            chube.on = !chube.on;
        }
        else resetTile();
    }

    public void resetTile()
    {
        if (tilemap.HasTile(previousTile)) tilemap.SetTile(previousTile, neutralTile);
    }

}