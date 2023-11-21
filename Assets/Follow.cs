using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform mainCharacter; // Reference to the main character's transform
    public float followSpeed = 5f; // Speed at which the follower moves towards the main character
    public float sideOffset = 2f; // Offset on the side when moving left or right
    public float verticalOffset = 1f; // Offset vertically when moving up or down

    private void LateUpdate()
    {
        if (mainCharacter != null)
        {
            // Calculate the forward and right vectors of the main character
            Vector3 forward = mainCharacter.forward;
            Vector3 right = mainCharacter.right;

            // Determine the movement direction based on input or character's velocity
            Vector3 movementDirection = mainCharacter.position - transform.position;
            movementDirection.y = 0f; // Ignore vertical movement

            // Calculate the target position with the offset
            Vector3 targetPosition = mainCharacter.position - forward * movementDirection.magnitude +
                                     right * (movementDirection.x < 0 ? -sideOffset : sideOffset) +
                                     mainCharacter.up * (movementDirection.y > 0 ? verticalOffset : -verticalOffset);

            // Move the follower towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Rotate the follower to face the same direction as the main character
            transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        }
        else
        {
            Debug.LogWarning("Main character not assigned to the Follower script.");
        }
    }
}
