using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Transform player; // Reference to the player Transform
    public Transform initialPosition; // Initial position to reset the player to
    public int currentLevel = 1; // Current level count

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player touched the pipe
        if (collision.CompareTag("Player"))
        {
            // Increment the level count
            currentLevel++;

            // Reset the player's position to the initial position
            if (player != null && initialPosition != null)
            {
                player.position = initialPosition.position;
            }

            // Optionally log or trigger other level-related actions
            Debug.Log("Player moved to the next level! Current Level: " + currentLevel);
        }
    }
}
