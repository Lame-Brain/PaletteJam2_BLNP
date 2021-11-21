using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class I_am_a_SaveState : MonoBehaviour
{
    public AudioMixer audiomixer;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.SetAudioMixer(audiomixer);

        if (PlayerPrefs.HasKey("MusicVolume"))
            GameManager.SetMusicVolumeLevel(PlayerPrefs.GetFloat("MusicVolume"));
        else
            GameManager.SetMusicVolumeLevel(.5f);

        if (PlayerPrefs.HasKey("SFXVolume"))
            GameManager.SetSFX_VolumeLevel(PlayerPrefs.GetFloat("SFXVolume"));
        else
            GameManager.SetSFX_VolumeLevel(.5f);

        if (PlayerPrefs.HasKey("LLReached"))
            GameManager.lastLevelReached = PlayerPrefs.GetInt("LLReached");
        else
            GameManager.lastLevelReached = 1;
    }
}
