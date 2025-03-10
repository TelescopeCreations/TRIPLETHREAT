using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Gold, Silver, Bronze }
    public CollectibleType collectibleType;

    public AudioClip collectionSound; // Assign in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
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
        if (collectionSound != null)
        {
            audioSource.PlayOneShot(collectionSound);
        }

        // Handle collection logic based on type
        switch (collectibleType)
        {
            case CollectibleType.Gold:
                Debug.Log("Collected Gold Collectible!");
                // Add points, effects, etc.
                break;
            case CollectibleType.Silver:
                Debug.Log("Collected Silver Collectible!");
                break;
            case CollectibleType.Bronze:
                Debug.Log("Collected Bronze Collectible!");
                break;
        }

        Destroy(gameObject, 3.0f); // Small delay so sound can play
    }
}

