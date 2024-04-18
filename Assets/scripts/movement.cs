using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float tileSize = 1.0f;

    private Vector3 targetPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                Vector3 hitPoint = hit.point;
                hitPoint.x = Mathf.Floor(hitPoint.x / tileSize) * tileSize + tileSize / 2.0f;
                hitPoint.z = Mathf.Floor(hitPoint.z / tileSize) * tileSize + tileSize / 2.0f;

                targetPosition = hitPoint;
            }
        }
        MoveToPosition(targetPosition);
    }

    void MoveToPosition(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 direction = (target - transform.position).normalized;
            direction.y = 0;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}