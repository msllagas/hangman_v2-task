using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // 

public class DiffHandler : MonoBehaviour

{
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

    }

}
