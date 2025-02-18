using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum InterfaceVariable { TIME, SCORE, LIVES }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;  
    public int lives = 3;
    public float initialTime = 120f;
    public float timeLeft;  

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // If already a singleton, destroy
        }

        timeLeft = initialTime;

        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "MainMenu")
        {
            timeLeft = 0;
        }

        else if (sceneName == "Game")
        {
            timeLeft = initialTime;
        }
    }

    private void Update()
    {
        // Timer down
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            GameOver();
        }
    }

    public float GetTime()
    {
        return timeLeft;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Puntuación: " + score);
    }

    public int GetLives()
    {
        return lives;
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Vidas restantes: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        timeLeft = initialTime; 
    }

    public void GameOver()
    {
        Debug.Log("¡Game Over!");
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
