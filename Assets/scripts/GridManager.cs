using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

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


    private Dictionary<Vector3, Tile> _tiles;

    public bool MapIsMade;

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

    void removeOldGrid()
    {
        if (_tiles != null)
        {
            foreach (var tile in _tiles.Values)
            {
                DestroyImmediate(tile.gameObject);
            }
            _tiles.Clear();
            
            DestroyImmediate(GameObject.FindGameObjectWithTag("Player"));
            DestroyImmediate(GameObject.FindGameObjectWithTag("Enemy"));
            
            foreach (var obj in ParentTiles.GetComponentsInChildren<Transform>())
            {
                if (obj != ParentTiles.transform)
                {
                    DestroyImmediate(obj.gameObject);
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
    }
}
