using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 39
using UnityEngine.UI;
using TMPro; // 44
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// 35
public class UIHandler : MonoBehaviour
{
    public static UIHandler instance; // 38

    //public Animator firstCloud;
    //public Animator secondCloud;
    public Animator gameOverPanel; // id 1
    public Animator statsPanel; // id 2
    public Animator winPanel; // id 3
    public Animator settingsPanel; // id 4
    public Animator hintPanel;
    [Header("STATS")] // 44
    public TMP_Text statsText; // 44
    public Stats saveFile; // 44
/*    [Header("POINTS")]
    public TMP_Text pointsText;*/
    [Header("AUDIO")]
    public AudioClip winnerSound;
    public AudioClip backgroundSound;
    public AudioClip gameOverSound;
    public AudioClip clickSound;
    public AudioSource audioSource;

    //fuzzy
    [Header("FuzzyLogic")]
    public AnimationCurve TimeonTaskshort;
    public AnimationCurve TimeonTaskmedium;
    public AnimationCurve TimeonTasklong;
    public AnimationCurve NumRepeatTaskLow;
    public AnimationCurve NumRepeatTaskMed;
    public AnimationCurve NumRepeatTaskHigh;
    public AnimationCurve PerformanceLow;
    public AnimationCurve PerformanceMid;
    public AnimationCurve PerformanceHigh;
    public AnimationCurve NumHelpRequestlow;
    public AnimationCurve NumHelpRequestmed;
    public AnimationCurve NumHelpRequesthigh;
    int totNumber;
    float totshortValue = 0f;
    float totmedValue = 0f;
    float totlongValue = 0f;
    int nrtNumber;
    float nrtLowValue = 0f;
    float nrtMedValue = 0f;
    float nrtHighValue = 0f;
    int perfNumber;
    float perfLowValue = 0f;
    float perfMedValue = 0f;
    float perfHighValue = 0f;
    int nhrNumber;
    float nhrlowValue = 0f;
    float nhrmedValue = 0f;
    float nhrhighValue = 0f;

    //motivation level
    float motivationLevel;


    [Header("SLIDER")]
    [SerializeField] Slider bgmSlider;

    public Image image;

    void Awake()
    {
        instance = this;   
    }// 38

    void Start()
    {

        BackGroundMusic();
        InitialSaveFile();
        UpdateStatsText();
        //Load();
        LoadBGMSession();
        //UpdatePoints();

    } // 45

    public void SettingsButton() // top-left corner button
    {
        settingsPanel.SetTrigger("open");
        ImageEnabler();
    }
    public void StatsButton() // top-left corner button
    {

        UpdateStatsText();
        statsPanel.SetTrigger("open");
        ImageEnabler();
       /* image.GetComponent<Image>();
        image.gameObject.SetActive(true);*/

        // 45
    }

    void InitialSaveFile()
    {
        Stats statFile = new Stats();

        string path = Application.persistentDataPath + "/stats.save";
        if (!File.Exists(path))
        {
            statFile.InitStats();
        }  
    }

    void UpdateStatsText()
    {
        StatsData statsList = SaveSystem.LoadStats(); 
        statsText.text =
            "" + statsList.totalWins + "\n" +
            "" + statsList.totalLosses + "\n" +
            "" + statsList.gamesPlayed + "\n" +
            "" + statsList.winRatio + "%\n" +
            "" + statsList.motivationLevel + "s\n" +
            "" + statsList.centralTend + "s\n";
    } // 45
/*    void UpdatePoints()
    {
        StatsData statsList = SaveSystem.LoadStats();
        pointsText.text = "" + statsList.points;
    }*/

    void BackGroundMusic()
    {
        audioSource.GetComponent<AudioSource>();
        audioSource.clip = backgroundSound;
        audioSource.loop = true;
        audioSource.Play();
        //audioSource.PlayOneShot(backgroundSound, 0.7f);
        
    }

    public void ClosePanelButton(int buttonId)
    {
        switch (buttonId)
        {
            case 1:
                gameOverPanelTrigger();
                break;
            case 2:
                //statsPanel.SetTrigger("close");
                statsPanelTrigger();
                break;
            case 3:
                winPanelTrigger();
                break;
            case 4:
                settingsPanelTrigger();
                //settingsPanel.SetTrigger("close");
                break;
        }
    }
    public void winPanelTrigger()
    {
        winPanel.SetTrigger("close");
        audioSource.clip = winnerSound;
        audioSource.Stop();
        BackGroundMusic();

    }
    public void gameOverPanelTrigger()
    {
        gameOverPanel.SetTrigger("close");
        audioSource.clip = gameOverSound;
        audioSource.Stop();
        BackGroundMusic();
    }
    public void statsPanelTrigger()
    {
        //image.GetComponent<Image>();
        //image.gameObject.SetActive(false);
        ImageDisabler();
        statsPanel.SetTrigger("close");

    }
    public void settingsPanelTrigger()
    {

        ImageDisabler();
        settingsPanel.SetTrigger("close");
        Save();
    }

    public void WinCondition(int playTime, int currentMistakes, int remHints) // could pass in mistakes used and time used
    {
        Stats statsFile = new Stats();
        StatsData statsList = SaveSystem.LoadStats();

        int numRepeatTask = nrtEvaluateValue(currentMistakes);
        float currentWinRatio = statsList.winRatio;
        int performance = perfEvaluateValue(currentWinRatio);
        int timeonTask = totEvaluateValue(playTime);
        int numHelpRequest = nhrEvaluateValue(remHints);
        if (statsList.gamesPlayed > 0)
        {
            motivationLevel = rulesEvaluator(timeonTask, numRepeatTask, performance, numHelpRequest);
            statsFile.SaveStats(true, true, motivationLevel, numRepeatTask); // 44
        }
        else
        {
            statsFile.SaveStats(true, false, 1000f, numRepeatTask); // 44
        }



        winPanel.SetTrigger("open");
        ImageEnabler();
        audioSource.Stop();
        if (winnerSound != null)
        {
            audioSource.PlayOneShot(winnerSound, 0.7f);
        }
    }
    public void LoseCondition(int playTime, int currentMistakes, int remHints) // could pass in mistakes used and time used
    {
        Stats statsFile = new Stats();
        StatsData statsList = SaveSystem.LoadStats();

        int numRepeatTask = nrtEvaluateValue(currentMistakes);
        float currentWinRatio = statsList.winRatio;
        int performance = perfEvaluateValue(currentWinRatio);
        int timeonTask = totEvaluateValue(playTime);
        int numHelpRequest = nhrEvaluateValue(remHints);
        if (statsList.gamesPlayed > 0)
        {
            motivationLevel = rulesEvaluator(timeonTask, numRepeatTask, performance, numHelpRequest);
            statsFile.SaveStats(false, true, motivationLevel, numRepeatTask); // 44
        }
        else
        {
            statsFile.SaveStats(false, false, 1000f ,numRepeatTask); // 44
        }

        gameOverPanel.SetTrigger("open");
        ImageEnabler();
        audioSource.Stop();
        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound, 0.7f);
        }
    }

    public void BackToMenu(string levelToLoad)
    {
        
        SceneManager.LoadScene(levelToLoad);

    } // 39

    public IEnumerator NextLevelAfterWait()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("Game");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Game");
        //StartCoroutine(NextLevelAfterWait());
    }

    public void DiffMenu()
    {
        SceneManager.LoadScene("Diff");
    }

    public void ResetGame()
    {
        // load the current open scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } // 39

    public void ExitGame()
    {
        Application.Quit();
    }// 40

    public void ImageEnabler()
    {
        image.GetComponent<Image>();
        image.gameObject.SetActive(true);
    }
    public void ImageDisabler()
    {

        image.GetComponent<Image>();
        image.gameObject.SetActive(false);
    }
    public void ClickSound()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", bgmSlider.value);
        //Load();
    }
    public void Load()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("musicVolume");
        //AudioListener.volume = bgmSlider.value;
    }
    public void LoadBGMSession()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = bgmSlider.value;
        Save();
    }
    public void DefinitionHint()
    {
        hintPanel.SetTrigger("open");
    }

    //TimeonTask
    public int totEvaluateValue(int time)
    {
        totshortValue = TimeonTaskshort.Evaluate(time);
        totmedValue = TimeonTaskmedium.Evaluate(time);
        totlongValue = TimeonTasklong.Evaluate(time);

        if (totshortValue > totmedValue && totshortValue > totlongValue)
        {
            totNumber = 3;
        }else if (totmedValue > totshortValue && totmedValue > totlongValue)
        {
            totNumber = 2;
        }else if (totlongValue > totshortValue && totlongValue > totmedValue)
        {
            totNumber = 1;
        }
        return totNumber;
    }

    //NumRepeatTaskMistakes
    public int nrtEvaluateValue(int mistakes)
    {
        nrtLowValue = NumRepeatTaskLow.Evaluate(mistakes);
        nrtMedValue = NumRepeatTaskMed.Evaluate(mistakes);
        nrtHighValue = NumRepeatTaskHigh.Evaluate(mistakes);

        if (nrtLowValue > nrtMedValue && nrtLowValue > nrtHighValue)
        {
            nrtNumber = 3;
        }
        else if (nrtMedValue > nrtLowValue && nrtMedValue > nrtHighValue)
        {
            nrtNumber = 2;
        }
        else if (nrtHighValue > nrtLowValue && nrtHighValue > nrtMedValue)
        {
            nrtNumber = 1;
        }
        return nrtNumber;
    }

    //NumRepeatTasksWins
    public int perfEvaluateValue(float winratio)
    {
        perfLowValue = PerformanceLow.Evaluate(winratio);
        perfMedValue = PerformanceMid.Evaluate(winratio);
        perfHighValue = PerformanceHigh.Evaluate(winratio);

        if (perfLowValue > perfMedValue && perfLowValue > perfHighValue)
        {
            perfNumber = 1;
        }
        else if (perfMedValue > perfLowValue && perfMedValue > perfHighValue)
        {
            perfNumber = 2;
        }
        else if (perfHighValue > perfLowValue && perfHighValue > perfMedValue)
        {
            perfNumber = 3;
        }
        return perfNumber;
    }

    //NumHelpRequest
    public int nhrEvaluateValue(int remHints)
    {
        nhrlowValue = NumHelpRequestlow.Evaluate(remHints);
        nhrmedValue = NumHelpRequestmed.Evaluate(remHints);
        nhrhighValue = NumHelpRequesthigh.Evaluate(remHints);

        if (nhrlowValue > nhrmedValue && nhrlowValue > nhrhighValue)
        {
            nhrNumber = 3;
        }
        else if (nhrmedValue > nhrlowValue && nhrmedValue > nhrhighValue)
        {
            nhrNumber = 2;
        }
        else if (nhrhighValue > nhrlowValue && nhrhighValue > nhrmedValue)
        {
            nhrNumber = 1;
        }
        return nhrNumber;
    }

    public float rulesEvaluator(int TimeonTask, int NumRepeatTask, int Performance, int NumHelpRequest)
    {
        //1
        if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 1 && NumHelpRequest == 3)
        {
            motivationLevel = 2.5f;
        }
        //2
        else if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 1 && NumHelpRequest == 2)
        {
            motivationLevel = 2.25f;
        }
        //3
        else if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 1 && NumHelpRequest == 1)
        {
            motivationLevel = 2f;
        }
        //4
        else if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 2 && NumHelpRequest == 3)
        {
            motivationLevel = 2.75f;
        }
        //5
        else if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 2 && NumHelpRequest == 2)
        {
            motivationLevel = 2f;
        }
        //6
        else if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 2 && NumHelpRequest == 1)
        {
            motivationLevel = 2f;
        }
        //7
        else if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 1)
        {
            motivationLevel = 2.5f;
        }
        //8
        else if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 2)
        {
            motivationLevel = 2.75f;
        }
        //9
        else if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 3)
        {
            motivationLevel = 3f;
        }
        //10
        else if (TimeonTask == 3 && NumRepeatTask == 2 && Performance == 1 && NumHelpRequest == 1)
        {
            motivationLevel = 1.75f;
        }
        //11
        else if (TimeonTask == 3 && NumRepeatTask == 2 && Performance == 1 && NumHelpRequest == 2)
        {
            motivationLevel = 2f;
        }
        //12
        else if (TimeonTask == 3 && NumRepeatTask == 2 && Performance == 1 && NumHelpRequest == 3)
        {
            motivationLevel = 2.25f;
        }
        //13
        else if (TimeonTask == 3 && NumRepeatTask == 2 && Performance == 2 && NumHelpRequest == 1)
        {
            motivationLevel = 2f;
        }
        //14
        else if (TimeonTask == 3 && NumRepeatTask == 2 && Performance == 2 && NumHelpRequest == 2)
        {
            motivationLevel = 2.25f;
        }
        //15
        else if (TimeonTask == 3 && NumRepeatTask == 2 && Performance == 2 && NumHelpRequest == 3)
        {
            motivationLevel = 2.5f;
        }
        //16
        else if (TimeonTask == 3 && NumRepeatTask == 2 && Performance == 3 && NumHelpRequest == 1)
        {
            motivationLevel = 2.25f;
        }
        //17
        else if (TimeonTask == 3 && NumRepeatTask == 2 && Performance == 3 && NumHelpRequest == 2)
        {
            motivationLevel = 2.5f;
        }
        //18
        else if (TimeonTask == 3 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 3)
        {
            motivationLevel = 2.75f;
        }
        //19
        else if (TimeonTask == 3 && NumRepeatTask == 1 && Performance == 1 && NumHelpRequest == 1)
        {
            motivationLevel = 1.5f;
        }
        //20
        else if (TimeonTask == 3 && NumRepeatTask == 1 && Performance == 1 && NumHelpRequest == 2)
        {
            motivationLevel = 1.75f;
        }
        //21
        else if (TimeonTask == 3 && NumRepeatTask == 1 && Performance == 1 && NumHelpRequest == 3)
        {
            motivationLevel = 2f;
        }
        //22
        else if (TimeonTask == 3 && NumRepeatTask == 1 && Performance == 2 && NumHelpRequest == 1)
        {
            motivationLevel = 1.75f;
        }
        //23
        else if (TimeonTask == 3 && NumRepeatTask == 1 && Performance == 2 && NumHelpRequest == 2)
        {
            motivationLevel = 2f;
        }
        //24
        else if (TimeonTask == 3 && NumRepeatTask == 1 && Performance == 2 && NumHelpRequest == 3)
        {
            motivationLevel = 2.25f;
        }
        //25
        else if (TimeonTask == 3 && NumRepeatTask == 1 && Performance == 3 && NumHelpRequest == 1)
        {
            motivationLevel = 2f;
        }
        //26
        else if (TimeonTask == 3 && NumRepeatTask == 1 && Performance == 3 && NumHelpRequest == 2)
        {
            motivationLevel = 2.25f;
        }
        //27
        else if (TimeonTask == 3 && NumRepeatTask == 1 && Performance == 3 && NumHelpRequest == 3)
        {
            motivationLevel = 2.5f;
        }
        //28
        else if (TimeonTask == 2 && NumRepeatTask == 3 && Performance == 1 && NumHelpRequest == 3)
        {
            motivationLevel = 2.25f;
        }
        //29
        else if (TimeonTask == 2 && NumRepeatTask == 3 && Performance == 1 && NumHelpRequest == 2)
        {
            motivationLevel = 2f;
        }
        //30
        else if (TimeonTask == 2 && NumRepeatTask == 3 && Performance == 1 && NumHelpRequest == 1)
        {
            motivationLevel = 1.75f;
        }
        //31
        else if (TimeonTask == 2 && NumRepeatTask == 3 && Performance == 2 && NumHelpRequest == 3)
        {
            motivationLevel = 2.5f;
        }
        //32
        else if (TimeonTask == 2 && NumRepeatTask == 3 && Performance == 2 && NumHelpRequest == 2)
        {
            motivationLevel = 2.25f;
        }
        //33
        else if (TimeonTask == 2 && NumRepeatTask == 3 && Performance == 2 && NumHelpRequest == 1)
        {
            motivationLevel = 2f;
        }
        //34
        else if (TimeonTask == 2 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 1)
        {
            motivationLevel = 2.25f;
        }
        //35
        else if (TimeonTask == 2 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 2)
        {
            motivationLevel = 2.5f;
        }
        //36
        else if (TimeonTask == 2 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 3)
        {
            motivationLevel = 2.75f;
        }
        //37
        else if (TimeonTask == 2 && NumRepeatTask == 2 && Performance == 1 && NumHelpRequest == 1)
        {
            motivationLevel = 1.5f;
        }
        //38
        else if (TimeonTask == 2 && NumRepeatTask == 2 && Performance == 1 && NumHelpRequest == 2)
        {
            motivationLevel = 1.75f;
        }
        //39
        else if (TimeonTask == 2 && NumRepeatTask == 2 && Performance == 1 && NumHelpRequest == 3)
        {
            motivationLevel = 2f;
        }
        //40
        else if (TimeonTask == 2 && NumRepeatTask == 2 && Performance == 2 && NumHelpRequest == 1)
        {
            motivationLevel = 1.75f;
        }
        //41
        else if (TimeonTask == 2 && NumRepeatTask == 2 && Performance == 2 && NumHelpRequest == 2)
        {
            motivationLevel = 2f;
        }
        //42
        else if (TimeonTask == 2 && NumRepeatTask == 2 && Performance == 2 && NumHelpRequest == 3)
        {
            motivationLevel = 2.25f;
        }
        //43
        else if (TimeonTask == 2 && NumRepeatTask == 2 && Performance == 3 && NumHelpRequest == 1)
        {
            motivationLevel = 2f;
        }
        //44
        else if (TimeonTask == 2 && NumRepeatTask == 2 && Performance == 3 && NumHelpRequest == 2)
        {
            motivationLevel = 2.25f;
        }
        //45
        else if (TimeonTask == 2 && NumRepeatTask == 2 && Performance == 3 && NumHelpRequest == 3)
        {
            motivationLevel = 1.75f;
        }
        //46
        else if (TimeonTask == 2 && NumRepeatTask == 1 && Performance == 1 && NumHelpRequest == 1)
        {
            motivationLevel = 1.25f;
        }
        //47
        else if (TimeonTask == 2 && NumRepeatTask == 1 && Performance == 1 && NumHelpRequest == 2)
        {
            motivationLevel = 1.5f;
        }
        //48
        else if (TimeonTask == 2 && NumRepeatTask == 1 && Performance == 1 && NumHelpRequest == 3)
        {
            motivationLevel = 1.75f;
        }
        //49
        else if (TimeonTask == 2 && NumRepeatTask == 1 && Performance == 2 && NumHelpRequest == 1)
        {
            motivationLevel = 1.5f;
        }
        //50
        else if (TimeonTask == 2 && NumRepeatTask == 1 && Performance == 2 && NumHelpRequest == 2)
        {
            motivationLevel = 1.75f;
        }
        //51
        else if (TimeonTask == 2 && NumRepeatTask == 1 && Performance == 2 && NumHelpRequest == 3)
        {
            motivationLevel = 2f;
        }
        //52
        else if (TimeonTask == 2 && NumRepeatTask == 1 && Performance == 3 && NumHelpRequest == 1)
        {
            motivationLevel = 1.75f;
        }
        //53
        else if (TimeonTask == 2 && NumRepeatTask == 1 && Performance == 3 && NumHelpRequest == 2)
        {
            motivationLevel = 2f;
        }
        //54
        else if (TimeonTask == 2 && NumRepeatTask == 1 && Performance == 3 && NumHelpRequest == 3)
        {
            motivationLevel = 2.25f;
        }
        //55
        else if (TimeonTask == 1 && NumRepeatTask == 3 && Performance == 1 && NumHelpRequest == 3)
        {
            motivationLevel = 2f;
        }
        //56
        else if (TimeonTask == 1 && NumRepeatTask == 3 && Performance == 1 && NumHelpRequest == 2)
        {
            motivationLevel = 1.75f;
        }
        //57
        else if (TimeonTask == 1 && NumRepeatTask == 3 && Performance == 1 && NumHelpRequest == 1)
        {
            motivationLevel = 1.5f;
        }
        //58
        else if (TimeonTask == 1 && NumRepeatTask == 3 && Performance == 2 && NumHelpRequest == 3)
        {
            motivationLevel = 2.25f;
        }
        //59
        else if (TimeonTask == 1 && NumRepeatTask == 3 && Performance == 2 && NumHelpRequest == 2)
        {
            motivationLevel = 2f;
        }
        //60
        else if (TimeonTask == 1 && NumRepeatTask == 3 && Performance == 2 && NumHelpRequest == 1)
        {
            motivationLevel = 1.75f;
        }
        //61
        else if (TimeonTask == 1 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 1)
        {
            motivationLevel = 2f;
        }
        //62
        else if (TimeonTask == 1 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 2)
        {
            motivationLevel = 2.25f;
        }
        //63
        else if (TimeonTask == 1 && NumRepeatTask == 3 && Performance == 3 && NumHelpRequest == 3)
        {
            motivationLevel = 2.5f;
        }
        //64
        else if (TimeonTask == 1 && NumRepeatTask == 2 && Performance == 1 && NumHelpRequest == 1)
        {
            motivationLevel = 1.25f;
        }
        //65
        else if (TimeonTask == 1 && NumRepeatTask == 2 && Performance == 1 && NumHelpRequest == 2)
        {
            motivationLevel = 1.5f;
        }
        //66
        else if (TimeonTask == 1 && NumRepeatTask == 2 && Performance == 1 && NumHelpRequest == 3)
        {
            motivationLevel = 1.75f;
        }
        //67
        else if (TimeonTask == 1 && NumRepeatTask == 2 && Performance == 2 && NumHelpRequest == 1)
        {
            motivationLevel = 1.5f;
        }
        //68
        else if (TimeonTask == 1 && NumRepeatTask == 2 && Performance == 2 && NumHelpRequest == 2)
        {
            motivationLevel = 1.75f;
        }
        //69
        else if (TimeonTask == 1 && NumRepeatTask == 2 && Performance == 2 && NumHelpRequest == 3)
        {
            motivationLevel = 2;
        }
        //70
        else if (TimeonTask == 1 && NumRepeatTask == 2 && Performance == 3 && NumHelpRequest == 1)
        {
            motivationLevel = 1.75f;
        }
        //71
        else if (TimeonTask == 1 && NumRepeatTask == 2 && Performance == 3 && NumHelpRequest == 2)
        {
            motivationLevel = 2f;
        }
        //72
        else if (TimeonTask == 1 && NumRepeatTask == 2 && Performance == 3 && NumHelpRequest == 3)
        {
            motivationLevel = 2.25f;
        }
        //73
        else if (TimeonTask == 1 && NumRepeatTask == 1 && Performance == 1 && NumHelpRequest == 1)
        {
            motivationLevel = 1f;
        }
        //74
        else if (TimeonTask == 1 && NumRepeatTask == 1 && Performance == 1 && NumHelpRequest == 2)
        {
            motivationLevel = 1.25f;
        }
        //75
        else if (TimeonTask == 1 && NumRepeatTask == 1 && Performance == 1 && NumHelpRequest == 3)
        {
            motivationLevel = 1.5f;
        }
        //76
        else if (TimeonTask == 1 && NumRepeatTask == 1 && Performance == 2 && NumHelpRequest == 1)
        {
            motivationLevel = 1.25f;
        }
        //77
        else if (TimeonTask == 1 && NumRepeatTask == 1 && Performance == 2 && NumHelpRequest == 2)
        {
            motivationLevel = 1.5f;
        }
        //78
        else if (TimeonTask == 1 && NumRepeatTask == 1 && Performance == 2 && NumHelpRequest == 3)
        {
            motivationLevel = 1.75f;
        }
        //79
        else if (TimeonTask == 1 && NumRepeatTask == 1 && Performance == 3 && NumHelpRequest == 1)
        {
            motivationLevel = 1.5f;
        }
        //80
        else if (TimeonTask == 1 && NumRepeatTask == 1 && Performance == 3 && NumHelpRequest == 2)
        {
            motivationLevel = 1.75f;
        }
        //81
        else if (TimeonTask == 1 && NumRepeatTask == 1 && Performance == 3 && NumHelpRequest == 3)
        {
            motivationLevel = 2f;
        }


        return motivationLevel;
    }
}
