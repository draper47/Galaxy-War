using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variable to store player speed
    public float maxSpeed;
    //Variable to store player current health
    public int playerHealth;
    //Variable to store player age
    public int playerAge;
    //Variable to store player name
    public string playerName;
    //Variable to store player score
    public int playerScore;
    //Variable for the amount of keys collected
    private bool hasAllKeys = false;
    //Variable for how much ammo is left in clip
    public int ammoLeft;
    //Variable for current speed of the player
    private double speed;

    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = new position (0, 0, 0) 
        transform.position = new Vector3(0, 0, 0);

        //Print player name
        Debug.Log("Players name is " + playerName);

        //Print players health
        Debug.Log("Current health is " + playerHealth);

        //Print players age
        Debug.Log("Players age is " + playerAge);

        //Print players maximum speed
        Debug.Log("The max speed is set to " + maxSpeed);

        //Print players score
        Debug.Log("Your score is " + playerScore);

        //Print how many keys the player has collected
        Debug.Log("All keys collected is " + hasAllKeys);

        //Print how much ammo is left.
        Debug.Log("You have " + ammoLeft + " left in your clip.");

        //Print the current speed of the payer
        Debug.Log("Your speed is " + speed);
         

    }

    // Update is called once per frame
    void Update()
    {
        // Move player right at 5 meters per second
        transform.Translate(Vector3.right * maxSpeed * Time.deltaTime);
    }
}
