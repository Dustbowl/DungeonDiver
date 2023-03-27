using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float agroRange;
    //[SerializeField] private GameObject player;


    private Rigidbody2D rb;
    private float distToPlayer;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        distToPlayer = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        
        timer += Time.deltaTime;

        if(distToPlayer <= agroRange)
        {
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
        
           
        
        
    }

    private void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);

    }
}
