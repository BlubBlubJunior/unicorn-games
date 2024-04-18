using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private List<Transform> Targets;

    [SerializeField] private float speed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private float Timer;
    [SerializeField] private float resetTimer;

    [SerializeField] private float offset;
    [SerializeField] private float range;
    
    private Transform target;
    private Transform closestTarget;
    
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        enemyInRange();
    }

    void enemyInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Player" && collider.transform != transform)
            {
                if (!Targets.Contains(collider.transform))
                {
                    Targets.Add(collider.transform);
                    target = Targets[0];
                }
                GetClosestEnemy();
            }
            else
            {
                Targets.Clear();
            }
        }
    }
    void GetClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        
        foreach (Transform target in Targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
                moveTowardsTarget();
            }
        }
    }
    void moveTowardsTarget()
    {
        if (Vector3.Distance(transform.position, closestTarget.position) < offset)
        {
            attacking();
        }
        else
        {
            navMeshAgent.SetDestination(target.position);
        }
    }


    void attacking()
    {
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
