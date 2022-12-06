using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour, IPointerClickHandler
{

    public string function = "";
    private string instruction = "";
    private string parameters = "";

    void Start()
    {

    }

    //vangt het event af wanneer de gebruiker op een stuk tekst klikt
    public void OnPointerClick(PointerEventData pointerEventData)
    {

        Debug.Log(name + " => " + function, this);

        if (function.IndexOf(" ") > 0) {
            instruction = function.Substring(0, function.IndexOf(" "));
            parameters = function.Substring(function.IndexOf(" ") + 1);
        }

        else {
            instruction = function;
        }

        if (instruction == "load") {
            SceneManager.LoadScene(parameters);
        }

        else if (instruction == "exit") {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }

    }
}
