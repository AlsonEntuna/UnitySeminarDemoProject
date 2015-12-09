using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Manages the Lives of the player in the game 
/// this is for easy management of data all throughout the game
/// </summary>
public class PlayerHealth : MonoBehaviour
{ 
    public int MaxLives; // Max Life of the player
    private int currentLives; // Current life of the player which will be modified all throughout the code

    [Header("UI Components")]
    public Text PlayerHealthUI;

    public bool IsAlive
    {
        get { return currentLives <= 0; }
    }
	void Start ()
    {
        currentLives = MaxLives; // sets the current lives to the Max Lives at the start of the game
	}

    void Update()
    {
        currentLives = Mathf.Clamp(currentLives, 0, MaxLives); // Makes sure that the current life doesn't go beyond the max life and below 0

        PlayerHealthUI.text = currentLives.ToString(); // Assigns the current life to the UI Component
    }

    /// <summary>
    /// Deducts a specific amount to the current life if called
    /// </summary>
    /// <param name="deduction"></param>
    public void DeductLife(int deduction)
    {
        currentLives -= deduction;
    }
}
