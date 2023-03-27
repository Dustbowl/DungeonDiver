using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_bullet : MonoBehaviour
{
    [SerializeField] private float force;

    private GameObject playerBullet;
    private Rigidbody2D rb;
    private float timer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerBullet = GameObject.FindWithTag("playerBullet");
    }

    // Update is called once per frame
    void Update()
    {


        rb.velocity = transform.right * force;


        timer += Time.deltaTime;

        if (timer > 2)
        {
            Destroy(playerBullet);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("hit");
            other.gameObject.GetComponent<enemy_health>().health -= 1;
            Destroy(playerBullet);
        }
    }
}
