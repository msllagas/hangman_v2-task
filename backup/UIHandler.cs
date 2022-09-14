using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 39
using TMPro; // 44

// 35
public class UIHandler : MonoBehaviour
{
    public static UIHandler instance; // 38


    public Animator gameOverPanel; // id 1
    public Animator statsPanel; // id 2
    public Animator winPanel; // id 3
    public Animator settingsPanel; // id 4
    [Header("STATS")] // 44
    public TMP_Text statsText; // 44
    [SerializeField] SCR_BaseStats saveFile; // 44

    void Awake()
    {
        instance = this;   
    }// 38

    void Start()
    {
        UpdateStatsText();
    } // 45

    public void SettingsButton() // top-left corner button
    {
        settingsPanel.SetTrigger("open");
    }
    public void StatsButton() // top-left corner button
    {
        statsPanel.SetTrigger("open");
       UpdateStatsText(); // 45
    } 

    void UpdateStatsText()
    {
        List<int> statsList = saveFile.GetStats(); 
        statsText.text =
            "Total Wins: " + statsList[0] + "\n" +
            "Total Losses: " + statsList[1] + "\n" +
            "Total Games Played: " + statsList[3] + "\n" +
            "Win Rate: " + statsList[2] + "% \n" +
            "Fastest Time: " + statsList[4] + " seconds \n"; 
    } // 45

    public void ClosePanelButton(int buttonId)
    {
        switch (buttonId)
        {
            case 1:
                gameOverPanel.SetTrigger("close");
                break;
            case 2:
                statsPanel.SetTrigger("close");
                break;
            case 3:
                winPanel.SetTrigger("close");
                break;
            case 4:
                settingsPanel.SetTrigger("close");
                break;
        }
    }


    public void WinCondition(int playTime) // could pass in mistakes used and time used
    {
        saveFile.SaveStats(true, playTime); // 44
        winPanel.SetTrigger("open");
    }

    public void LoseCondition(int playTime) // could pass in mistakes used and time used
    {
        saveFile.SaveStats(false, playTime); // 44
        gameOverPanel.SetTrigger("open");
    }

    public void BackToMenu(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    } // 39

    public void ResetGame()
    {
        // load the current open scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } // 39

    public void ExitGame()
    {
        Application.Quit();
    }// 40
}
