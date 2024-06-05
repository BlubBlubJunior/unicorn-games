using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleCamera : MonoBehaviour
{
    public float Speed;

    private Vector3 moveDirection;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rb.velocity = moveDirection * Speed;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed /= 2;
        }
    }
}
