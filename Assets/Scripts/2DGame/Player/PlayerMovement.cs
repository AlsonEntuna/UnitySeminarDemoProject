using UnityEngine;
using System.Collections;

/// <summary>
/// Used to control the movement of the player for a 2D platformer game
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;

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
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded) jump = true;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rigidBody2D.velocity.x < maxSpeed)
            rigidBody2D.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rigidBody2D.velocity.x) > maxSpeed)
            rigidBody2D.velocity = new Vector2(Mathf.Sign(rigidBody2D.velocity.x) * maxSpeed, rigidBody2D.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("Jump");
            rigidBody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
