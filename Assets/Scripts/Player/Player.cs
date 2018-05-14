using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BoxCollider2D BodyCollider { get; private set; }
    public float jumpHeight = 2;
    public float timeToJumpApex = .4f;
    public float jumpTime;
    public float jumpTimeCounter;

    private Animator animator;
    private PlayerHealth Health { get; set; }
    Movement movement;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;
    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;
    private bool facingRight;

    private SpriteRenderer mySpriteRenderer;
    private Immunity immunity;
    private bool canJump;

    internal Sword Weapon { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public bool JumpKeyIsDown { get; private set; }
    public bool JumpKeyIsUp { get; private set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        Health = GetComponent<PlayerHealth>();
        BodyCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        immunity = new Immunity(mySpriteRenderer);
        Health.CurrentValue = 3;
    }

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<Movement>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }


    private void Update()
    {
        immunity.DoYourThing();
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        JumpKeyIsDown = Input.GetKey(KeyCode.Space);
        JumpKeyIsUp = Input.GetKeyUp(KeyCode.Space);

        if (movement.collisions.below)
        {
            jumpTimeCounter = jumpTime;
            canJump = true;
        }

        Attack();
        Crouch();
    }

    private void FixedUpdate()
    {
        if (movement.collisions.above || movement.collisions.below)
            velocity.y = 0;


        Jump();
        Move();
        Flip();
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            animator.SetBool("IsCrouching", true);
        else if (Input.GetKeyUp(KeyCode.DownArrow))
            animator.SetBool("IsCrouching", false);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            animator.SetBool("Attack", true);
        if (Input.GetKeyUp(KeyCode.LeftControl))
            animator.SetBool("Attack", false);
    }

    private void Flip()
    {
        if (MovementInput.x < 0.0f && facingRight == false)
            FlipPlayer();
        else if (MovementInput.x > 0.0f && facingRight)
            FlipPlayer();
    }

    private void Move()
    {
        float targetVelocityX = MovementInput.x == 0 ? 0 : MovementInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (movement.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += movement.collisions.below ? 0 : gravity * Time.deltaTime;
        movement.Move(velocity * Time.deltaTime);

        bool isWalking = MovementInput.x != 0.0f;
        animator.SetBool("IsWalking", isWalking);
    }

    private void Jump()
    {
        if (JumpKeyIsDown && canJump)
        {
            if (jumpTimeCounter > 0)
            {
                velocity.y = jumpVelocity;
                jumpTimeCounter -= Time.deltaTime;
                canJump = true;
                animator.SetBool("IsJumping", true);
            }
        }
        if (JumpKeyIsUp)
        {
            canJump = false;
            jumpTimeCounter = 0;
        }

        if (movement.collisions.below && velocity.y == 0)
            animator.SetBool("IsJumping", false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("sword"))
        {
            other.gameObject.SetActive(false);
            animator.SetTrigger("GotSword");
            animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("player_sword");
            Weapon = new Sword();
        }

        if (other.gameObject.CompareTag("Enemy") && immunity.NotImmune)
        {
            Health.ChangeHealth(-.25f);
            animator.SetTrigger("Hurt");
            var direction = facingRight ? 1 : -1;
            velocity.x = direction * 8f;
            movement.Move(velocity * Time.deltaTime);
            immunity.Start(2);

            Weapon weapon = other.GetComponent<Weapon>();
            if (weapon != null)
                weapon.TriggerWeaponHit();
        }
    }

    private void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
