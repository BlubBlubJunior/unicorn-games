using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialoge : MonoBehaviour
{
    [SerializeField] private float reach;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector3.forward * reach);
        
        Gizmos.DrawWireSphere(transform.position + transform.forward * reach, 0.2f);
    }
}
