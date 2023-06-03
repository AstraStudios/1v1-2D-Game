using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 15f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private string horizontalInputAxis;
    private string jumpInputButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetPlayerControls();
    }

    void SetPlayerControls()
    {
        if (transform.parent.name == "PlayerOne")
        {
            horizontalInputAxis = "Horizontal_P1";
            jumpInputButton = "Vertical_P1";
        }
        else if (transform.parent.name == "PlayerTwo")
        {
            horizontalInputAxis = "Horizontal_P2";
            jumpInputButton = "Jump_P2";
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis(horizontalInputAxis);
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        if (Input.GetButtonDown(jumpInputButton) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            isGrounded = false;
        }

        if (rb.velocity.y < 0 && isJumping)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * 2.5f * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
}
