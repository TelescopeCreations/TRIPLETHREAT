using UnityEngine;
using TMPro; // Required for UI Text

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton pattern

    private int score = 0; // Player's score
    public TextMeshProUGUI scoreText; // Assign in Inspector

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("New Score: " + score);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned in the GameManager.");
        }
    }
}
