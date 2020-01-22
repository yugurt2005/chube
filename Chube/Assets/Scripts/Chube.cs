using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Chube : MonoBehaviour
{
    public bool switchedMode = false;

    public GameObject BuildMode;
    public GameObject BuildCursor;
    public IsometricMovement troops;
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
    }    

    void onButtonPress()
    {
        switchedMode = !switchedMode;
        if (switchedMode) turnOn();
        else turnOff();
    }

    public void turnOff()
    {
        BuildCursor.SetActive(false);
        BuildMode.SetActive(false);
    }

    public void turnOn()
    {
        BuildCursor.SetActive(true);
        BuildMode.SetActive(true);

        troops.troopsOn = false;
        troops.resetTile();
    }
}
