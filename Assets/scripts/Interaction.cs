using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float reach;

    [SerializeField] private LayerMask interactionlayer;
    

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        
        if (Input.GetKeyDown(KeyCode.F))
        { 
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, reach, interactionlayer)) 
            {
                
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector3.forward * reach);
        
        Gizmos.DrawWireSphere(transform.position + transform.forward * reach, 0.2f);
    }
}
