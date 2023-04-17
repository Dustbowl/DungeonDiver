using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bulletScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float force;
    private Rigidbody2D rb;
    private float timer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        Vector3 dir = player.transform.position - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * force;

        float rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 4)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            print("hit");
            other.gameObject.GetComponent<player_life>().health -= .5f;
            Destroy(gameObject);
        }
    }
}
