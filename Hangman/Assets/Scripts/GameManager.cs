using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq; // 19

// 16
public class GameManager : MonoBehaviour
{

    public static GameManager instance; // 23


    List<string> solvedList = new List<string>();

    string[] unsolvedWord;
    [Header("Letters")]
    [Space]
    public GameObject letterPrefab;
    public Transform letterHolder;
    List<TMP_Text> letterHolderList = new List<TMP_Text>(); // 20
    [Header("Categories")]
    [Space]
    public Category[] categories; // 19
    public TMP_Text categoryText; // 24
    public TMP_Text definitionText;
    public TMP_Text correctWord;
    [Header("Timer")] // 25
    [Space]
    public TMP_Text timerText; // 25
    int playTime; // 25
    bool gameOver; // 25
    [Header("Hints")] // 27
    [Space]
    public int maxHints = 3; // 27
    [Header("Mistakes")] // 32
    [Space]
    public Animator[] petalList;
    [SerializeField]
    int maxMistakes;
    int currentMistakes;

    

    void Awake()
    {
        instance = this;
    } // 23

    // Start is called before the first frame update
    void Start()
    {
        maxMistakes = petalList.Length; // 32
        Initialize();
        StartCoroutine(Timer()); // 25
    }

 
    void Initialize()
    {
        // pick a category first

        int catNum = DiffHandler.instance.selectedButton;
        Debug.Log(catNum);
        //int cIndex = Random.Range(0, categories.Length); // 19
        int cIndex = catNum;

        categoryText.text = categories[cIndex].name; // 24
        int wIndex = Random.Range(0, categories[cIndex].wordList.Length); // 19

        //pick a word from list or category
        string pickedWord = categories[cIndex].wordList[wIndex]; // 19

        definitionText.text = categories[cIndex].definition[wIndex];
        correctWord.text = pickedWord;
        // split the word into single letters
        string[] splittedWord = pickedWord.Select(l => l.ToString()).ToArray(); // 19
        unsolvedWord = new string[splittedWord.Length]; // 19
        foreach (string letter in splittedWord) // 20
        {
            solvedList.Add(letter);
        }
        // create the visual
        for (int i = 0; i < solvedList.Count; i++) // 20
        {
            GameObject tempLetter = Instantiate(letterPrefab, letterHolder, false);
            letterHolderList.Add(tempLetter.GetComponent<TMP_Text>());
        }
    }

    public void InputFromButton(string requestedLetter, bool isThatAHint)
    {
        // Check if the game is not over yet


        // search mechanic for solved list
        CheckLetter(requestedLetter, isThatAHint); // 21
    }

    void CheckLetter(string requestedLetter, bool isThatAHint)
    {
        if (gameOver) // 33
        {
            return;
        }

        bool letterFound = false; // 21
        // find the letter in the solved list
        for (int i = 0; i < solvedList.Count; i++) // 21
        {
            if (solvedList[i] == requestedLetter)
            {
                letterHolderList[i].text = requestedLetter;
                unsolvedWord[i] = requestedLetter;
                letterFound = true;
            }
        }

        if (!letterFound && !isThatAHint) // 21
        {
            // mistake stuff - graphical representation
            petalList[currentMistakes].SetTrigger("miss"); // 32
            currentMistakes++;

            // and do game over
            if (currentMistakes == maxMistakes) // 32
            {
                // Debug.Log("Lost Game");
                UIHandler.instance.LoseCondition(playTime, maxHints); // 38
                gameOver = true;
                
                return;
            }

        }

        // check if game won
        Debug.Log("Game Won?: " + CheckIfWon()); // 22
        gameOver = CheckIfWon(); // 25
        if (gameOver) // 25
        {
            // show ui
            UIHandler.instance.WinCondition(playTime, maxHints);
        }
    }

    bool CheckIfWon()
    {
        // Check Mechanic
        for (int i = 0; i < unsolvedWord.Length; i++) // 22
        {
            if (unsolvedWord[i] != solvedList[i])
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator Timer()
    {
        int seconds = 0;
        int minutes = 0;
        timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        while (!gameOver)
        {
            yield return new WaitForSeconds(1);
            playTime++;
            
            seconds = playTime % 60;
            minutes = (playTime / 60) % 60;

            timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }
    } // 25

    public bool GameOver()
    {
        return gameOver;
    } // 27
}
