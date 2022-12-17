using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue3 : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text DialogHolder;
    public string[] Lines;
    public float TextSpeed;

    [Header("Animators")]
    public Animator PointLeft;

    [Header("Buttons")]
    public Button[] ButtonGroup;

    [Header("Image")]
    public Image PreventButtonClick;

    private int index;
    void Start()
    {
        DialogHolder.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (GuideNavigate.initChild == GuidesChild.transform.childCount - 1)
        {
            NextLine();
        }
    }*/
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
                PointLeft.gameObject.SetActive(true);
                PreventButtonClick.gameObject.SetActive(false);
                foreach (Button item in ButtonGroup)
                {
                    item.interactable = false;
                }
            }
        }
        else
        {
            /*PointLeft.gameObject.SetActive(true);
            PreventButtonClick.gameObject.SetActive(false);
            foreach (Button item in ButtonGroup)
            {
                item.interactable = false;
            }*/
        }
    }
}
