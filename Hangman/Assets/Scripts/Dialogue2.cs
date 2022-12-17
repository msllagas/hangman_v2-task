using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue2 : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text DialogHolder;
    public string[] Lines;
    public float TextSpeed;
    public Button PlayButton;
    public GameObject GuidesChild;

    [Header("Animator")]
    public Animator PointDown;
    public Animator PointRight;

    private int index;
    void Start()
    {
        DialogHolder.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (GuideNavigate.initChild == GuidesChild.transform.childCount - 1)
        {
            NextLine();
        }
    }
    public void NextDialog()
    {
        if (DialogHolder.text == Lines[index])
        {  
            if (GuideNavigate.initChild == GuidesChild.transform.childCount - 1)
            {
                NextLine();
            }
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
        }
        else
        {
            PlayButton.gameObject.SetActive(true);
            PointDown.gameObject.SetActive(false);
            PointRight.gameObject.SetActive(true);
        }
    }
}
