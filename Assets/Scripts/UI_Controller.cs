using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Controller : MonoBehaviour
{
    public int energy;
    public int numOfEnergy;
    public Image[] energys;
    public Sprite fullEnergy;
    public Sprite emptyEnergy;


    public static bool GameIsPaused = false;
    public GameObject pauseMenu;

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
        #region energycounter
        for (int i = 0; i < energys.Length; i++)
        {
            if (i < energy)
            {
                energys[i].sprite = fullEnergy;
            }
            else
            {
                energys[i].sprite = emptyEnergy;
            }
            if (i < numOfEnergy)
            {
                energys[i].enabled = true;
            }
            else
            {
                energys[i].enabled = false;
            }
        }
        #endregion
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");

    }
     public void Retry()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
