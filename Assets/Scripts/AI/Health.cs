using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float MaxHealth;

    [SerializeField]private float currentHealth;

    /// <summary>
    /// Normalized value means a ratio between two numbers
    /// </summary>
    public float NormalizedCurrentHealth
    {
        get { return currentHealth / MaxHealth;  }
    }

    /// <summary>
    /// Returns a boolean once the health reaches 0 or below 0
    /// </summary>
    public bool IsDead
    {
        get
        {
            return currentHealth <= 0;
        }
    }


    void Start()
    {
        currentHealth = MaxHealth;   // Assigns the currentHealth to MaxHealth at the start of the program to give it the value 
    }
}
