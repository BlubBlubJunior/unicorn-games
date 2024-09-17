using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int xp;
    [SerializeField] private float xpToNextLevel;
    [SerializeField] private float MaxHp;
    [SerializeField] private float currentHP;
    
    [SerializeField] public float Strength;
    [SerializeField] public float Defence;
    [SerializeField] public float damage;
    
    public TMP_Text HpText;
    
    private bool isSelected;

    public GameObject selectobjectUi;
    public GameObject HealthUi;

    public bool IsSelected => isSelected;

    private void Start()
    {
        currentHP = MaxHp;
    }

    private void Update()
    {
        HpText.text = currentHP + " / " + MaxHp;
        damage = Strength;
        
        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }

        /*if (xp >= xpToNextLevel)
        {
            level += 1;
            xpToNextLevel *= 1.25f;
            MaxHp *= 1.05f;
            Strength *= 1.05f;
            Defence *= 1.05f;
        }*/
    }
    
    public void TakeDamage(float damage)
    {
        if (currentHP != null)
        {
            currentHP -= damage;
        }
    }
    
    
    
    public void Select()
    {
        
        isSelected = true;
        // Add any visual feedback for selection here
        selectobjectUi.SetActive(true);
        HealthUi.SetActive(true);
    }

    public void Deselect()
    {
        isSelected = false;
        // Remove any visual feedback for deselection here
        selectobjectUi.SetActive(false);
        HealthUi.SetActive(false);
    }
}

