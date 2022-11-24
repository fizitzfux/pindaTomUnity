using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuText : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // add callbacks in the inspector like for buttons

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name + " Game Object Clicked!", this);
        if (name == "SinglePlayerButton")
        {
            SceneManager.LoadScene("Main");
        }
        if (name == "ExitButton") { Application.Quit(); }
        
    }
}
