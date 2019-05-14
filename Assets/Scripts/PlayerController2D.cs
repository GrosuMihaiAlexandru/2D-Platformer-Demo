using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    bool isGrounded;
    public bool isClimbing;

    public float distance;
    //Layer Masks for collisions
    public LayerMask whatIsLadder;

    // Raycasting for ground Check
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;

    public float runSpeed;
    public float jumpSpeed;

    //Climbing variables
    public bool onLadder;
    public float climbSpeed;
    private float climbVelocity;
    private float gravityStore;

    private bool jumped = false;
    public bool startedClimbing = false;

    // Damage and invincibility frames
    public bool damage; // Turns on when on collision with a damage source
    public bool invincible;  // Invincible while on
    public float maxInvincibilityPeriod; // Maximum invincibility time
    public float invincibilityTime; // Invincibility time left
    public float noInputTime; // Short time when you can't control the player.
    public float noInputDuration;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gravityStore = rb2d.gravityScale;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        //Detecting ground
        if (Physics2D.Linecast(transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground")))
        {
              isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        // Check if player can Move
        if (noInputDuration <= 0)
        {
            //Moving right
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

                if (isGrounded)
                    animator.Play("Player_run");
                else if (jumped == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_jump") && !isClimbing)
                    animator.Play("Player_fall");

                spriteRenderer.flipX = false;
            }
            //Moving left
            else if (Input.GetKey("a") || Input.GetKey("left"))
            {
                rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

                //Display running animation on ground and falling animation in the air
                if (isGrounded)
                    animator.Play("Player_run");
                //Falling only after jump animation finished and not while cimbing
                else if (jumped == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_jump") && !isClimbing)
                    animator.Play("Player_fall");

                spriteRenderer.flipX = true;
            }
            else
            {
                if (isGrounded)
                    animator.Play("Player_idle");

                rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            }

            //Jumping
            if (Input.GetKey("space"))
            {
                if (isGrounded)
                    if (jumped == false)
                    {
                        jumped = true;
                        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                        animator.Play("Player_jump");
                    }

            }
            else
            {
                jumped = false;
            }

            //Climbing
            if (onLadder)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    isClimbing = true;
                    if (startedClimbing == false)
                    {
                        startedClimbing = true;
                    }
                }
                else
                {
                    isClimbing = false;
                }

                if (isClimbing == true)
                {
                    rb2d.gravityScale = 0f;
                    climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
                    rb2d.velocity = new Vector2(rb2d.velocity.x, climbVelocity);
                    if (!isGrounded)
                        animator.Play("Player_climb");
                }
                else
                {
                    if (startedClimbing == true)
                        rb2d.velocity = Vector2.zero;
                }
            }
            else
            {
                rb2d.gravityScale = gravityStore;
                startedClimbing = false;
            }
            if (isGrounded)
                startedClimbing = false;

            
        }
        // Cooldowns 
        if (invincibilityTime > 0)
            invincibilityTime -= Time.deltaTime;

        if (noInputDuration > 0)
            noInputDuration -= Time.deltaTime;

        // DamageAnimation
        if (damage)
        {
            if(!invincible)
            {
                invincible = true;
                invincibilityTime = maxInvincibilityPeriod;
                animator.Play("Player_hurt");
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                noInputDuration = noInputTime;
                //Start the blinking during invincibility
                StartCoroutine(Blinking(maxInvincibilityPeriod/19, 0.2f));
                // Play Sound
                FindObjectOfType<AudioManager>().Play("HurtSound");
            }

        }
        if (invincibilityTime <= 0)
            invincible = false;

    }

    IEnumerator Blinking(float duration, float blinkTime)
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(blinkTime);
        }

        renderer.enabled = true;
    }
}
