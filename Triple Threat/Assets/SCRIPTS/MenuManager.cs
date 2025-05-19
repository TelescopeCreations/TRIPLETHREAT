using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class MenuManager : MonoBehaviour
{

    public GameObject[] pauseUI; // Array to hold pause UI elements

    public GameManager gameManager; // Reference to the GameManager script

    // Start is called before the first frame update
    void Start()
    {

    }


    public void PauseGame()
    {
        Time.timeScale = 0; // Pause the game
        gameManager.timerRunning = false; // Stop the timer
        gameManager.endScreen.SetActive(false); // Hide the end screen if it's active

        // Show pause UI elements
        foreach (GameObject ui in pauseUI)
        {
            ui.SetActive(true);
        }
    }


    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game
        gameManager.timerRunning = true; // Restart the timer

        // Hide pause UI elements
        foreach (GameObject ui in pauseUI)
        {
            ui.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }


}
