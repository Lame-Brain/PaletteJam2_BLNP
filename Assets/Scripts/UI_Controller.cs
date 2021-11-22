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

    public TMPro.TextMeshProUGUI lives, waves, timer;

    // Start is called before the first frame update
    void Start()
    {
        
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
    public void QuitGame()
    {
        Application.Quit();
    }
}
