using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBattle : MonoBehaviour
{
    public float gridSize = 1f;
    public float moveSpeed = 1f;  // Speed of the enemy
    public bool canMove = true;
    public Vector3 targetPosition;

    
    public bool hasTarget = false;
    public bool verticalPhase = true;
    
    public float range;
    public LayerMask groundLayer;

    private List<GameObject> player;
    
    public LayerMask obstacleLayer;
    void Update()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                
                targetPosition = col.transform.position;
                //Vector3 gridPosition = RoundToNearestGrid(col.transform.position);
                //Vector3 direction = (gridPosition - transform.position).normalized;
                //float distance = Vector3.Distance(transform.position, col.transform.position) / gridSize;
                
                //targetPosition = gridPosition;
                
                if (Physics.Raycast(gameObject.transform.position + Vector3.up * 0.5f, Vector3.down ,1f, groundLayer))
                {
                    if (canMove)
                    {
                        hasTarget = true;
                        verticalPhase = true;
                        //StartCoroutine(MoveToDestination(targetPosition));
                        MoveToTarget();
                    }
                }
            }
        }
        
    }
    
    void MoveToTarget()
    {
        if (hasTarget)
        {
            if (verticalPhase)
            {
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
                    AvoidObstacle();
                }
            }
            else
            {
                Vector3 horizontalTarget = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
                if (!IsPathBlocked(horizontalTarget))
                {
                    transform.position = Vector3.MoveTowards(transform.position, horizontalTarget, moveSpeed * Time.deltaTime);
                    if (Mathf.Approximately(transform.position.x, horizontalTarget.x))
                    {
                        hasTarget = false;
                    }
                }
                else
                {
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

    IEnumerator MoveToDestination(Vector3 destination)
    {
        canMove = false;
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            Vector3 direction = (destination - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = destination;  
        canMove = true;
    }

    Vector3 RoundToNearestGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(x, transform.position.y, z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
