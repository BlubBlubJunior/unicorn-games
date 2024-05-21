using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _Width,_Deept;

    [SerializeField] private Tile _TilePrefab;
    
    
    [SerializeField] private GameObject _PlayerPrefab;
    
    public Vector3 tileZeroZero;

    private float distanceground = 0.2f;

    [SerializeField] private GameObject ParentTiles;

    [SerializeField] private Vector3 spawnpointEnemie;
    [SerializeField] private GameObject Enemie;

    public List<GameObject> spawnedEnemies;

    private Dictionary<Vector3, Tile> _tiles;

    public bool MapIsMade;

    public int randomspawn;

    private void Start()
    {
        if (MapIsMade == false)
        {
            GenerateGrid();
            InstantiatePlayerOnTileZeroZero();     
        }

        if (_tiles == null)
        {
            MapIsMade = false;
        }
    }

    public void highLightTilesInRange(Vector3 center, int range)
    {
        Debug.Log("fux");
        if (_tiles == null)
        {
            Debug.LogWarning("Tiles dictionary is not initialized.");
            return;
        }
        Debug.Log("heck");
        foreach (var tile in _tiles.Values)
        {
            float distance = Vector3.Distance(center, tile.transform.position);
            if (distance <= range)
            {
                tile.HighLight(true);
                Debug.Log("check");
            }
            {
                tile.HighLight(false);
            }
        }
    }
    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector3, Tile>();
        for (int x = 0; x < _Width; x++)
        {
            for (int z = 0; z < _Deept; z++)
            {
                Vector3 spawnPositon = new Vector3(x, 0, z);
                if (!isPositionOccupied(spawnPositon))
                {
                    var spawnedTile = Instantiate(_TilePrefab, spawnPositon, Quaternion.Euler(90, 0, 0)); 
                    randomspawn = Random.Range(0, 1000);
                    if (randomspawn == 1 )
                    {
                        var spawnEnemy = Instantiate(Enemie, spawnPositon, quaternion.identity); 
                        spawnEnemy.transform.SetParent(ParentTiles.transform);
                        spawnedEnemies.Add(spawnEnemy);
                    }
                    spawnedTile.name = $"Tile {x} {z}";
                    spawnedTile.transform.SetParent(ParentTiles.transform);

                    var isOffset = (x % 2 == 0 && z % 2 != 0) || (x % 2 != 0 && z % 2 == 0);
                    spawnedTile.Init(isOffset);

                    _tiles.Add(spawnPositon, spawnedTile);    
                }
            }
        }
    }

    bool isPositionOccupied(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f);

        foreach(Collider col in colliders)
        {
            if(col.CompareTag("obs"))
            {
                return true;     
            }
           
        }

        return false;
    }
    void InstantiatePlayerOnTileZeroZero()
    {
        Instantiate(_PlayerPrefab, tileZeroZero, Quaternion.identity);

        Instantiate(Enemie, spawnpointEnemie, quaternion.identity);
    }

    public void regererateGrid()
    {
        removeOldGrid();
        GenerateGrid();
        InstantiatePlayerOnTileZeroZero();
        MapIsMade = true;
    }

    public void removeOldGrid()
    {
        if (_tiles != null)
        {
            foreach (var tile in _tiles.Values)
            {
                DestroyImmediate(tile.gameObject);
            }
            _tiles.Clear();
            
            spawnedEnemies.Clear();
            
            DestroyImmediate(GameObject.FindGameObjectWithTag("Player"));
            DestroyImmediate(GameObject.FindGameObjectWithTag("Enemy"));
            
            foreach (var obj in ParentTiles.GetComponentsInChildren<Transform>())
            {
                if (obj != ParentTiles.transform)
                {
                    DestroyImmediate(obj.gameObject);
                    DestroyImmediate(GameObject.FindGameObjectWithTag("Enemy"));
                }
            }     
        }
       
    }
}

[CustomEditor(typeof(GridManager))] public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridManager gridManager = (GridManager)target;

        if (GUILayout.Button("Regenerate Grid"))
        {
            gridManager.regererateGrid();
        }
        
        if (GUILayout.Button("Remove Grid"))
        {
            gridManager.removeOldGrid();
        }
    }
}
