using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleController : MonoBehaviour
{
    public float moveSpeed = 1f; 
    public float gridSize = 1f;
    public int maxMoveRange = 5;
    public int remainingMoveRange;
    
    private Vector3 targetPosition;
    
    public bool moving;
    void Start()
    {
        targetPosition = transform.position;
        remainingMoveRange = maxMoveRange;
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && remainingMoveRange > 0) 
        {
         
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 gridPosition = RoundToNearestGrid(hit.point);
                float distance = Vector3.Distance(transform.position, gridPosition) / gridSize;

                if (distance <= remainingMoveRange)
                {
                    Move(gridPosition);
                    remainingMoveRange -= (int)distance;
                    moving = true;
                }
            }
        }

        
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            moving = false;
        
        
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
    }

    Vector3 RoundToNearestGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(x,transform.position.y, z);
    }
}