using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public float timeRemaining = 600;
    public bool active = false;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public GameObject scoreMenu;
    public int floorNumber = 0;
    public int enemiesKilled = 0;
    public void IncreaseFloor()
    {
        floorNumber++;
    }
    public void IncreaseKills()
    {
        enemiesKilled++;
    }
    private void Start()
    {
        Time.timeScale = 1f;
        // Starts the timer automatically
        active = true;

    }
    void Update()
    {
        if (floorNumber >= 5)
        {
            EndGame();
        }
        if (active)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                EndGame();
                timeRemaining = 0;
                
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text= string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void EndGame()
    {
        active = false;
        scoreMenu.SetActive(true);
        
        int score = enemiesKilled * 100 + floorNumber * 1000;
        if (floorNumber >= 10)
        {
            score = score * 3;
            scoreText.text = string.Format("GAME COMPlETE\nTime Remaining: {0}\nFloor Reached: {1}\nEnemies Killed: {2}\nOverall Score: {3}", timeText.text, floorNumber, enemiesKilled, score);
        } else
        {
            scoreText.text = string.Format("GAME OVER\nTime Remaining: {0}\nFloor Reached: {1}\nEnemies Killed: {2}\nOverall Score: {3}", timeText.text, floorNumber, enemiesKilled, score);
        }

        
        Time.timeScale = 0f;
    }
    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
