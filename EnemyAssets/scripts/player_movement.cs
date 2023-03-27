using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private float timer;
    

    Vector2 dir;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private Transform playerBulletPos;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown("space"))
        {
            shoot();
            Debug.Log("shooting");
        }


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    private void shoot()
    {
        Instantiate(playerBullet, playerBulletPos.position, Quaternion.identity);

    }
}
