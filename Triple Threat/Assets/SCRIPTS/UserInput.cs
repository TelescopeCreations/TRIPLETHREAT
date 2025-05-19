using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{

    public string firstName;
    public string lastName;
    private TextMeshProUGUI textMeshPro;
    private int playerHealth = 100;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Game Started");


    }

    // Update is called once per frame
    void Update()
    {

        textMeshPro = GetComponent<TextMeshProUGUI>();

        Debug.Log("UserInput Check");

        for (int i = 0; i < 10; i++)
        {
            textMeshPro.text = "Health: " + (playerHealth - i);

        }
    }
}
