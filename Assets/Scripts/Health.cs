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
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float amount)
    {
        float oldHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(this, oldHealth, currentHealth);
    }



    private void Update()
    {
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
    }
}