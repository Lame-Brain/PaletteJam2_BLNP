using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_a_SaveState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume")) GameManager.SetMusicVolumeLevel(PlayerPrefs.GetFloat("MusicVolume"));
        if (PlayerPrefs.HasKey("SFXVolume")) GameManager.SetSFX_VolumeLevel(PlayerPrefs.GetFloat("SFXVolume"));
        if (PlayerPrefs.HasKey("LLReached")) PlayerPrefs.GetInt("LLReached");
    }
}
