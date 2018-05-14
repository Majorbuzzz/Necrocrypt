//using UnityEngine;

//public class SpearGoblin : MeleeGoblin
//{
//    Collider2D spearCollider;
//    private float breakingSpeedInterpolation = 0.0f;

//    internal override void StartOverride()
//    {
//        spearCollider = GetComponentInChildren<Collider2D>();
//        Weapon.WeaponHit += () => SpearHit();
//        base.StartOverride();
//    }

//    private void SpearHit()
//    {
//        CurrentState = State.Breaking;
//    }

//    internal override void PlayerIsInRange(GameObject playerObject)
//    {
//        if (CurrentState == State.Breaking)
//        {
//            if (Velocity.x <= 0.5 && FacingRight)
//                Flip();
//            if (Velocity.x >= -0.5 && !FacingRight)
//                Flip();
//            else
//                SlowlyFlipTowardPlayer(playerObject);
//        }

//        base.PlayerIsInRange(playerObject);
//    }

//    protected override void Flip()
//    {
//        if (Velocity.x <= 0.5 && FacingRight)
//        {
//            breakingSpeedInterpolation = 0.0f;
//            base.Flip();
//        }
//        else if (Velocity.x >= -0.5 && !FacingRight)
//        {
//            breakingSpeedInterpolation = 0.0f;
//            base.Flip();
//        }
//        else
//            Break();

//    }

//    private void SlowlyFlipTowardPlayer(GameObject playerObject)
//    {
//        float playerDirection = Mathf.Sign(playerObject.transform.position.x - transform.position.x);

//        if (playerDirection == 1 && !FacingRight)
//            Break();
//        else if (playerDirection == -1 && FacingRight)
//            Break();

//        Velocity.x = Mathf.Lerp(Velocity.x, 0, breakingSpeedInterpolation);
//        breakingSpeedInterpolation += 0.05f * Time.deltaTime;
//        Movement.Move(Velocity * Time.deltaTime);
//    }

//    private void Break()
//    {
//        CurrentState = State.Breaking;
//        Animator.SetBool("IsBreaking", true);
//    }
//}
