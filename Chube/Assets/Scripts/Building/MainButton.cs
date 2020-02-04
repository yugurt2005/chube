using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton : MonoBehaviour
{
    public bool on = false;

    public virtual void turnOn() {
        on = true;
    }

    public virtual void turnOff() {
        on = false;
    }

}