using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_life : MonoBehaviour
{

    private Rigidbody2D rb;
    public int hitCount;
    public Collider2D col;
    private float dmg = SwordAttack.damage;
    private float playerSpeed = PlayerController.moveSpeed;
    public  float health;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col.GetComponent<BoxCollider2D>();
        
    }

    private void Update()
    {
        if(health == 0)
        {
            death();
            levelRestart();
        }
    }
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "healthPotion")
        {
            print("health:" + health);
            healthUp();
            print(health);
            Destroy(other.gameObject);
        }
        else if (other.tag == "damagePotion")
        {
            print("damage");

            print("dmg:" + dmg);

            dmg += .5f;
            SwordAttack.damage = dmg;

            print(SwordAttack.damage);
            Destroy(other.gameObject);
        } else if (other.tag == "speedPotion")
        {
            print(playerSpeed);

            playerSpeed += .5f;
            PlayerController.moveSpeed= playerSpeed;

            Destroy(other.gameObject);
        }

    }

    
    private void death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject);

    }


    private void levelRestart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void healthUp()
    {
        health++;
    }
}
