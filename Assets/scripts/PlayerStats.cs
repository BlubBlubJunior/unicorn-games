using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using Random = UnityEngine.Random;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private float xp;
    [SerializeField] private float xpToNextLevel;
    [SerializeField] private float MaxHp;
    [SerializeField] private float currentHP;
    
    [SerializeField] public float Strength;
    [SerializeField] public float Defence;
    [SerializeField] public float damage;
    
    public TMP_Text HpText;
    public TMP_Text strength_text;
    public TMP_Text defence_text;
    public TMP_Text level_text;
    public TMP_Text xpToNextLevel_text;
    public TMP_Text XP_text;

    public bool statsscreen;

    [SerializeField] private GameObject playercanvas;
    [SerializeField] private GameObject player;

    [SerializeField] private float timerInfONOrOff;
    
    
    private bool isSelected;

    public GameObject selectobjectUi;
    public GameObject HealthUi;

    public bool IsSelected => isSelected;

    private void Start()
    {
        currentHP = MaxHp;

        GameObject level_component = GameObject.Find("level");
        level_text = level_component.GetComponent<TMP_Text>();
        
        GameObject hp_component = GameObject.Find("hp");
        HpText = hp_component.GetComponent<TMP_Text>();
        
        GameObject strenght_component = GameObject.Find("strength");
        strength_text = strenght_component.GetComponent<TMP_Text>();
        
        GameObject defence_component = GameObject.Find("defence");
        defence_text = defence_component.GetComponent<TMP_Text>();
        
    }

    private void Update()
    {
        timerInfONOrOff -= Time.deltaTime;
        //HpText.text = currentHP + " / " + MaxHp;
        damage = Strength;
        
        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }

        if (xp >= xpToNextLevel)
        {
            int randomMaxHpIncrease = Random.Range(1, 5);
            int randomSrenghtIncrease = Random.Range(1, 5);
            int randomdefenceIncrease = Random.Range(1, 5);
            
            
            level += 1;
            xpToNextLevel *= 3f;
            MaxHp += randomMaxHpIncrease;
            Strength += randomSrenghtIncrease;
            Defence += randomdefenceIncrease;

            MaxHp = Mathf.Ceil(MaxHp);
            Strength = Mathf.Ceil(Strength);
            Defence = Mathf.Ceil(Defence);
            xp = Mathf.Ceil(xp);
            xpToNextLevel = Mathf.Ceil(xpToNextLevel);
            
            xp -= xpToNextLevel;
        }

        if (Input.GetKeyDown(KeyCode.E) && statsscreen == false && timerInfONOrOff <= 0)
        {
            timerInfONOrOff = 0.1f;
            statsscreen = true;
            player.SetActive(false);
            playercanvas.SetActive(true);
            level_text.text = "lv. " + level.ToString();
            HpText.text = "HP: " + currentHP + " / " + MaxHp;
            strength_text.text = "AKT: " + Strength.ToString();
            defence_text.text = "DEF: " + Defence.ToString();
        }
        
        if (Input.GetKeyDown(KeyCode.E) && statsscreen == true && timerInfONOrOff <= 0)
        {
            timerInfONOrOff = 0.1f;
            player.SetActive(true);
            statsscreen = false;
            playercanvas.SetActive(false);
        }
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

