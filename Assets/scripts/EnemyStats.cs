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
    
    public void TakeDamage(float damage)
    {
        if (currentHealth != null)
        {
            currentHealth -= damage;
            Debug.Log(currentHealth + "lower");
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
