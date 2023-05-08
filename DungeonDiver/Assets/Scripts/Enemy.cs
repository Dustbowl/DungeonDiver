using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    public float Health {
        set {
            health = value;

            if(health <= 0) {
                death();
            }
        }
        get {
            return health;
        }
    }

    public float health = 1;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    private void death()
    {
        GameObject scoreManager = GameObject.FindWithTag("Score");
        scoreManager.GetComponent<ScoreManager>().IncreaseKills();
        GameObject exit = GameObject.FindWithTag("Exit");
        exit.GetComponent<FloorExitScript>().ProgressIncrease();
        Destroy(gameObject);
    }
}
