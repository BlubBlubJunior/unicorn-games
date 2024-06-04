using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class battleController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float gridSize = 1f;
    public int maxMoveRange = 5;
    public int remainingMoveRange;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public GameObject particles;

    
    private Vector3 targetPosition;
    public bool canMove = true;
    private GameObject selectedUnit;
    private PlayerStats selectedPlayerStats;
    public GridManager gridManager;
    void Start()
    {
        selectedPlayerStats = GetComponent<PlayerStats>();
        targetPosition = transform.position;
        remainingMoveRange = maxMoveRange;
        gridManager = FindObjectOfType<GridManager>(); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.CompareTag("Player"))
                {
                    SelectUnit(clickedObject);
                }
                else if (selectedUnit != null && remainingMoveRange > 0)
                {
                    moveToPosition(hit.point);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && selectedUnit != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {
                Vector3 enemyposition = hit.collider.transform.position;
                float distance = Vector3.Distance(transform.position, enemyposition) / gridSize;

                if (distance <= 1)
                {
                    EnemyStats enemyHealth = hit.collider.GetComponent<EnemyStats>();
                    enemyHealth.TakeDamage(selectedPlayerStats.damage);
                    GameObject particle = Instantiate(particles, hit.transform.position, quaternion.identity);
                    StartCoroutine(DestroyParticlesAfterDelay(particle, 5f));
                }
            }
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void SelectUnit(GameObject unit)
    {
        if (selectedUnit == unit)
        {
            selectedPlayerStats.Deselect();
            selectedUnit = null;
            selectedPlayerStats = null;
            return;
        }
        
        if (selectedUnit != null)
        {
            selectedPlayerStats.Deselect();
        }
        
        selectedUnit = unit;
        selectedPlayerStats = unit.GetComponent<PlayerStats>();
        selectedPlayerStats.Select();
        
        gridManager.highLightTilesInRange(selectedUnit.transform.position, remainingMoveRange);
    }

    void moveToPosition(Vector3 destination)
    {
        Ray ray = new Ray(transform.position, destination - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, remainingMoveRange * gridSize, groundLayer))
        {
            Vector3 gridPosition = RoundToNearestGrid(hit.point);
            float distance = Vector3.Distance(transform.position, gridPosition) / gridSize;

            if (distance <= remainingMoveRange && !isTileOccupied(gridPosition) && selectedUnit != null && selectedUnit.transform == transform)
            {
                canMove = false;
                StartCoroutine(moveToDestination(gridPosition));
            }
        }
    }
    
    IEnumerator DestroyParticlesAfterDelay(GameObject particle, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(particle);
    }

    IEnumerator moveToDestination(Vector3 destination)
    {
        while (transform.position != destination)
        {
            Vector3 currentPos = transform.position;
            Vector3 diff = destination - transform.position;

            if (Mathf.Abs(diff.x) > 0.000001f)
            {
                float targetX = Mathf.MoveTowards(currentPos.x, destination.x, gridSize);
                targetPosition = new Vector3(targetX, currentPos.y, currentPos.z);
                
                remainingMoveRange -= Mathf.CeilToInt(Mathf.Abs(targetX - currentPos.x));
            }
            else if (Mathf.Abs(diff.z) > 0.1f)
            {
                float targetZ = Mathf.MoveTowards(currentPos.z, destination.z, gridSize);
                targetPosition = new Vector3(currentPos.x,currentPos.y,targetZ);

                remainingMoveRange -= Mathf.CeilToInt(Mathf.Abs(targetZ - currentPos.z));
            }
            
            transform.position = Vector3.MoveTowards(currentPos, targetPosition, gridSize * Time.deltaTime);
            
            yield return null;
        }

        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            canMove = true;
        }
    }

    Vector3 RoundToNearestGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(x,transform.position.y, z);
    }

    bool isTileOccupied(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f, groundLayer);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
}