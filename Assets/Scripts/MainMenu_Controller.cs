using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_Controller : MonoBehaviour
{
    public GameObject Locked_Icon;
    public TMPro.TextMeshProUGUI Level_Number;

    private int levelNumber;

    private void Awake()
    {
    }

    private void Start()
    {
        levelNumber = 1;
        Level_Number.text = levelNumber.ToString();
        Locked_Icon.SetActive(false);
    }

    public void ChangeLevel(int delta)
    {
        levelNumber += delta;

        if (levelNumber > SceneManager.sceneCountInBuildSettings - 1) levelNumber = 1;
        if (levelNumber < 1) levelNumber = SceneManager.sceneCountInBuildSettings - 1;

        Level_Number.text = "";
        if (levelNumber < 10) Level_Number.text = "0";
        Level_Number.text += levelNumber.ToString();

        if (levelNumber > GameManager.lastLevelReached) 
            Locked_Icon.SetActive(true);
        else
            Locked_Icon.SetActive(false);
    }

    public void StartLevel()
    {
        if(levelNumber <= GameManager.lastLevelReached) SceneManager.LoadScene(levelNumber);
    }

}
