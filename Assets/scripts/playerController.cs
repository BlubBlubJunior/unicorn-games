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
        
        movedirection = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
    }
    void FixedUpdate()
    {
        rb.velocity = movedirection * movementspeed;
    }
}
