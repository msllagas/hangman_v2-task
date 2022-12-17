using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Introduction : MonoBehaviour
{

    public string[] Lines;
    public float TextSpeed;

    [Header("Player Name Panel")]
    public Animator PlayerNamePanel;
    public Animator ConfirmationPanel;

    [Header("Game Object")]
    public GameObject[] GameObjects;

    [Header("Images")]
    public Image SecondDialog;
    public Image Preventer;

    [Header("Input Field")]
    public TMP_InputField PlayerFirstName;
    public TMP_InputField PlayerLastName;

    [Header("Text Field")]
    public TMP_Text DialogHolder;
    public TMP_Text ErrorText;
    public TMP_Text ConfirmationText;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        DialogHolder.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void NextDialog()
    {
        if (DialogHolder.text == Lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            DialogHolder.text = Lines[index];
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in Lines[index].ToCharArray())
        {
            DialogHolder.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }
    void NextLine()
    {
        if (index < Lines.Length - 1)
        {
            index++;
            DialogHolder.text = string.Empty;
            StartCoroutine(TypeLine());
            if (index == Lines.Length - 1)
            {
               /* Arrow.GetComponent<Image>();
                Arrow.gameObject.SetActive(true);
                GuideButton.GetComponent<Button>();
                GuideButton.gameObject.SetActive(true);*/
            }
        }
        else
        {
            foreach (GameObject item in GameObjects)
            {
                item.gameObject.SetActive(false);
            }
            PlayerNamePanel.gameObject.SetActive(true);
            PlayerNamePanel.SetTrigger("open");
        }
    }
    public void Save()
    {
        string firstName = PlayerFirstName.text;
        string lastName = PlayerLastName.text;
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            ErrorText.gameObject.SetActive(true);
        }
        else
        {     
            ErrorText.gameObject.SetActive(false);
            Preventer.gameObject.SetActive(true);
            ConfirmationPanel.gameObject.SetActive(true);
            ConfirmationPanel.SetTrigger("open");
            ConfirmationText.text = "Confirm your name " + "\"" + firstName.Trim() + " " + lastName.Trim() + "\"" + "?";

        }

    }
    public void Confirm()
    {
        string firstName = PlayerFirstName.text.Trim();
        string lastName = PlayerLastName.text.Trim();
        StatsData statsList = SaveSystem.LoadStats();
        statsList.firstName = firstName;
        statsList.lastName = lastName;
        statsList.fullname = firstName + "_" + lastName;
        SaveSystem.SaveStats(statsList);
        SecondDialog.GetComponent<Image>();
        SecondDialog.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void Edit()
    {
        Preventer.gameObject.SetActive(false);
        ConfirmationPanel.SetTrigger("close");
        ConfirmationPanel.gameObject.SetActive(false);
    }
}
