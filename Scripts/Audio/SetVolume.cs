using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public SoundManager manager;

    public Slider slider;

    private void Start()
    {
        
    }

    public void SetBGMVolume()
    {
        manager.SetVolume("BGM", slider.value);
    }

    public void SetSFXVolume()
    {
        manager.SetVolume("SFX", slider.value);
    }
}
