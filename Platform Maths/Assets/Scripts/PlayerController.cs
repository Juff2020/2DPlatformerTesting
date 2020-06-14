using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float movementForce;
    
    public float creepForce;

    public float jumpForceImpulse;

    public float airJumpForceImpulse;
    public int totalAirJumps;
    public int airJumpsRemaining;
    public bool airControlAllowed;

    [Range(0.01f, 1f)]
    public float airControlSensitivity;

    public float jetpackTriggerDown;
    public float jetpackForce;
    public float jetpackFuel;
    public float jetpackFuelRemaining;
    public float jetpackRefuelSpeed;

    private Rigidbody2D rb;

    public float jumpPressedRememberStart;
    public float jumpPressedRememberRemaining;

    public float groundedRememberStart;
    public float groundedRememberRemaining;

    public bool jumpActive = false;

    public bool isGrounded;
    public bool wasGrounded;

    private bool facingRight;

    public float maxDashTime; //max time between button presses
    public float lastDashPress;
    public float timeSinceLastDashPress;
    public float dashForce;

    private float horizontal;

    public ShootAtMousePosition shootAtMousePosition;

    public Animator animator;

    public bool isJumping;
    public bool isJetpacking;
    public bool isFalling;

    public float playerHealthStart;
    public float playerHealthRemaining;

    public GameObject pausePanel;
    public bool showPausePanel;

    public float fallVelocity;
    public float fallVelocityOnImpact;
    public float maxSafeFallSpeed;
    public float fallDamageMultiplier;

    public int playerScore;
    public int levelCompleteScore;
    public GameObject levelCompletePanel;
    public Text playerScoreText;

    public int starUpgrades;

    public GameObject controlsPanel;
    public bool controlsPanelIsShowing;

    public deathByFallDamageScript deathByFallDamageScript;

        // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jetpackFuelRemaining = jetpackFuel;
        playerHealthRemaining = playerHealthStart;
        showPausePanel = false;
        pausePanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        playerScore = 0;
        controlsPanel.SetActive(false);
        controlsPanelIsShowing = false;
    }


    void Update()
    {
        jetpackTriggerDown = Input.GetAxisRaw("Jetpack");

        playerScoreText.text = playerScore.ToString();

        if (playerScore == levelCompleteScore)
        {
            LevelComplete();
        }

        if (Input.GetKeyDown(KeyCode.C) && controlsPanelIsShowing == false)
        {
            ShowControlsPanel();
        }
        else if (Input.GetKeyDown(KeyCode.C) && controlsPanelIsShowing == true)
        {
            CloseControlsPanel();
        }


        //get player horizontal input. If airControlAllowed is of then control will deactivate when not grounded

        if (isGrounded == true && airControlAllowed == false)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        else if (airControlAllowed == true)
        {
            if (isGrounded == false)
            {
                horizontal = Input.GetAxisRaw("Horizontal") * airControlSensitivity;
            }
            else
            {
                horizontal = Input.GetAxisRaw("Horizontal");
            }
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        HandleMovement(horizontal);

        //impact force calculation
        fallVelocity = rb.velocity.y;

        //flip player to they face in the direction they are moving
        if (horizontal < 0 && !facingRight)
        {
            flip();
        }
        else if (horizontal > 0 && facingRight)
        {
            flip();
        }

        //usejetpack

        /*
        if ((Input.GetMouseButtonDown(1) || jetpackTriggerDown == 1) && jetpackFuelRemaining >= 0 && starUpgrades >= 5)
        {
            SoundManagerScript.PlaySound("jetpack");
        }
        */

        if ((Input.GetMouseButton(1) || jetpackTriggerDown == 1) && starUpgrades >= 5)
        {
            if (jetpackFuelRemaining >= 0)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                animator.SetBool("isJetpacking", true);
                jetpackFuelRemaining -= Time.deltaTime;
                rb.AddForce(new Vector2(0, jetpackForce), ForceMode2D.Force);
            }           
        }

        //jetpack refueling
        if (isGrounded == true)
        {
            if (jetpackFuelRemaining <= jetpackFuel)
            {
                jetpackFuelRemaining = jetpackFuelRemaining += ((Time.deltaTime/10)*jetpackRefuelSpeed);
            }
        }

        //If player presses jump just before reaching the ground, remember to jump on ground impact

        //timer counting down from jump button press.
        jumpPressedRememberRemaining -= Time.deltaTime;

        //check if player presses "jump" button
        if (Input.GetButtonDown("Jump") && starUpgrades >= 1)
        {
            jumpPressedRememberRemaining = jumpPressedRememberStart;
            jumpActive = true;
        }

        // Jump only if "jumpActive" is true, "isGrounded" is true, and jumper timer greater than zero 
        if (jumpActive == true && wasGrounded == true && jumpPressedRememberRemaining >= 0 && starUpgrades >= 2)
        {
            SoundManagerScript.PlaySound("jump");
            jumpPressedRememberRemaining = 0; //reset jump timer to zero
            jumpActive = false; //change
            wasGrounded = false;
            Jump(); //perform jump
        }

        if (jumpPressedRememberRemaining <= 0)
        {
            jumpActive = false;
        }



        //if player presses jump just after leaving an edge then still jump (AKA "Coyote Time")

        //timer counting down from when player was last on ground
        groundedRememberRemaining -= Time.deltaTime;

        if (groundedRememberRemaining >= 0)
        {
            wasGrounded = true;
        }

        if (isGrounded == true)
        {
            groundedRememberRemaining = groundedRememberStart;
        }

        if (groundedRememberRemaining <= 0)
        {
            wasGrounded = false;
        }
        



        // AIR JUMPING
        //check player is not on ground, if the jump button is pressed, and air jumps remaning is great than zero.
        if (isGrounded == false && jumpActive == true && airJumpsRemaining > 0 && starUpgrades >= 3)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJetpacking", false);
            animator.SetBool("isJumping", true);
            airJump();
            jumpActive = false;
        }

        //dash
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Dash")) && starUpgrades >= 4)
        {
            lastDashPress = Time.time;
            
            if (timeSinceLastDashPress <= lastDashPress)
            {
                rb.AddForce(new Vector2(horizontal * (movementForce*dashForce), rb.velocity.y));
                timeSinceLastDashPress = lastDashPress + maxDashTime;

            }
        }





        //creep

        if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Creep"))
        {
            movementForce = movementForce / creepForce;
        }
        if (Input.GetButtonUp("Vertical") || Input.GetButtonUp("Creep"))
        {
            movementForce = movementForce * creepForce;
        }      


        if (jetpackFuelRemaining <= 0.1 && isGrounded == false)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isJetpacking", false);
            animator.SetBool("isFalling", true);
        }

        
        // Show/Hide Pause Panel

        if (playerHealthRemaining <= 0)
        {
            deathByFallDamageScript.isDeadByFallDamage = true;
            deathByFallDamageScript.ShowDeathByFallDamagePanel();

        }

        if (Input.GetKeyDown(KeyCode.Escape) && showPausePanel == false)
        {
            showPausePanel = true;
            pausePanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && showPausePanel == true)
        {
            pausePanel.SetActive(false);
        }

    }

    private void HandleMovement(float horizontal)
    {
        rb.AddForce(new Vector2(horizontal * movementForce , rb.velocity.y));
    }

    //face player in correct direction
    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump()
    {
        animator.SetBool("isFalling", false);
        animator.SetBool("isJetpacking", false);
        animator.SetBool("isJumping", true);
        rb.AddForce(new Vector2(0, jumpForceImpulse), ForceMode2D.Impulse);
        isJumping = true;
    }


        private void airJump()
    {
        SoundManagerScript.PlaySound("airJump");
        rb.AddForce(new Vector2(0, airJumpForceImpulse), ForceMode2D.Impulse);
        airJumpsRemaining--;
    }

    

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
    }

    public void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowControlsPanel()
    {
        controlsPanelIsShowing = true;
        controlsPanel.SetActive(true);
    }

    public void CloseControlsPanel()
    {
        controlsPanelIsShowing = false;
        controlsPanel.SetActive(false);
    }
}
