using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuHandler : MonoBehaviour
{
    public bool isClicked = true;

    [Header("SLIDER")]
    [SerializeField] Slider bgmSlider;

    [Header("Animators")]
    public Animator AnimateStats;
    public Animator AnimateSettings;
    public Animator AnimateGuide;

    [Header("Panel")]
    public GameObject GuideObj;
    public GameObject GuidePanel;

    [Header("Buttons")]
    public Button StatsButton;
    public Button SettingsButton;
    public Button GuideButton;

    [Header("Image")]
    public Image OpenGuidePanel;
    public Image IntroDialog;
    public Image SecondDialog;

    [Header("STATS")]
    public TMP_Text statsText;
    public Stats saveFile;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStatsText();
        LoadBGMSession();
        IdentifyNewPlayer();
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }
    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }

    void UpdateStatsText()
    {
        StatsData statsList = SaveSystem.LoadStats();
/*        statsText.text =
            "" + statsList.totalWins + "\n" +
            "" + statsList.totalLosses + "\n" +
            "" + statsList.gamesPlayed + "\n" +
            "" + statsList.winRatio + "%\n" +
            "" + statsList.motivationLevel + "s\n" +
            "" + statsList.centralTend + "s\n";*/

        statsText.text =
            "" + statsList.totalWins + "\n" +
            "" + statsList.totalLosses + "\n" +
            "" + statsList.gamesPlayed + "\n" +
            "" + statsList.winRatio + "%\n" +
            "" + statsList.fastestTime + "s\n";


    } // 45
    public void OpenSettings()
    {
        AnimateSettings.SetTrigger("open");
        SettingsButton.GetComponent<Button>();
        SettingsButton.enabled = !SettingsButton.enabled;

        StatsButton.GetComponent<Button>();
        StatsButton.enabled = !StatsButton.enabled;

    }
    public void CloseSettings()
    {
        AnimateSettings.SetTrigger("close");
        SettingsButton.GetComponent<Button>();
        SettingsButton.enabled = true;
        StatsButton.enabled = true;
    }
    public void OpenStats()
    {
        AnimateStats.SetTrigger("open");
        StatsButton.GetComponent<Button>();
        StatsButton.enabled = !StatsButton.enabled;


        SettingsButton.GetComponent<Button>();
        SettingsButton.enabled = !SettingsButton.enabled;
    }
    public void CloseStats()
    {

        AnimateStats.SetTrigger("close");
        StatsButton.GetComponent<Button>();
        StatsButton.enabled = true;
        SettingsButton.enabled = true;
    }
    public void OpenGuide()
    {
        GuidePanel.gameObject.SetActive(true);
        AnimateGuide.SetTrigger("open");
        GuidePanelImgEnabler();
        StatsData statsList = SaveSystem.LoadStats();
        if (statsList.isNewPlayer)
        {
            Debug.Log("You're New");
            SecondDialog.GetComponent<Image>();
            SecondDialog.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("You're old");
        }

    }
    public void CloseGuide()
    {
        AnimateGuide.SetTrigger("close");
        GuidePanelImgDisabler();
    }
    public void GuidePanelImgEnabler()
    {
        OpenGuidePanel.GetComponent<Image>();
        OpenGuidePanel.gameObject.SetActive(true);
    }
    public void GuidePanelImgDisabler()
    {
        OpenGuidePanel.GetComponent<Image>();
        OpenGuidePanel.gameObject.SetActive(false);
    }
    public void IdentifyNewPlayer()
    {
        StatsData statsList = SaveSystem.LoadStats();
        if (statsList.isNewPlayer)
        {
            Debug.Log("You're New");
            IntroDialog.GetComponent<Image>();
            IntroDialog.gameObject.SetActive(true);
            GuideButton.GetComponent<Button>();
            GuideButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("You're old");
        }
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
}
