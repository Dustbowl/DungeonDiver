using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 3;
    [SerializeField]
    public int currentHealth = 3;
    private Rigidbody2D rb;
    void Start()
    {
       rb= GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            death();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("collisionhit");
            currentHealth--;
        }
        if (currentHealth <= 0)
        {
            death();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("hit");
            currentHealth--;
        }
        if (currentHealth <= 0)
        {
            death();
        }
    }
    private void death()
    {
        GameObject exit = GameObject.FindWithTag("Exit");
        exit.GetComponent<FloorExitScript>().ProgressIncrease();
        Destroy(gameObject);
    }
}
