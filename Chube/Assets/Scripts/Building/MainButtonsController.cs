using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonsController : MonoBehaviour
{
    public int buttonPressed;
    public int mode;

    public TroopModeButton troopButton;
    public BuildModeButton buildButton;
    public EmptySystem emptyMode;

    private List<MainButton> buttons = new List<MainButton>();

    void Start()
    {
        buttons.Add(emptyMode);
        buttons.Add(buildButton);
        buttons.Add(troopButton);
    }

    void Update()
    {
        if (buttonPressed != 0) {
            for (int i = 0; i < buttons.Count; i++) {
                if (i == buttonPressed)
                {
                    if (buttons[buttonPressed].on)
                    {
                        buttons[buttonPressed].turnOff();
                        mode = 0;
                    }
                    else buttons[buttonPressed].turnOn();
                    
                }
                else buttons[i].turnOff();
            }
            buttonPressed = 0;
        }
    }
}
