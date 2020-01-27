using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

// NOTE TO SELF: FIX THIS SCRIPT ITS GROSS
public class Chube : MonoBehaviour
{
    public bool on = false;

    public GameObject BuildMode;
    public GameObject BuildCursor;
    public GameObject BuildMenu;
    public IsometricMovement troops;
    public Button BuildModeButton;
    public Tilemap tilemap;
    public Tile chubeTile;
    public TilemapRenderer tilemapRenderer;

    void Start()
    {
        BuildModeButton.onClick.AddListener(onButtonPress);
        tilemap.SetTile(new Vector3Int(0, -1, tilemapRenderer.sortingOrder), chubeTile);
    }

    void Update()
    {
    }    

    void onButtonPress()
    {
        on = !on;
        if (on) turnOn();
        else turnOff();
    }

    public void turnOff()
    {
        BuildCursor.SetActive(false);
        BuildMode.SetActive(false);
        BuildMenu.SetActive(false);
    }

    public void turnOn()
    {
        BuildCursor.SetActive(true);
        BuildMode.SetActive(true);
        BuildMenu.SetActive(true);

        if (troops.troopsOn)
        {
            troops.troopsOn = false;
            troops.resetTile();
        }
    }
}
