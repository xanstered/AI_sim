using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Image progressBar;

    // Threshold value on the progress bar to trigger movement
    public float thresholdValue = 0.75f;

    // Reference to the target location
    public Transform targetLocation;

    // Speed of the player movement
    public float moveSpeed = 5f;

    // Reference to the player Rigidbody
    private Rigidbody playerRigidbody;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the progress bar fill amount has reached the threshold
        if (progressBar.fillAmount >= thresholdValue)
        {
            // Move the player towards the target location
            MovePlayerToTarget();
        }
    }

    void MovePlayerToTarget()
    {
        // Calculate the direction to the target location
        Vector3 direction = (targetLocation.position - transform.position).normalized;

        // Calculate the new position
        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        // Move the player to the new position
        playerRigidbody.MovePosition(newPosition);
    }
}
