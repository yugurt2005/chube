using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

// Inherits from MainButton class
public class BuildModeButton : MainButton
{
    public GameObject BuildMode;
    public GameObject BuildCursor;
    public GameObject BuildMenu;

    public Button button;
    public MainButtonsController controller;

    void Start()
    {
        button.onClick.AddListener(onButtonPress);
    }

    // Overrides turnOn and turnOff functions
    public override void turnOn()
    {
        controller.mode = 1;
        setActivity(true);
        on = true;
    }

    public override void turnOff()
    {
        setActivity(false);
        on = false;
    }

    public void setActivity(bool mode)
    {
        BuildCursor.SetActive(mode);
        BuildMenu.SetActive(mode);
    }

    void onButtonPress() {
        controller.buttonPressed = 1;
    }
}
