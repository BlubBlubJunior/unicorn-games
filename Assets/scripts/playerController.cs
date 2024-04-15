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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        anime.SetFloat("sidemovement", movedirection.x);
        movedirection = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));
       
    }

    void FixedUpdate()
    {
        rb.velocity = movedirection * movementspeed;
    }
}
