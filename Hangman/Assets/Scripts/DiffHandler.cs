using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // 
using UnityEngine.UI;

public class DiffHandler : MonoBehaviour

{
    [Header("Game Object")]
    public Button[] ButtonGroup;

    [Header("Image")]
    public Image StarterImage;
    public Image PreventButtonClick;

    public static DiffHandler instance; // 38

    public int selectedButton = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }// 38
    void Start()
    {
        IdentifyNewPlayer();
    }
    // Update is called once per frame
    public void LevelButton()
    {
        string clickedButton = EventSystem.current.currentSelectedGameObject.name;

        if (string.Equals(clickedButton, "Level A"))
        {
            selectedButton = 0;
            Debug.Log(selectedButton);
            SceneManager.LoadScene("Game");
        }
        else if (string.Equals(clickedButton, "Level B"))
        {
            selectedButton = 1;
            Debug.Log(selectedButton);
            SceneManager.LoadScene("Game");
        }
        else if (string.Equals(clickedButton, "Level C"))
        {
            selectedButton = 2;
            Debug.Log(selectedButton);
            SceneManager.LoadScene("Game");
        }
        StatsData statsList = SaveSystem.LoadStats();
        statsList.isNewPlayer = false;
        SaveSystem.SaveStats(statsList);

    }
    public void IdentifyNewPlayer()
    {
        StatsData statsList = SaveSystem.LoadStats();
        if (statsList.isNewPlayer)
        {
            Debug.Log("You're New");
            /*NewPlayerImage.gameObject.SetActive(true);
            PlayButton.gameObject.SetActive(false);*/
            StarterImage.gameObject.SetActive(true);
            PreventButtonClick.gameObject.SetActive(true);
}
        else
        {
            Debug.Log("You're old");
        }
    }

}
