using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    [SerializeField] private GameObject[] spawnpoint;
    
    public GameObject portal;
    
    void Start()
    {
        if (!IsBattleScene())
        {
            InstantiatePlayerAtClosestSpawnPoint();
        }
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            Vector3 playerPos = new Vector3(x, y, z);
            Instantiate(playerPrefab, playerPos, Quaternion.identity);
        }
    }
    private bool IsBattleScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name.Contains("Battle");
    }
    private void InstantiatePlayerAtClosestSpawnPoint()
    {
        Vector3 spawnPosition = Vector3.zero;
        
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            spawnPosition = new Vector3(x, y, z);
        }
        else
        {
            GameObject closestSpawnPoint = FindClosestSpawnPoint();
            if (closestSpawnPoint != null)
            {
                spawnPosition = closestSpawnPoint.transform.position;
            }
        }

        // Adjust the spawn position to be slightly above the closest spawn point
        spawnPosition += Vector3.up * 2f; // Adjust the value according to your preference

        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }

    private GameObject FindClosestSpawnPoint()
    {
        GameObject closestSpawnPoint = null;
        float closestDistance = Mathf.Infinity;
        
        Vector3 portalPosition = portal.transform.position;
        
        foreach (GameObject spawnPoint in spawnpoint)
        {
            float distance = Vector3.Distance(portalPosition, spawnPoint.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSpawnPoint = spawnPoint;
            }
        }

        return closestSpawnPoint;
    }
}
