using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Gold, Silver, Bronze }
    public CollectibleType collectibleType;

    [Header("Audio Settings")]
    public AudioClip collectionSound; // Assign in the Inspector
    public AudioSource audioSource;
    private Collider collectibleCollider;
    
    private int points; // Points for each collectible
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on " + gameObject.name + ". Please add one in the Inspector.");
        }
        
         // Get the Collider component
        collectibleCollider = GetComponent<Collider>();
        if (collectibleCollider == null)
        {
            Debug.LogWarning("No Collider found on " + gameObject.name + ". Please add one in the Inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            Collect();
        }
    }

    private void Collect()
    {   
        
        // Disable collider to prevent multiple triggers
        if (collectibleCollider != null)
        {
            collectibleCollider.enabled = false;
        }   


         // Play collection sound
         if (audioSource != null && collectionSound != null)
        {
             audioSource.PlayOneShot(collectionSound);
        }

      
        // Handle collection logic based on type
        switch (collectibleType)
        {
            case CollectibleType.Gold:
                Debug.Log("Collected Gold Collectible!");
                // Add points, effects, etc.
                points = 30;
                break;
            case CollectibleType.Silver:
                Debug.Log("Collected Silver Collectible!");
                points = 20;
                break;
            case CollectibleType.Bronze:
                Debug.Log("Collected Bronze Collectible!");
                points = 10;
                break;
        }
        GameManager.instance.AddScore(points);

          // Add points to player's score
    
        Debug.Log($"Collected {collectibleType}! +{points} points");

        

        Destroy(gameObject, 4.0f); // Small delay so sound can play
    }
}

