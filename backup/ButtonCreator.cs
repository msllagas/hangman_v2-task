using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonCreator : MonoBehaviour
{
    public static ButtonCreator instance; // 26

    public GameObject buttonPrefab;
    string[] letterToUse = new string[26] {"A", "B", "C", "D", "E",
                                            "F", "G", "H", "I", "J",
                                            "K", "L", "M", "N", "O",
                                            "P", "Q", "R", "S", "T",
                                            "U", "V", "W", "X", "Y",
                                            "Z"};
    public Transform buttonHolder;

    List<LetterButton> letterList = new List<LetterButton>(); // 26

    void Awake()
    {
        instance = this;
    } // 26

    // Start is called before the first frame update
    void Start()
    {
        PopulateKeyboard();
    }

    void PopulateKeyboard()
    {
        for (int i = 0; i < letterToUse.Length; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonHolder, false);
            newButton.GetComponentInChildren<TMP_Text>().text = letterToUse[i];
            LetterButton myLetter = newButton.GetComponent<LetterButton>(); // 26
            myLetter.SetButton(letterToUse[i]);

            letterList.Add(myLetter);
        }
    }

    public void RemoveLetter(LetterButton theButton)
    {
        letterList.Remove(theButton);
    }// 26

    // From the hint button
    public void UseHint()
    {
        if (GameManager.instance.GameOver() || GameManager.instance.maxHints <= 0)
        {
            Debug.Log("No Hints Left!");
            return;
        } // 27
        GameManager.instance.maxHints--; // 27
        int randomIndex = Random.Range(0, letterList.Count);
        letterList[randomIndex].Sendletter(true);
    } // 26
}
