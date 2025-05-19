using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Reset : MonoBehaviour
{
    public Rigidbody rb;
    public float threshold = -20f;
    public Transform respawnPoint;
    //RESPAWN POINT


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < threshold)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Reset the player's position to the respawn point
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        transform.position = respawnPoint.position;
    }
}
