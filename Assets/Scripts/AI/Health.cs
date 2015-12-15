using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float MaxHealth;

    private float currentHealth;

    public float NormalizedCurrentHealth
    {
        get { return currentHealth / MaxHealth;  }
    }

    public bool IsDead
    {
        get
        {
            return currentHealth <= 0;
        }
    }


    void Start()
    {
        currentHealth = MaxHealth;
    }
}
