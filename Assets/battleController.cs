using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleController : MonoBehaviour
{
    public float moveSpeed = 1f; 
    public float gridSize = 1f; 

    private Vector3 targetPosition; 

    void Start()
    {
        targetPosition = transform.position; 
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
        {
         
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 gridPosition = RoundToNearestGrid(hit.point);
                Move(gridPosition);
            }
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void Move(Vector3 destination)
    {
        targetPosition = destination;
    }

    Vector3 RoundToNearestGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(x,transform.position.y, z);
    }
}