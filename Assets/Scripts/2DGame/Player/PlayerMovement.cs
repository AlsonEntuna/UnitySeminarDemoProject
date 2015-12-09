using UnityEngine;
using System.Collections;

/// <summary>
/// Used to control the movement of the player for a 2D platformer game
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    Animator anim;
    Rigidbody2D rigidBody;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
}
