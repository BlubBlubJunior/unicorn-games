using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float movementspeed;
    private Rigidbody rb;
    private Vector3 movedirection;

    public Animator anime;
    
    
    [SerializeField] private LayerMask groundLayer;
    
    public Vector3 targetPosition;

    private bool move;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        anime.SetFloat("sidemovement", movedirection.x);
        
        movedirection = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            
            if (Physics.Raycast(ray,out hit, Mathf.Infinity, groundLayer))
            {
                targetPosition = hit.point;
                move = true;
            }
        }

        if (move == true)
        {
            //MoveToPosition(targetPosition);      
        }
    }
    void MoveToPosition(Vector3 target)
    {
        Vector3 transformpos = new Vector3(target.x, transform.position.y, target.z);
        Vector3 currentpos = new Vector3(transform.position.x, transform.position.z, transform.position.y);
        Vector3 direction = (transformpos - currentpos).normalized;
        
        if (Vector3.Distance(currentpos, transformpos) > 0.1f)
        {
            transform.position += direction * movementspeed * Time.deltaTime;
        }
        else
        {
            print("check");
        }
    }
    void FixedUpdate()
    {
        rb.velocity = movedirection * movementspeed;
    }
}
