using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
    void FixedUpdate() {
        //XRay ();
    }

    /*private void XRay() {

        RaycastHit oldHit;
        float characterDistance = Vector3.Distance(transform.position, GameObject.Find("Character").transform.position);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, characterDistance)) {
            if(oldHit.transform) {

                
                Color colorA = oldHit.transform.gameObject.GetComponent<Renderer>().material.color;
                colorA.a = 1f;
                oldHit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorA);
            }
            Color colorB = hit.transform.gameObject.GetComponent<Renderer>().material.color;
            colorB.a = 0.5f;
            hit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorB);
            
            oldHit = hit;
        }
    }*/
}

