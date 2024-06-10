using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public Vector3 verticalTarget;
    public Vector3 horizontalTarget;

    public int remainingMoves;
    public int resetMovement;

    public bool EnemyTurn;

    public Vector3 adjustedTarget;
    private Vector3 enemypos;

    private bool walking;
    void Update()
    {
        if (remainingMoves > 0 && EnemyTurn == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    targetPosition = col.transform.position;

                    if (Physics.Raycast(gameObject.transform.position + Vector3.up * 0.5f, Vector3.down, 1f, groundLayer))
                    {
                        if (canMove)
                        {
                            //horizontalTarget = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
                            //verticalTarget = new Vector3(transform.position.x, transform.position.y, targetPosition.z);
                            hasTarget = true;
                        }

                        if (verticalTarget.z != targetPosition.z)
                        {
                            verticalPhase = true;
                            walking = true;
                        }
                    }
                }
            }
            MoveToTarget();
        }

        if (Vector3.Distance(transform.position, targetPosition) <= gridSize)
        {
            attacking();
        }
    }

    void attacking()
    {
        int attack = 1;

        if (attack <= 0)
        {
            print("attack");
        }
        else
        {
            print("endTurn");
        }
        
    }
    void MoveToTarget()
    {
        if (hasTarget && remainingMoves > 0)
        {
            if (verticalPhase)
            { 
                verticalTarget = new Vector3(transform.position.x, transform.position.y, targetPosition.z);
                enemypos = new Vector3(targetPosition.x,targetPosition.y,targetPosition.z);
                    
                moveTowardsTarget(verticalTarget); 
                
                if (Mathf.Approximately(transform.position.z, verticalTarget.z)) 
                { 
                    verticalPhase = false;
                }
            }
            else
            {
                horizontalTarget = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
                moveTowardsTarget(horizontalTarget);
            }
        }
    }
    
    void moveTowardsTarget(Vector3 target)
    {

        if (Mathf.Approximately(transform.position.z, targetPosition.z))
        {
            Vector3 direction = (target - transform.position).normalized;
            adjustedTarget = target - direction * gridSize;
        
            float step = moveSpeed * Time.deltaTime;
            Vector3 newPostion = Vector3.MoveTowards(transform.position, adjustedTarget, step);

            float  distanceMoved = Vector3.Distance(transform.position, newPostion);
        
            if (walking == true)
            {
                remainingMoves -= Mathf.CeilToInt(distanceMoved / gridSize);
                walking = false;
            }
            
            transform.position = newPostion;    
        }
        else
        {
            float step = moveSpeed * Time.deltaTime;
            Vector3 newPostion = Vector3.MoveTowards(transform.position, target, step);
            
            float distanceMoved = Vector3.Distance(transform.position, newPostion);
            
            if (walking == true)
            {
                remainingMoves -= Mathf.CeilToInt(distanceMoved / gridSize);
                walking = false;
            }
            
            transform.position = newPostion;
        }
    }
    bool IsPathBlocked(Vector3 target)
    {
        RaycastHit hit;
        Vector3 direction = (target - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target);
        if (Physics.Raycast(transform.position, direction, out hit, distance, obstacleLayer))
        {
            return true;
        }
        return false;
    }

    void AvoidObstacle()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, obstacleLayer);
        if (colliders.Length > 0)
        {
            Vector3 avoidanceDirection = Vector3.zero;
            float maxDistance = float.MinValue;

            foreach (Collider col in colliders)
            {
                Vector3 toCollider = col.ClosestPoint(transform.position) - transform.position;
                if (toCollider.magnitude > maxDistance)
                {
                    avoidanceDirection = toCollider;
                    maxDistance = toCollider.magnitude;
                }
            }

            avoidanceDirection = Vector3.Cross(avoidanceDirection.normalized, Vector3.up);
            avoidanceDirection = transform.position + avoidanceDirection * gridSize;
            if (IsPathBlocked(avoidanceDirection))
            {
                avoidanceDirection = transform.position - avoidanceDirection.normalized * gridSize;
                remainingMoves -= Mathf.FloorToInt(Vector3.Distance(transform.position, avoidanceDirection) / gridSize);
            }
        }
        
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}