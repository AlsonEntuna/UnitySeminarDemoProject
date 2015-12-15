using UnityEngine;
using System.Collections;

/// <summary>
/// Used to control the movement of the player for a 2D platformer game
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 365f; // The speed of the player
    public float MaxSpeed = 5f; // Max speed that the player will be able to move at
    public float JumpForce = 1000f; // The force that will be applied in order for the player to jump
    public Transform GroundCheckObject; // The GameObject that will be responsible for detecting the platform

    [Header("Private Attributes")]
    [SerializeField]
    private bool facingRight = true;
    [SerializeField]
    private bool jump = false;
    [SerializeField]
    private bool grounded = false;

    // Components
    Animator anim;
    Rigidbody2D rigidBody2D;

    void Awake()
    {
        // GetComponent is used in order for us to get reference of the Component itself
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Sets the boolen if the object detects a platform in order for the code to enable the jump
        // if grounded is false meaning that the player could not jump
        // if otherwise then the player has the ability to jump
        grounded = Physics2D.Linecast(transform.position, GroundCheckObject.position, 1 << LayerMask.NameToLayer("Ground"));

        // Gets the input of the player to tell the code that the player wants to jump
        if (Input.GetButtonDown("Jump") && grounded) jump = true;

        // Sets the Animator data of "Grounded" to grounded
        anim.SetBool("Grounded", grounded);
    }

    /// <summary>
    /// Fixed update is used to calculate physic based calculations
    /// </summary>
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal"); // Gets the input of the player
        Debug.Log("Speed: " + Mathf.Abs(h));
        anim.SetFloat("Speed", Mathf.Abs(h)); // Sets the data that is needed for the Animator

        if (h * rigidBody2D.velocity.x < MaxSpeed)
            rigidBody2D.AddForce(Vector2.right * h * MovementSpeed); // Apply force to the character for it to move based on the max speed

        if (Mathf.Abs(rigidBody2D.velocity.x) > MaxSpeed)
            rigidBody2D.velocity = new Vector2(Mathf.Sign(rigidBody2D.velocity.x) * MaxSpeed, rigidBody2D.velocity.y); // if the velocity is higher than the max speed the we assign it to not exceed the max speed

        // Flipping the sprite of the character in order to correct its orientation
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        // Jump Algorithm
        if (jump)
        {
            rigidBody2D.AddForce(new Vector2(0f, JumpForce));
            jump = false;
        }
    }

    /// <summary>
    /// This functions is used to flip the sprite once the player is moving left or right
    /// depending on the orientation of the character
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
