// Decipricated, gonna phase out soon ~Jip

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour, IPointerClickHandler
{
    // Variables
    public string function = "";
    private string instruction = "";
    private string parameters = "";

    // When click interaction on text
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Separate intruction command from parameters
        if (function.IndexOf(" ") > 0) {
            instruction = function.Substring(0, function.IndexOf(" "));
            parameters = function.Substring(function.IndexOf(" ") + 1);
        }
        else {
            instruction = function;
        }
        // If instruction "load" then load scene "parameters"
        if (instruction == "load") {
            SceneManager.LoadScene(parameters);
        }
        // If instruction "exit" then exit application
        else if (instruction == "exit") {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }

    }
}