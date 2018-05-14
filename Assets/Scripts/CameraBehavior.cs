using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject Player;
    private Vector2 _velocity;
    public float SmoothTimeX;
    public float SmoothTimeY;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x+2, ref _velocity.x, SmoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y+2, ref _velocity.y, SmoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
