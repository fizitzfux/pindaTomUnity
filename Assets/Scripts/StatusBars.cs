using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBars : MonoBehaviour
{   
    // REFERENCES
    public Text healthBar;
    public Health health;

    // VARIABLES


    public void Start()
    {
        healthBar.text = "Health: " + health.currentHealth.ToString();
    }

    public void statusBars()
    {
       health = GetComponent<Health>();
       healthBar.text = "Health: " + health.currentHealth.ToString();
    }

    public void update()
    {
        statusBars();
    }
}
