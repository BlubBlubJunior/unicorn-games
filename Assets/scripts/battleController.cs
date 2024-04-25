using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class battleController : MonoBehaviour
{
    public float moveSpeed = 1f; 
    public float gridSize = 1f;
    public int maxMoveRange = 5;
    public int remainingMoveRange;
    public LayerMask Groundlayer;
    
    private Vector3 targetPosition;
    
    public bool canMove = true;

    public LayerMask enemieLayer;

    public GameObject particales;

    private PlayerStats playerStats;
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        targetPosition = transform.position;
        remainingMoveRange = maxMoveRange;
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && remainingMoveRange > 0 && canMove) 
        {
         
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Groundlayer))
            {
                Vector3 gridPosition = RoundToNearestGrid(hit.point);
                float distance = Vector3.Distance(transform.position, gridPosition) / gridSize;

                if (distance <= remainingMoveRange && !isTileOccupied(gridPosition))
                {
                    canMove = false;
                    Move(gridPosition);
                    remainingMoveRange -= (int)distance;
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemieLayer))
            {
                Vector3 enemyposition = hit.collider.transform.position;
                float distance = Vector3.Distance(transform.position, enemyposition) / gridSize;

                if (distance <= 1)
                {
                    EnemyStats enemyHealth = hit.collider.GetComponent<EnemyStats>();
                    enemyHealth.TakeDamage(playerStats.damage);
                    GameObject particle = Instantiate(particales, hit.transform.position, quaternion.identity);
                    StartCoroutine(DestroyParticlesAfterDelay(particle, 5f));
                }
            }
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    IEnumerator DestroyParticlesAfterDelay(GameObject particle, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(particle);
    }
    void Move(Vector3 destination)
    {
        StartCoroutine(moveToDestination(destination));
    }

    IEnumerator moveToDestination(Vector3 destination)
    {
        while (transform.position != destination)
        {
            Vector3 diff = destination - transform.position;

            if (Mathf.Abs(diff.x) > Mathf.Abs(diff.z))
            {
                targetPosition = new Vector3(Mathf.MoveTowards(transform.position.x, destination.x, gridSize), transform.position.y, transform.position.z);
            }
            else
            {
                targetPosition = new Vector3(transform.position.x, transform.position.y, Mathf.MoveTowards(transform.position.z, destination.z, gridSize));
            }

            yield return null;
        }

        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            canMove = true;
        }
    }

    Vector3 RoundToNearestGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(x,transform.position.y, z);
    }

    bool isTileOccupied(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f, Groundlayer);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
}