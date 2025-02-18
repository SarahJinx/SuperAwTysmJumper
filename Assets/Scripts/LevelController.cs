using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Transform player; // Reference to the player Transform
    public Transform initialPosition; // Initial position to reset the player to
    public int currentLevel = 1; // Current level count

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player touched the pipe
        if (player)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
