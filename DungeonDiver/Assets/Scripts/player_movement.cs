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
    private SpriteRenderer sprite;
    private Animator anim;


    private float dirX;
    private float dirY;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private Transform playerBulletPos;

    private enum movementState { idle, running };
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
     
    }

    // Update is called once per frame
    private void Update()
    {


        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxis("Vertical");//Dirx holds whether the left or right arrow key is  being pressed. negative for left and positive for right

        rb.velocity = new Vector2(dirX * moveSpeed, dirY * moveSpeed); // dirx is mutlipled by , allowing the player to move

        if (Input.GetKeyDown("space"))
        {
            if(sprite.flipX == true)
            {
               
                Instantiate(playerBullet, playerBulletPos.position, playerBulletPos.rotation);
            }
            else if (sprite.flipX == false) 
            {
                
                Instantiate(playerBullet, playerBulletPos.position, playerBulletPos.rotation);
            }
        }

        timer += Time.deltaTime;

       


        updateAnimationState(); 

    }

    //private void shoot()
    //{
    //    Instantiate(playerBullet, playerBulletPos.position, Quaternion.identity);

    //}


    private void updateAnimationState()
    {
        movementState state;

        if (dirX > 0f)
        {
            state = movementState.running;
            sprite.flipX = false;
            
           
        }
        else if (dirX < 0f)
        {
            state = movementState.running;
            sprite.flipX = true;
            

        }else
        {
            state = movementState.idle;
        }
            
        anim.SetInteger("state", (int)state);



    }

   

}
