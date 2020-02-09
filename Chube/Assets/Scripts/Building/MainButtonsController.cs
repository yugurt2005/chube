using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonsController : MonoBehaviour
{
    public int buttonPressed;
    public int mode;
    private bool foundButton;

    public TroopModeButton troopButton;
    public BuildModeButton buildButton;
    public EmptySystem emptyMode;

    private List<MainButton> buttons = new List<MainButton>();

    public AudioSource buttonPress;
    public AudioSource buttonExit;
    public SFXController SFX;

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
                        SFX.playSound(buttonExit);
                        mode = 0;
                    }
                    else
                    {
                        buttons[buttonPressed].turnOn();
                        SFX.playSound(buttonPress);
                        foundButton = true;
                    }
                }
                else
                {
                    buttons[i].turnOff();
                    if (!foundButton) {
                        mode = 0;
                    }
                }
            }
            buttonPressed = 0;
            foundButton = false;
        }
    }
}
