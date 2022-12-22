using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // EVENT HANDLER
    public delegate void HealthChangedHandler(object source, float oldHealth, float newHealth);
    public event HealthChangedHandler OnHealthChanged;

    // VARIABLES
    [SerializeField] private float maxHealth;
    [SerializeField] private float testdamage = -5f;
    [SerializeField] private float testhealing = 5f;
    [SerializeField] float currentHealth;

    // REFERENCES
    public float CurrentHealth => currentHealth;

    public void Start()
    {
        // Zet je health naar de maxHealth
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float amount)
    {
        // Zorgt dat je nieuwe health de current health wordt en dat dit alsnog kan veranderen door de invoke functie
        float oldHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(this, oldHealth, currentHealth);
    }



    private void Update()
    {
        // Healing toevoegen en healing afhalen
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ChangeHealth(testhealing);
            Debug.Log("Health is " + currentHealth);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangeHealth(testdamage);
            Debug.Log("Health is " + currentHealth);
        }

        if(currentHealth <= 0f)
        {
            // Teleport player to spawning area and disable damage
            this.GetComponent<playerController>().Teleport(new Vector3(0, 1.5f, 0));
            this.GetComponent<Health>().enabled = false;
        }
    }
}