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


    [SerializeField] float pushBack;
    public double health;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("hit");
            hitCount++;

            if (hitCount == health)
            {
                death();
                levelRestart();
            }
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
}
