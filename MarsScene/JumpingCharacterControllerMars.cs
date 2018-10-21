using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class JumpingCharacterControllerMars : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    public bool isFacingRight = true;
    public int jumpForce = 100;
	public Text countText;
	public Text error;
	public TextMeshProUGUI Welcome;
	public TextMeshProUGUI Instruc;
	public TextMeshProUGUI WelcomeJup;

	private int count;
    private bool isJumpPressed = false;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    // Awake is used to initialize any variables before the game starts
    // Called after all objects are initialized so you can safely communicate with other objects
    //
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called when a script is enabled. If you need to make sure something is initialized
    // put it in awake.
    //
    void Start()
    {
		count = 0;
		countText.text = "";
		SetCountText ();
		error.text = "";

		Welcome.text = "Welcome to Mars.";
		Instruc.text = "Collect all the gems to powerup your ship!";

		WelcomeJup.text = "Welcome to Jupiter.";
    }

    // Update is called once per frame
    void Update()
    {
        float vSpeed = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxisRaw("Horizontal");
        isJumpPressed = Input.GetButtonDown("Jump");

        // if the input is to the left
        if (vSpeed < 0.0)
        {
            // if the player is facing right
            if (isFacingRight)
            {
                // tell the sprite renderer to flip along the X-axis
                spriteRenderer.flipX = true;

                // the player is no longer facing right
                isFacingRight = false;
            }
        }
        else if (vSpeed > 0.0)
        {
			Welcome.text = "";
			Instruc.text = "";
			WelcomeJup.text = "";

            if (!isFacingRight)
            {
                // tell the sprite renderer to stop flipping along the X-axis
                // this should have the player facing right
                //
                spriteRenderer.flipX = false;

                // the player is facing right again
                isFacingRight = true;

            }
        }


        // we want the absolute value of the vSpeed for our animations since we want to 
        // use the 'negative' speed to just be speed in the left direction
        //

        if (vInput != 0.0f)
        {
            animator.SetFloat("xVelocity", Mathf.Abs(vSpeed));
        }
        else
        {
            animator.SetFloat("xVelocity", 0.0f);
        }
        animator.SetFloat("xInput", vInput);



        rb.velocity = new Vector2(vSpeed * playerSpeed, rb.velocity.y);
    }

    // Called multiple times per frame depending on the frame rate
    // locked in sync with the physics engine so physics manipulation
    // should take place here - particularly with RigidBodies
    void FixedUpdate()
    {
        // if the player pressed the jump button
        // apply a rigidbody force to launch the player up in the air
        //
        if ( isJumpPressed )
        {
			Debug.Log ("is this still active?");
            // up direction with a magnitude of jumpForce
            rb.AddForce(Vector2.up * jumpForce );
			animator.SetTrigger ("isJumping");

            // player event is consumed
            isJumpPressed = false;
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Rock")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
		if (other.gameObject.CompareTag ("Fire")) {


				animator.SetTrigger ("isDead");
				SceneManager.LoadScene ("Final Game Over");


		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("FallingRock"))
		{

				animator.SetTrigger ("isDead");
				SceneManager.LoadScene ("Final Game Over");

		}

		if (other.gameObject.CompareTag ("marsrocket"))
		{
			if (count == 10) 
			{
				Application.LoadLevel ("OriginalSceneUpdatedmarstojup");
			} 
			else 
			{
				error.text = "You need to collect all 10 gems!";
			}
		}
		if (other.gameObject.CompareTag ("juprocket"))
		{
			if (count >= 0) 
			{
				Application.LoadLevel ("OriginalSceneUpdatedafterjup");
			} 
			else 
			{
				error.text = "You need to collect all 10 gems!";
			}
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		error.text = "";
	}
		
	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString () + " out of 10";
	}
		
}