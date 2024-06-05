using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscr : MonoBehaviour
{
    public LayerMask groundLayer; // Set this to your ground layer in the Inspector
    public LayerMask obstacleLayer; // Set this to your obstacle layer in the Inspector
    public float moveSpeed = 5f; // Adjust movement speed as needed
    public Vector3 targetPosition;
    private bool hasTarget = false;
    private bool verticalPhase = true;

    void Start()
    {
        StartCoroutine(CalculateNewTargetPosition());
    }

    void Update()
    {
        MoveToTarget();
    }

    IEnumerator CalculateNewTargetPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Vector3 newPosition = GetRandomTargetPosition();
            RaycastHit hit;
            if (Physics.Raycast(newPosition + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                targetPosition = hit.point;
                hasTarget = true;
                verticalPhase = true; // Start with vertical movement
            }
        }
    }

    Vector3 GetRandomTargetPosition()
    {
        // Generate a random target position within a defined area. Adjust as needed.
        int x = Random.Range(-10, 10);
        int z = Random.Range(-10, 10);
        return new Vector3(x, 0, z);
    }

    void MoveToTarget()
    {
        if (hasTarget)
        {
            if (verticalPhase)
            {
                // Move vertically towards the target position
                Vector3 verticalTarget = new Vector3(transform.position.x, transform.position.y, targetPosition.z);
                if (!IsPathBlocked(verticalTarget))
                {
                    transform.position = Vector3.MoveTowards(transform.position, verticalTarget, moveSpeed * Time.deltaTime);
                    if (Mathf.Approximately(transform.position.z, verticalTarget.z))
                    {
                        verticalPhase = false;
                    }
                }
                else
                {
                    // Handle obstacle
                    AvoidObstacle();
                }
            }
            else
            {
                // Move horizontally towards the target position
                Vector3 horizontalTarget = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
                if (!IsPathBlocked(horizontalTarget))
                {
                    transform.position = Vector3.MoveTowards(transform.position, horizontalTarget, moveSpeed * Time.deltaTime);
                    if (Mathf.Approximately(transform.position.x, horizontalTarget.x))
                    {
                        hasTarget = false; // Reached the target position
                    }
                }
                else
                {
                    // Handle obstacle
                    AvoidObstacle();
                }
            }
        }
    }

    bool IsPathBlocked(Vector3 target)
    {
        RaycastHit hit;
        Vector3 direction = (target - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target);
        if (Physics.Raycast(transform.position, direction, out hit, distance, obstacleLayer))
        {
            return true; // Path is blocked by an obstacle
        }
        return false;
    }

    void AvoidObstacle()
    {
        // Move the object away from the obstacle
        Vector3 avoidanceDirection = transform.position - targetPosition;
        avoidanceDirection.y = 0; // Keep movement horizontal
        transform.position += avoidanceDirection.normalized * moveSpeed * Time.deltaTime;

        // Ensure the object stays on the ground layer
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            transform.position = hit.point;
        }
        else
        {
            // If it leaves the ground layer, stop the movement
            hasTarget = false;
        }
    }
}


