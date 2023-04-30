using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class player_life : MonoBehaviour
{

    private Rigidbody2D rb;
    public int currentHealth;
    public EntityHealthBar healthBar;
    [SerializeField] 
    float pushBack;
    public int maxHealth;
    private float dmg = SwordAttack.damage;
    private float playerSpeed = PlayerController.moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        
    }

    private void Update()
    {

    }
    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("collisionhit");
            currentHealth--;
            healthBar.SetHealth(currentHealth);
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
        if (currentHealth <= 0)
        {
            death();
        }


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
            currentHealth--;
            healthBar.SetHealth(currentHealth);
        }
    }
    private void death()
    {
        print("Death");
    }
    /*
    public void SetupHealthBar(Canvas canvas)
    {
        healthBar.transform.SetParent(canvas.transform);
    }
    */
}
