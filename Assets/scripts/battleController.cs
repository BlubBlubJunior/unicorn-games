using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class battleController : MonoBehaviour
{
    [Tooltip("How fast you move over the tiles.")]
    public float MovementSpeed = 1f;
    [Tooltip("How big the steps are the player takes over the tiles.")]
    public float gridSize = 1f; 
    [Tooltip("Resets remainingMovementRange when turn starts.")]
    public int ResetMovementRange = 5; 
    [Tooltip("How many steps you can take.")]
    public int remainingMovementRange;
    
    
    [Tooltip("The layer the character moves over and raycast looks for when playing.")]
    public LayerMask groundLayer; 
    [Tooltip("layer of the enemy when attacking.")]
    public LayerMask enemyLayer;
    [Tooltip("particles for attacking.")]
    public GameObject particles;

    
    private Vector3 targetPosition; 
    private bool canMove = true;
    private GameObject selectedUnit;
    private PlayerStats selectedPlayerStats;
    private GridManager gridManager;

    public bool playerTurn;
    private bool walking;
    void Start()
    {
        selectedPlayerStats = GetComponent<PlayerStats>();
        remainingMovementRange = ResetMovementRange; //resetting movement
        targetPosition = transform.position;
        gridManager = FindObjectOfType<GridManager>(); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove && playerTurn == true)
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
                else if (selectedUnit != null && remainingMovementRange > 0)
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
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, MovementSpeed * Time.deltaTime);
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
        
        gridManager.highLightTilesInRange(selectedUnit.transform.position, remainingMovementRange);
    }

    void moveToPosition(Vector3 destination)
    {
        Ray ray = new Ray(transform.position, destination - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, remainingMovementRange * gridSize, groundLayer))
        {
            Vector3 gridPosition = RoundToNearestGrid(hit.point);
            float distance = Vector3.Distance(transform.position, gridPosition) / gridSize;

            if (distance <= remainingMovementRange && !isTileOccupied(gridPosition) && selectedUnit != null && selectedUnit.transform == transform)
            {
                walking = true;
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

                if (walking == true)
                {
                    remainingMovementRange -= Mathf.CeilToInt(Mathf.Abs(destination.x - currentPos.x));
                    walking = false;
                }
               
            }
            else if (Mathf.Abs(diff.z) > 0.1f)
            {
                float targetZ = Mathf.MoveTowards(currentPos.z, destination.z, gridSize);
                targetPosition = new Vector3(currentPos.x,currentPos.y,targetZ);

                if (walking == true)
                {
                    remainingMovementRange -= Mathf.CeilToInt(Mathf.Abs(destination.z - currentPos.z));
                    walking = false;
                }
                
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