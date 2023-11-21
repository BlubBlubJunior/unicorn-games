using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : MonoBehaviour
{
        public float moveSpeed = 5f;
        public LayerMask obstacleMask;
    
        private bool isMoving = false;
    
        void Update()
        {
            if (!isMoving)
            {
                HandleMovement();
            }
        }
    
        void HandleMovement()
        {
            if (Input.GetMouseButtonDown(0)) // assuming left mouse button for movement
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
    
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, obstacleMask))
                {
                    StartCoroutine(MoveToTile(hit.point));
                }
            }
        }
    
        IEnumerator MoveToTile(Vector3 targetPosition)
        {
            isMoving = true;
    
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
    
            isMoving = false;
        }
}
