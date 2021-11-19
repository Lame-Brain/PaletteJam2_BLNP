using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public static class GameManager
{
    public static PuzzleManager PUZZLE;
    public static I_am_the_player PLAYER;
    public static AudioMixer SFX;

    private static bool isMuted;

    public static Action<Vector2> OnPlayerDeath;    
    public static Action<Vector2> OnBlockDeath;
    public static Action<Vector2> OnBombDeath;

    public static void SetAudioMixer(AudioMixer _am)
    {
        SFX = _am;
    }
    public static void SetSFX_VolumeLevel(float sliderVal)
    {
        SFX.SetFloat("SFX_Volume", Mathf.Log10(sliderVal) * 20);
    }
    public static void SetMusicVolumeLevel(float sliderVal)
    {
        SFX.SetFloat("Music_Volume", Mathf.Log10(sliderVal) * 20);
    }
    public static void MuteSFX()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
}
