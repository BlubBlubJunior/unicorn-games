using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBattle : MonoBehaviour
{
    public float gridSize = 1f;
    public float moveSpeed = 1f;  // Speed of the enemy
    public bool canMove = true;
    private Vector3 targetPosition;

    public float range;
    public LayerMask groundLayer; // LayerMask to specify the ground layer

    private List<GameObject> player;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization if needed
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                Vector3 gridPosition = RoundToNearestGrid(col.transform.position);
                Vector3 direction = (gridPosition - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, col.transform.position) / gridSize;

                // Calculate target position to be 1 grid block away from the player
                targetPosition = gridPosition - direction * gridSize;

                // Check if targetPosition is on the ground layer
                if (Physics.Raycast(targetPosition + Vector3.up * 0.5f, Vector3.down, 1f, groundLayer))
                {
                    if (canMove)
                    {
                        StartCoroutine(MoveToDestination(targetPosition));
                    }
                }

                if (distance <= 1f)
                {
                    print("attack");
                }
            }
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
        transform.position = destination;  // Ensure it exactly reaches the destination
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
