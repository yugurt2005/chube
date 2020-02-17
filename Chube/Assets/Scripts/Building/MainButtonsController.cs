using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Now I realize this script is kind of useless after getting rid of "troop mode" & learning about buttons lol... ITS OK ILL KEEP IT JUST
// IN CASE THERES MORE MAIN BUTTONS TO ADD
// But later I'll get rid of if if not

public class MainButtonsController : MonoBehaviour
{
    /*
    public int buttonPressed;
    public int mode;
    private bool foundButton;

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
    */
}
