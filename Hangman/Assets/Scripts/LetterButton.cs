using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 26

public class LetterButton : MonoBehaviour
{
    string letter;
    [SerializeField]
    public AudioSource audioSource;


    public void SetButton(string _letter)
    {
        letter = _letter;
    }

    public void Sendletter(bool isThatAHint)// button input or hint
    {
        Debug.Log("My letter is: " + letter);
        GameManager.instance.InputFromButton(letter, isThatAHint); // 23
        ButtonCreator.instance.RemoveLetter(this); // 26
        GetComponent<Button>().interactable = false;
    }
    public void click()
    {
        audioSource.GetComponent<AudioSource>();
        audioSource.Play();
    }
}
