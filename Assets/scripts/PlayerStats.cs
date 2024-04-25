using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float level;
    [SerializeField] private float xp;
    [SerializeField] private float MaxXp;
    [SerializeField] public float Health;
    [SerializeField] public float Strength;
    [SerializeField] public float Defence;
    [SerializeField] public float damage;
    
    private void Update()
    {
        damage = Strength;
    }
}

