using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using TMPro;

public class turn : MonoBehaviour
{
    public bool TurnSystem, turnon;

    public GameObject TurnButton;
    
    public GameObject Turneffect;

    public float nextturnTimer;
    public float resetTurnTimer;
    
    public TMP_Text tmpText;
    
    private EnemyAIBattle EAIB;
    private battleController BC;
    private PlayerStats PS;
    private void Start()
    {
        resetTurnTimer = nextturnTimer;
        EAIB = FindObjectOfType<EnemyAIBattle>();
        BC = FindObjectOfType<battleController>();
        PS = FindObjectOfType<PlayerStats>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TurnSystem = true;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            TurnSystem = false;
        }
    }
    public void Turn()
    {
        TurnButton.SetActive(false);
        Turneffect.SetActive(true);
        tmpText.text = "Enemies Turn";
        
        StartCoroutine(turnprocess());
    }

    IEnumerator turnprocess()
    {
        yield return new WaitForSeconds(nextturnTimer);
        
        Turneffect.SetActive(false);
        BC.remainingMovementRange = BC.ResetMovementRange;
        TurnSystem = false;
        BC.attack = true;
        resettimer();
        
    }

    public void enemyturn()
    {
        Turneffect.SetActive(true);
        tmpText.text = "Player's Turn";
        StartCoroutine(enemyprocess());
    }

    IEnumerator enemyprocess()
    {
        yield return new WaitForSeconds(nextturnTimer);
        
        TurnButton.SetActive(true);
        Turneffect.SetActive(false);
        EAIB.remainingMoves = EAIB.resetMovement;
        TurnSystem = true;
        EAIB.attack = true;
        resettimer();
    }

    private void resettimer()
    {
        nextturnTimer = resetTurnTimer;
    }
}
