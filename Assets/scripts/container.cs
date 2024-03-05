using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable] public class container
{
    public GameObject spawnpoint;
    public GameObject enemyPrefab;
    public Vector3 reach;
    
    public float Timer;
    public float ResetTimer;

    public int EnemiesSpawned;
    public int MaxEnemiesCanSpawn;
}
