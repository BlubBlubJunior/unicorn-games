using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    public GameObject player;

    public float speed, distancestop;
    void Start()
    {
        
    }
    
    void Update()
    {
        float distancePlayer = Vector3.Distance(transform.position, player.transform.position);
        
        if (distancePlayer >= distancestop)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
