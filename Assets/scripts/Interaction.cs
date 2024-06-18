using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float reach;
    

    // Update is called once per frame
    void Update()
    {
        Ray ray;

        RaycastHit hit;
        
        Physics.Raycast(hit)
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector3.forward * reach);
        
        Gizmos.DrawWireSphere(transform.position + transform.forward * reach, 0.2f);
    }
}
