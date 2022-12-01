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

    [Header("Buttons")]
    public Button StatsButton;
    public Button SettingsButton;

    [Header("STATS")]
    public TMP_Text statsText;
    public Stats saveFile;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStatsText();
        LoadBGMSession();
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
