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
    private GameObject currentObject;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Interact.performed += _ => PlayerInteract();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.Paused && !GameManager.instance.interactionActive)
        {
            horizontal = playerControls.Player.Move.ReadValue<Vector2>().x;

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

    public void PlayerInteract()
    {
        Debug.Log("Interact Button Called!");

        if(currentObject != null && !GameManager.instance.interactionActive)
        {
            if (currentObject.CompareTag("LevelLoader"))
            {
                GameManager.instance.interactionActive = true;
                rb.velocity = new Vector2(0, rb.velocity.y);
                playerAnim.SetFloat("SpeedX", 0);
                currentObject.GetComponentInChildren<NewLevelController>().LoadToNextLevel();
            }

            if (currentObject.CompareTag("NPC"))
            {
                Debug.Log("NPC Interaction Start!");
                GameManager.instance.interactionActive = true;
                rb.velocity = new Vector2(0, rb.velocity.y);
                playerAnim.SetFloat("SpeedX", 0);
                currentObject.GetComponentInChildren<NPCController>().StartDialog();
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentObject = collision.gameObject;
    }
}
