using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movepigie : MonoBehaviour
{
    public Vector3 verticalTarget;
    public float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, verticalTarget, moveSpeed * Time.deltaTime);
    }
}
