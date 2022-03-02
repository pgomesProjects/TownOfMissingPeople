using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;
    private float horizontal;
    private float speed = 8f;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InGameManager.instance.Paused)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            playerAnim.SetFloat("SpeedX", Mathf.Abs(horizontal));

            //If the input is moving the player right and the player is facing left
            if (horizontal > 0 && !isFacingRight)
                Flip();

            //If the input is moving the player left and the player is facing right
            else if (horizontal < 0 && isFacingRight)
                Flip();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
