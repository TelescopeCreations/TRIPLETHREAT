using UnityEngine;
using System.Collections.Generic; // Required for List<T>
using UnityEngine.UI; // Required for UI Text
using TMPro; // Required for UI Text

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton pattern

    private int score = 0; // Player's score
    public TextMeshProUGUI scoreText; // Assign in Inspector

    public TextMeshProUGUI timerText; // Assign in Inspector
    private int totalCollectibles; // How many collectibles exist at the start
    private int collectedCount = 0; // How many we’ve picked up

    private MenuManager menuManager; // Reference to MenuManager script

    public GameObject endScreen; // Drag your Win UI panel here in Inspector

    // Timer variables
    public float startTimeInSeconds = 30f; // Adjustable in Inspector
    private float currentTime; // Current time left on the timer
    private bool timerRunning = false;
    //private bool GameObject.FindGameObjectsWithTag("Player").SetActive = true; // Check if the player is active

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


    void Start()
    {

        Invoke("StartTimer", 2.0f); // Start the timer after a 2-second delay

        // Count how many collectibles are in the scene
        totalCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;

        // Make sure win screen is hidden at start
        if (endScreen != null)
        {
            endScreen.SetActive(false);
        }
    }






    // ADDS SCORE TO THE PLAYER
    public void AddScore(int points)
    {
        score += points;
        Debug.Log("New Score: " + score);
        UpdateScoreUI();
    }

    // Method to update the score UI
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




    //TIMER FUNCTIONALITY
    // Start the timer when the game starts
    private void StartTimer()
    {
        currentTime = startTimeInSeconds;
        timerRunning = true;
        StartCoroutine(TimerCountdown());

    }


    private System.Collections.IEnumerator TimerCountdown()
    {
        while (timerRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI(currentTime);
            yield return null;
        }

        currentTime = 0;
        UpdateTimerUI(currentTime);
        ShowWinScreen(); // Show win screen when time runs out
        timerRunning = false;

        Debug.Log("Timer Finished!");

        // Optional: Trigger a fail state, game over, or another action when time runs out
        // ShowGameOverScreen();
    }



    // Method to update the timer UI
    private void UpdateTimerUI(float timeLeft)
    {
        timeLeft = currentTime; // Update current time
        if (timerText != null)
        {
            timerText.text = currentTime + "s";
        }
        else
        {
            Debug.LogWarning("Timer Text is not assigned in the GameManager.");
        }
    }



    //COLLECTION
    public void CollectItem()
    {
        collectedCount++;

        if (collectedCount >= totalCollectibles)
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        if (endScreen != null)
        {
            endScreen.SetActive(true);
            Debug.Log("You Win!");
        }
    }




}
