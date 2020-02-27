using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class BuildModeButton : MonoBehaviour
{
    public bool on;

    public GameObject BuildMode;
    public GameObject BuildCursor;
    public GameObject BuildMenu;

    public Button button;

    void Start()
    {
        button.onClick.AddListener(onButtonPress);
    }

    // Overrides turnOn and turnOff functions
    public void turnOn()
    {
        setActivity(true);
        on = true;
    }

    public void turnOff()
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
        if (on) turnOff();
        else turnOn();
    }
}
