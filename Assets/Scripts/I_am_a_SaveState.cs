using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class I_am_a_SaveState : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Slider MusicVolumeSlider, SFXVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.SetAudioMixer(audiomixer);

        if (PlayerPrefs.HasKey("MusicVolume"))
            MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        else
        {
            MusicVolumeSlider.value = .5f;
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
        }
        GameManager.SetMusicVolumeLevel(MusicVolumeSlider.value);

        if (PlayerPrefs.HasKey("SFXVolume"))
            SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        else
        {
            SFXVolumeSlider.value = 0.5f;
            PlayerPrefs.SetFloat("SFXVolume", 0.5f);
        }
        GameManager.SetSFX_VolumeLevel(SFXVolumeSlider.value);

        if (PlayerPrefs.HasKey("LLReached"))
            GameManager.lastLevelReached = PlayerPrefs.GetInt("LLReached");
        else
            GameManager.lastLevelReached = 1;
    }

    public void Music_Volume_Slider()
    {
        GameManager.SetMusicVolumeLevel(MusicVolumeSlider.value);
    }
    public void SFX_Volume_Slider()
    {
        GameManager.SetSFX_VolumeLevel(SFXVolumeSlider.value);
    }

}
