using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Controller : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject VictoryScreen, DeadScreen, NextLevelButton, RetryButton;
    public AudioSource MusicPlayer;
    public List<AudioClip> Songs = new List<AudioClip>();
    public AudioClip failSong, victorySong;
    private int musicTrack = 0;


    public TMPro.TextMeshProUGUI lives, waves, timer;

    // Start is called before the first frame update
    void Start()
    {
        MusicPlayer.clip = Songs[0];
        MusicPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        #region pausemenu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        #endregion
        #region HUD hook-ups
        lives.text = GameManager.Lives.ToString();
        if (GameManager.PUZZLE.current_Wave < GameManager.PUZZLE.Number_of_Waves)
            waves.text = "WAVE : " + (GameManager.PUZZLE.current_Wave + 1).ToString();
        else
            waves.text = "NEXT";
        timer.text = GameManager.PUZZLE.Timer.ToString("F2");
        #endregion

        if (!MusicPlayer.isPlaying && !DeadScreen.activeSelf && !VictoryScreen.activeSelf)
        {
            if (musicTrack < Songs.Count-1)
            {
                musicTrack++;
                MusicPlayer.clip = Songs[musicTrack];
                MusicPlayer.Play();
            }
            if (musicTrack == Songs.Count - 1) MusicPlayer.Play();
        }
    }


    public void Show_Death_Screen()
    {
        StartCoroutine(Show_Death_Screen_CR());
    }
    IEnumerator Show_Death_Screen_CR()
    {
        MusicPlayer.clip = failSong;
        MusicPlayer.Play();
        GameManager.Lives--;
        if (GameManager.Lives < 1) RetryButton.SetActive(false);
        yield return new WaitForSeconds(2);
        DeadScreen.SetActive(true);
    }

    public void Show_Victory_Screen()
    {
        StartCoroutine(Show_Victory_Screen_CR());
    }
    IEnumerator Show_Victory_Screen_CR()
    {
        MusicPlayer.clip = victorySong;
        MusicPlayer.Play();
        yield return new WaitForSeconds(2);
        if (GameManager.CheckLastLevelReached(SceneManager.GetActiveScene().buildIndex + 1)) NextLevelButton.SetActive(false);
        VictoryScreen.SetActive(true);
    }


    public void Resume()
    {
        pauseMenu.SetActive(false);
        GameIsPaused = false;
        GameManager.PauseGame(GameIsPaused);
        HUD.SetActive(true);
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        GameIsPaused = true;
        GameManager.PauseGame(GameIsPaused);
        HUD.SetActive(false);
    }
    public void LoadMenu()
    {
        GameIsPaused = false;
        GameManager.PauseGame(GameIsPaused);
        SceneManager.LoadScene("MainMenu");

    }
     public void Retry()
    {
        GameIsPaused = false;
        GameManager.PauseGame(GameIsPaused);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    
    public void NextLevel()
    {
        GameManager.NextLevel();
    }
}
