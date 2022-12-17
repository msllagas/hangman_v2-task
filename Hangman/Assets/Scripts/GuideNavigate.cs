using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideNavigate : MonoBehaviour
{
    // Start is called before the first frame update
    public static GuideNavigate instance;
    public static int initChild = 0;


    [Header("Buttons")]
    public Button PrevButton;
    public Button NextButton;
    public Button PlayButton;

    [Header("Images")]
    public Image NewPlayerImage;
    void Start()
    {
        SelectGuide(0);
        CheckNextPrev();
        IdentifyNewPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(initChild);
    }
    public void Navigate(int _change)
    {
        initChild += _change;
        StatsData stats = SaveSystem.LoadStats();
        if (initChild > transform.childCount - 1)       
            initChild = 0;     
        else if (initChild < 0)      
            initChild = transform.childCount - 1;
        SaveSystem.SaveStats(stats);
        SelectGuide(initChild);
        CheckNextPrev();
    }
    private void SelectGuide(int _index)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == _index);
    }
    private void CheckNextPrev()
    {
        int children = transform.childCount;

        if (initChild == 0)
        {
            PrevButton.gameObject.SetActive(false);
        }
        else if(initChild == children-1)
        {
            NextButton.gameObject.SetActive(false);
            PlayButton.gameObject.SetActive(true);
        }
        else
        {
            PrevButton.gameObject.SetActive(true);
            NextButton.gameObject.SetActive(true);
        }
    }
    public void IdentifyNewPlayer()
    {
        StatsData statsList = SaveSystem.LoadStats();
        if (statsList.isNewPlayer)
        {
            Debug.Log("You're New");
            NewPlayerImage.gameObject.SetActive(true);
            PlayButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("You're old");
        }
    }
}
