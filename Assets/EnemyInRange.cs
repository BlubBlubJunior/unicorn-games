using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    public List<GameObject> enemies;

    public float range;
    void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                enemies.Add(col.gameObject);   
            }
        }
    }

    private void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
