using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBody : MonoBehaviour {

    public Monster Monster;

    private void OnTriggerStay2D(Collider2D other)
    {
        var playerAttack = other.gameObject.CompareTag("PlayerAttack");
        if (playerAttack)
            Monster.YouGotHurt(other.gameObject);     
    }
}
