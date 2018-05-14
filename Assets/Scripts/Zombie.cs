using System;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveSpeed = 1;
    protected Vector3 Velocity;
    protected Movement Movement;
    protected Animator Animator;
    public AnimationClip[] AnimationClips;
    protected AnimatorOverrideController animatorOverrideController;

    float accelerationTimeGrounded = .1f;
    float velocityXSmoothing;
    protected bool FacingRight;

    Vector3 touchPosWorld;
    private Jobs job;

    private enum Jobs
    {
        None = 0,
        Digger = 1,

    }

    private void Start()
    {
        FacingRight = true;
        Movement = GetComponent<Movement>();
        Animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(Animator.runtimeAnimatorController);
        Animator.runtimeAnimatorController = animatorOverrideController;
    }

    private void Update()
    {
        DoYourJob();
        CheckTouch();
        MoveFoward();
        if (Movement.collisions.right || Movement.collisions.left)
            Flip();
    }

    private void DoYourJob()
    {
        switch (job)
        {
            case Jobs.Digger:
                Animator.SetBool("Digging", true);
                moveSpeed = 0;

                break;
        }
    }

    private void CheckTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null)
            {
                if (hitInformation.transform.gameObject == transform.gameObject)
                    animatorOverrideController["Zombie_Moving"] = AnimationClips[1];
            }
        }
    }

    private void MoveFoward()
    {
        float direction = FacingRight ? 1 : -1;
        float targetVelocityX = moveSpeed * direction;
        Velocity.x = Mathf.SmoothDamp(Velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
        Movement.Move(Velocity * Time.deltaTime);
    }


    protected virtual void Flip()
    {
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        FacingRight = !FacingRight;
    }
}
