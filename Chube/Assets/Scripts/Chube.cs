using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Chube : MonoBehaviour
{    
    private bool switchedMode = false;

    public GameObject BuildMode;
    public GameObject BuildCursor;
    public Button BuildModeButton;
    public Tilemap tilemap;
    public Tile chubeTile;

    void Start()
    {
        BuildModeButton.onClick.AddListener(onButtonPress);
        tilemap.SetTile(new Vector3Int(0, -1, -4), chubeTile);
    }

    void Update()
    {
        // If the button was pressed, building mode related things are turned on/off
        if (switchedMode)
        {
            BuildCursor.SetActive(!BuildCursor.activeSelf);
            BuildMode.SetActive(!BuildMode.activeSelf);
            switchedMode = false;
        }
    }    

    void onButtonPress()
    {
        switchedMode = true;
    }
}
