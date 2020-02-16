using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    public void setMusic(float volume) {
        mixer.SetFloat("Music", volume);
    }
    public void setSFX(float volume)
    {
        mixer.SetFloat("SFX", volume);
    }
}
