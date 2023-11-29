using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] public float currentHealth;
    [SerializeField] public float Attack;
    [SerializeField] public float Defence;
    public List<PlayerStats> playerList = new List<PlayerStats>();
    private void Start()
    {
        FindAllPlayers();
    }

    public void TakeDamage(PlayerStats stats)
    {
        Debug.Log(currentHealth + "lower");
            if (currentHealth != null)
            {
                currentHealth -= stats.Attack;
            }
    }

    void FindAllPlayers()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (GameObject playerObject in playerObjects)
        {
            PlayerStats Stats = playerObject.GetComponent<PlayerStats>();

            if (Stats != null)
            {
                playerList.Add(Stats);
            }
        }
    }
    

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
