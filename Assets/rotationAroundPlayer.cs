using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rotationAroundPlayer : MonoBehaviour
{
    public Vector3 Rotation;
    
    
    void Update()
    {
        transform.Rotate(Rotation * Time.deltaTime);
    }
}
