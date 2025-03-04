using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;
    public float threshold = -20f;
    public Transform respawnPoint;
    public float moveSpeed = 10f;

    public float boostspeed = 5f;

    private float xInput;
    private float zInput;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //For Handling Input and Animation
        ProcessInputs();
        if (transform.position.y < threshold)
        {
            Respawn();
        }


    }

    private void FixedUpdate()
    {
        // Move the player
        Move();
    }

    private void ProcessInputs()
    {
        //For Handling Input and Animation
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

    }
    private void Move()
    {
        // Move the player
        rb.AddForce(new Vector3(xInput, 0f, zInput) * moveSpeed);
    }

    private void Respawn()
    {
        // Reset the player's position to the respawn point
        transform.position = respawnPoint.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        transform.position = respawnPoint.position;
    }

    private void Boost()
    {
        moveSpeed *= boostspeed;
    }
}
