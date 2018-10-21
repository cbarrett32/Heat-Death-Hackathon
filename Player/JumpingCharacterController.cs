using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class JumpingCharacterController : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    public bool isFacingRight = true;
    public int jumpForce = 100;
	public Text countText;
	public Text error;
	public TextMeshProUGUI Welcome;
	public TextMeshProUGUI Instruc;
	public TextMeshProUGUI WelcomeJup;

	private Transform transform;
	public int count;
    private bool isJumpPressed = false;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
	private Vector3 scaleDirection;
	private GameObject marsbutton;
	private GameObject jupiterButton;
	private AudioSource myAudioSource;
	private bool audioIsPlaying = false;
	protected int health =3;
	private Scene scene;


    // Awake is used to initialize any variables before the game starts
    // Called after all objects are initialized so you can safely communicate with other objects
    //
    void Awake()
    {
		transform = GetComponent<Transform> ();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		scaleDirection = transform.localScale;
		scene = SceneManager.GetActiveScene();
		if (scene.name == "OriginalSceneUpdated"||scene.name == "OriginalSceneUpdatedmarstojup" || scene.name == "OriginalSceneUpdatedafterjup") {
			marsbutton = GameObject.FindGameObjectWithTag ("HopToMars");
			marsbutton.transform.localScale = Vector3.zero;
			jupiterButton = GameObject.FindGameObjectWithTag ("HopToJupiter");
			jupiterButton.transform.localScale = Vector3.zero;
		}
		myAudioSource = GetComponent<AudioSource> ();

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

	void OnTriggerEnter2D(Collider2D collider)
	{
		if ((collider.tag == "DeadlyThing")&&(animator.GetBool("HasDied")==false)) {
			health--;
			//if (health == 0) {
			Debug.Log(scene.name + " hi");
				animator.SetBool ("isDead", true);
			//}
		}

		if (collider.gameObject.CompareTag ("Rock")) 
		{
			collider.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
		if (collider.gameObject.CompareTag ("Fire")) {
			health--;
			//if (health == 0) {
				animator.SetTrigger ("isDead");
				SceneManager.LoadScene ("Final Game Over");
			//}


		}

	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.tag == "Mars") {


			marsbutton.transform.localScale = Vector3.one;
		}

		if (collider.tag == "Jupiter") {


			jupiterButton.transform.localScale = Vector3.one;
		}

	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.tag == "Mars") {
			marsbutton.transform.localScale = Vector3.zero;

		}

		if (collider.tag == "Jupiter") {
			jupiterButton.transform.localScale = Vector3.zero;

		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("FallingRock"))
		{
			health--;
			//if (health == 0) {
				animator.SetTrigger ("isDead");
				SceneManager.LoadScene ("Final Game Over");
			//}

		}

		if (other.gameObject.CompareTag ("marsrocket"))
		{
			Debug.Log ("count is " + count);
			if (count >= 4) 
			{
				Application.LoadLevel ("OriginalSceneUpdatedmarstojup");
			} 
			else 
			{
				error.text = "You need to collect at least 4 gems!";
			}
		}
		if (other.gameObject.CompareTag ("juprocket"))
		{
			if (count >= 4) 
			{
				Application.LoadLevel ("YouWin");
			} 
			else 
			{
				error.text = "You need to collect at least 4 gems!";
			}
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		error.text = "";
	}
		


    // Update is called once per frame
    void Update()
    {

        float vSpeed = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxisRaw("Horizontal");
        isJumpPressed = Input.GetButton("Jump");

        // if the input is to the left
        if (vSpeed < 0.0)
        {
            // if the player is facing right
            if (isFacingRight)
            {
                // tell the sprite renderer to flip along the X-axis
                //spriteRenderer.flipX = true;
				transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
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

                //spriteRenderer.flipX = false;
				transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                // the player is facing right again
                isFacingRight = true;

            }
        }

		if (animator.GetBool ("isDead") == true) {
			SceneManager.LoadScene ("Final Game Over");

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
		if (scene.name == "OriginalSceneUpdated"||scene.name == "OriginalSceneUpdatedmarstojup") {
			Debug.Log ("a;lskdfj");
			if (isJumpPressed) {
				// up direction with a magnitude of jumpForce
				rb.AddForce (Vector2.up * jumpForce);
				//animator.SetTrigger ("isJumping");

	
				// player event is consumed
				isJumpPressed = false;
				Input.GetButtonUp ("Jump");
				rb.AddForce (Vector2.up / jumpForce);

			}
			if (Input.GetButtonDown ("Jump")){
				myAudioSource.Play();
			} if(Input.GetButtonUp ("Jump")){
				myAudioSource.Stop(); 
			}
		} else {
			if ( isJumpPressed )
			{
				Debug.Log ("otherwise");
				// up direction with a magnitude of jumpForce
				rb.AddForce(Vector2.up * jumpForce );
				//animator.SetTrigger ("isJumping");

				// player event is consumed
				isJumpPressed = false;
			}
		}






	

		if (animator.GetBool ("isDead") == true) {
			animator.SetBool ("HasDied", true);
		}
    }

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString () + " out of 10";
	}
}

