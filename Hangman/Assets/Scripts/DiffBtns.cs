using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // 

public class DiffBtns : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonClicked()
    {
        DiffHandler.instance.LevelButton();
    }

}
