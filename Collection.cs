using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class CoinCollection : MonoBehaviour
{
    private int coinCount = 0; // Tracks the number of coins collected
    private bool canCollect = false; // Checks if the player is near a coin
    private GameObject coinToCollect; // Reference to the coin that can be collected

    public TextMeshProUGUI coinText; // Reference to the UI text for displaying coin count
    public int winningThreshold = 10; // Number of objects to collect to win

    private void Start()
    {
        // Initialize the coin count text in the UI
        coinText.text = "POT: " + coinCount.ToString();
    }

    private void Update()
    {
        // Check if the player presses 'E' and is near a coin
        if (canCollect && Input.GetKeyDown(KeyCode.E))
        {
            CollectCoin();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "POT")
        {
            canCollect = true; // Player is near a coin
            coinToCollect = other.gameObject; // Reference to the coin
        }
        else if (other.transform.tag == "DOOR")
        {
            ToggleDoorRotation(other.gameObject); // Rotate the door
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "POT")
        {
            canCollect = false; // Player is no longer near the coin
            coinToCollect = null;
        }
    }

    private void CollectCoin()
    {
        if (coinToCollect != null)
        {
            coinCount++; // Increment the coin count
            if (coinCount >= winningThreshold)
            {
                coinText.text = "Game Over: You Win!"; // Display winning message
            }
            else
            {
                coinText.text = "POT: " + coinCount.ToString(); // Update the UI text
            }
            
            Debug.Log("POT Collected: " + coinCount);
            Destroy(coinToCollect); // Destroy the coin object
            coinToCollect = null; // Reset the coin reference
        }
    }

    private void ToggleDoorRotation(GameObject door)
    {
        // Get the current Y rotation of the door
        float currentYRotation = door.transform.localEulerAngles.y;

        // Determine new rotation: Toggle between -90 and 0 degrees
        if (Mathf.Approximately(currentYRotation, 270f)) // If currently at -90 degrees
        {
            door.transform.localEulerAngles = new Vector3(door.transform.localEulerAngles.x, 0f, door.transform.localEulerAngles.z); // Rotate to 0
            Debug.Log("Door rotated to +90 degrees");
        }
        else // If not at -90 degrees, rotate to -90
        {
            door.transform.localEulerAngles = new Vector3(door.transform.localEulerAngles.x, 90f, door.transform.localEulerAngles.z); // Rotate to -90
            Debug.Log("Door rotated to -90 degrees");
        }
    }
}
