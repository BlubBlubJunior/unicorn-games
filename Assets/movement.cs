using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask Groundlayer;

    [SerializeField] private float gridsize;
    [SerializeField] private int maxgrid;

    private Vector3 targetposition;
    private int gridsmoved;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gridsmoved < maxgrid)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Groundlayer))
            {
                MoveToPosition(hit.point);
            }
        }
        MoveToPosition();
    }

    void MoveToPosition(Vector3 target)
    {
        target.y = transform.position.y;
        target.x = Mathf.Round(target.x / gridsize) * gridsize;
        target.z = Mathf.Round(target.z / gridsize) * gridsize;

        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 direction = (target - transform.position).normalized;

            direction.y = 0;
        
            transform.LookAt(target);

            transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            gridsmoved++;
        }
    }
}