using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TargetPlayer;
    public float xOffset, yOffset, zOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = TargetPlayer.transform.position + new Vector3(xOffset, yOffset, zOffset);
        transform.LookAt(TargetPlayer.transform.position);

    }
}
