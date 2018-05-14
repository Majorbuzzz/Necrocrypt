//using UnityEngine;

//public class SlingGoblin : Monster
//{
//    public Rigidbody2D _slingRockPrefab;
//    private bool _isSlingRecharged;
//    private bool _isAttacking;
//    private float timer = 1f;
        
//    internal override void UpdateOverride()
//    {
//        if (!_isSlingRecharged)
//            timer -= Time.deltaTime;
//        if (timer <= 0)
//        {
//            _isSlingRecharged = true;
//            timer = 5F;
//        }
//        base.UpdateOverride();
//    }

//    internal override void PlayerIsInRange(GameObject playerObject)
//    {
//        if (!_isAttacking)
//        {
//            Animator.SetBool("PlayerIsInRange", true);
//            _isAttacking = true;
//        }
//        if (_isSlingRecharged)
//        {
//            var slingLocation = new Vector3(transform.localPosition.x - 0.22f, transform.localPosition.y - 0.22f, 0);
//            Rigidbody2D slingRock = Instantiate(_slingRockPrefab, slingLocation, Quaternion.identity);
//            slingRock.velocity = new Vector2(-6f, 7);
//            _isSlingRecharged = false;
//        }

//    }

//    internal override void Die()
//    {
//    }
//}
