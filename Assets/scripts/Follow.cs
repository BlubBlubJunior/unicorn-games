using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform mainCharacter; 
    public float followSpeed = 5f; 
    public float sideOffset = 2f; 
    public float verticalOffset = 1f; 

    private void LateUpdate()
    {
        if (mainCharacter != null)
        {
            Vector3 forward = mainCharacter.forward;
            Vector3 right = mainCharacter.right;
            
            Vector3 movementDirection = mainCharacter.position - transform.position;
            movementDirection.y = 0f; 

            
            Vector3 targetPosition = mainCharacter.position - forward * movementDirection.magnitude +
                                     right * (movementDirection.x < 0 ? -sideOffset : sideOffset) +
                                     mainCharacter.up * (movementDirection.y > 0 ? verticalOffset : -verticalOffset);

            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

            
            transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        }
        else
        {
            Debug.LogWarning("Main character not assigned to the Follower script.");
        }
    }
}
