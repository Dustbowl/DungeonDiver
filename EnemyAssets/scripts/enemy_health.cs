using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health : MonoBehaviour
{
    public  float health;
    private Rigidbody2D rb;
    void Start()
    {
       rb= GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            death();
        }
    }

    private void death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject);

    }
}
