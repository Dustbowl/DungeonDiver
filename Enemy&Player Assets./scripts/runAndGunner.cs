using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runAndGunner : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDist;
    [SerializeField] private float retreatDist;
    [SerializeField] private GameObject bullet;
    //[SerializeField] private Transform bulletPos;

    private Rigidbody2D rb;
    private float distToPlayer;
    private float timer;
    private float timeBtwShots;
    public float startTimeShots;




    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
       

        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer > stopDist)
        {
            move();
            if (timer > 2)
            {
                timer = 0;
                shoot();

            }



        }
        else if (distToPlayer < stopDist && distToPlayer > retreatDist)
        {
            transform.position = this.transform.position;
            if (timer > 2)
            {
                timer = 0;
                shoot();

            }


        }
        else if (distToPlayer < retreatDist)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, -moveSpeed * Time.deltaTime);
        }


       




    }


    private void move ()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void shoot () { Instantiate(bullet, transform.position, Quaternion.identity); }



}
