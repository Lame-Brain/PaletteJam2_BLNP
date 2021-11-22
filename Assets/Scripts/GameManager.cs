using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static PuzzleManager PUZZLE;
    public static I_am_the_player PLAYER;
    public static AudioMixer SFX;

    private static bool isMuted;

    public static Action<Vector2> OnPlayerDeath;    
    public static Action<Vector2> OnBlockDeath;
    public static Action<Vector2> OnBombDeath;

    public static int Lives = 3;
    public static int currentLevel;

    public static int lastLevelReached;

    public static object SceneManagment { get; private set; }

    #region Audio Stuff
    public static void SetAudioMixer(AudioMixer _am)
    {
        SFX = _am;
    }
    public static void SetSFX_VolumeLevel(float sliderVal)
    {
        SFX.SetFloat("SFX_Volume", Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderVal);
    }
    public static void SetMusicVolumeLevel(float sliderVal)
    {
        SFX.SetFloat("Music_Volume", Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderVal);
    }
    public static void MuteSFX()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
    #endregion

    public static void CheckLastLevelReached(int level)
    {
        if(level > lastLevelReached)
        {
            lastLevelReached = level;
            PlayerPrefs.SetInt("LLReached", level);
        }
    }

    public static void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public static void NextLevel()
    {
        if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
        {
            CheckLastLevelReached(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);            
        }
        else
        {
            //WIN GAME I GUESS?
        }
    }
}
