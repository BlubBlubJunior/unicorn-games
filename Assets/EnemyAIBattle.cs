using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAIBattle : MonoBehaviour
{
    public float gridSize = 1f;
    
    public bool canMove = true;
    private Vector3 targetPosition;

    public float range;

    private List<GameObject> player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, range);

        foreach (Collider col in collider)
        {
            if (col.CompareTag("Player"))
            {
                Vector3 gridPosition = RoundToNearestGrid(col.transform.position);
                
                Vector3 direction = (gridPosition - transform.position).normalized;

                float distance = Vector3.Distance(transform.position, col.transform.position) / gridSize;

                targetPosition = gridPosition - direction * gridSize;
                
                if (canMove = true)
                {
                    StartCoroutine(moveToDestination(targetPosition));    
                }

                if (distance <= 1f)
                {
                    print(" attack");
                }
            }
        }
        
    }


    
    IEnumerator moveToDestination(Vector3 destination)
    {
        canMove = false;
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

            transform.position = targetPosition;
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
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
