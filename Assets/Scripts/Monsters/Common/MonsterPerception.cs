using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPerception : MonoBehaviour {

    public Monster Monster;

    private void OnTriggerStay2D(Collider2D other)
    {
        var playerIsInRange = other.gameObject.CompareTag("Player");

        if (playerIsInRange)
            Monster.PlayerIsInRange(other.gameObject);
    }
}
