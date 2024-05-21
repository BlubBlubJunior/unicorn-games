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
    
    private bool isSelected;

    public GameObject selectobject;

    public bool IsSelected => isSelected;
    private void Update()
    {
        damage = Strength;
    }
    
    public void Select()
    {
        isSelected = true;
        // Add any visual feedback for selection here
        selectobject.SetActive(true);
    }

    public void Deselect()
    {
        isSelected = false;
        // Remove any visual feedback for deselection here
        selectobject.SetActive(false);
    }
}

