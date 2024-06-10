using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class turn : MonoBehaviour
{
    public EnemyAIBattle _enemyAIBattle;
    public battleController _battleController;

    public bool TurnSystem;

    public GameObject TurnButton;
    
    public GameObject Turneffect;
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
        if (TurnSystem == true)
        {
            _enemyAIBattle.EnemyTurn = false;
            _battleController.playerTurn = true;
            //_battleController.remainingMovementRange = _battleController.ResetMovementRange;
        }
        else if (TurnSystem == false)
        {
            _enemyAIBattle.EnemyTurn = true;
            _battleController.playerTurn = false;
            //_enemyAIBattle.remainingMoves = _enemyAIBattle.resetMovement;
        }
        
    }
    public void Turn()
    {
        TurnButton.SetActive(false);       
    }
}
