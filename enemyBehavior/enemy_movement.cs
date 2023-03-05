using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float agroRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float patrolSpeed;

    private Rigidbody2D rb;
    private float distToPlayer;
    private int currentWaypointIndex = 0;
    private bool isAgro;

 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        isAgro = false;
        distToPlayer = Vector2.Distance(transform.position, player.position);
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();

        if (distToPlayer < agroRange)
        {
            isAgro = true;
            move();
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, waypoints[currentWaypointIndex].transform.position, moveSpeed * Time.deltaTime); //moves the enemy back to th patrol route
            patrol();
        }
    }

    private void move()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);

    }

    private void patrol ()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if( currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * patrolSpeed);
    }
}
