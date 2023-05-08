using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class player_life : MonoBehaviour
{

    private Rigidbody2D rb;
    public SpriteRenderer sprite;
    public int currentHealth;
    public EntityHealthBar healthBar;
    [SerializeField]
    float pushBack;
    public int maxHealth;
    private float dmg = SwordAttack.damage;
    private float playerSpeed = PlayerController.moveSpeed;
    public bool invuln = false;
    private float invulnDuration = 0f;
    private float invulnCD = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

    }

    private void Update()
    {
        if(invulnCD > 0)
        {
            invulnCD -= Time.deltaTime;
        } else
        {
            invulnCD = 0f;
        }
        if(invuln)
        {
            TickInvuln();
        }
        if (currentHealth <= 0)
        {
            death();
        }
    }
    public void Invuln()
    {
        if(invulnCD == 0)
        {
            invulnCD = 15f;
            invuln = true;
            invulnDuration = 3f;
            sprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
    private void TickInvuln()
    {
        invulnDuration -= Time.deltaTime;
        if (invulnDuration <= 0)
        {
            invuln = false;
            sprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }
    // Update is called once per frame
    public void takeDamage(int damage = 1)
    {
        if(!invuln) 
        {
            currentHealth = currentHealth - damage;
            healthBar.SetHealth(currentHealth);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("collisionhit");
            
        }
        if (currentHealth <= 0)
        {
            death();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Harmful")) {
        //    print("hit");
        //    currentHealth--;
        //    healthBar.SetHealth(currentHealth);
        //}
        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    print("hit");
        //    currentHealth--;
        //    healthBar.SetHealth(currentHealth);
        //}



        if (other.tag == "healthPotion")
        {
            print("health:" + currentHealth);
            currentHealth++;
            print(currentHealth);
            Destroy(other.gameObject);
            healthBar.SetHealth(currentHealth);
        }
        else if (other.tag == "damagePotion")
        {
            print("damage");

            print("dmg:" + dmg);

            dmg += .5f;
            SwordAttack.damage = dmg;

            print(SwordAttack.damage);
            Destroy(other.gameObject);
        }
        else if (other.tag == "speedPotion")
        {
            print(playerSpeed);

            playerSpeed += .5f;
            PlayerController.moveSpeed = playerSpeed;

            Destroy(other.gameObject);
        }
        else if (other.tag == "enemy_melee")
        {
            takeDamage();
        }
        else if (other.tag == "boss_melee")
        {
            takeDamage(3);
        }
    }
    private void death()
    {
        GameObject.FindWithTag("Score").GetComponent<ScoreManager>().EndGame();
    }
    /*
    public void SetupHealthBar(Canvas canvas)
    {
        healthBar.transform.SetParent(canvas.transform);
    }
    */
}
