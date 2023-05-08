using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorExitScript : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Gradient gradient;
    private Rigidbody2D rb;
    private int progress = 0;
    public int maxKillCount = 1;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMaxKillCount(int newCount)
    {
        maxKillCount = newCount;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && progress >= maxKillCount)
        {
            print("entered exit");
            time = 5;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && progress >= maxKillCount)
        {
            
            time -= Time.deltaTime;
            print("time = "+time);
            if (time <= 0f)
            {
                ExitFloor();
            }
        }
        
    }
    public void ProgressIncrease()
    {
        progress++;
        if(Random.Range(0, 3) == 0)
        {
            progress++;
        }
        sprite.color = gradient.Evaluate(((float)progress)/((float)maxKillCount));
    }
    private void ExitFloor()
    {
        GameObject scoreManager = GameObject.FindWithTag("Score");
        scoreManager.GetComponent<ScoreManager>().IncreaseFloor();
        GameObject.FindWithTag("DungeonGenerator").GetComponent<RoomInitialGeneration>().GenerateDungeon();
    }
}
