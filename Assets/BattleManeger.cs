using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BattleManeger : MonoBehaviour
{
    bool meleattack = false;
    public List<EnemyStats> enemyList = new List<EnemyStats>();

    private void Start()
    {
        FindAllEnemies();
    }
    void Update()
    {
        if (meleattack == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        PlayerStats stats = hit.collider.GetComponent<PlayerStats>();
                        DoAttack(stats);
                    }
                }
            }
        }
    }
    void FindAllEnemies()
    {
        
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemyObject in enemyObjects)
        {
            EnemyStats enemyStats = enemyObject.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyList.Add(enemyStats);
            }
        }
    }
    
    void DoAttack(PlayerStats stats)
    {
        
        foreach (EnemyStats enemyStats in enemyList)
        {
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(stats);
            }
        }
        meleattack = false;
    }
    public void attack()
    {
        meleattack = true;
    }
}
