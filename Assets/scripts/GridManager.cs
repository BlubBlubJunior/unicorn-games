using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _Width,_Deept;

    [SerializeField] private Tile _TilePrefab;

    [SerializeField] private Transform Cam;

    private Dictionary<Vector3, Tile> _tiles;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector3, Tile>();
        for (int x = 0; x < _Width; x++)
        {
            for (int z = 0; z < _Deept; z++)
            {
                var spawnedTile = Instantiate(_TilePrefab, new Vector3(x,0, z), Quaternion.Euler(90 ,0,0));
                spawnedTile.name = $"Tile {x} {z}";

                var isOffset = (x % 2 == 0 && z % 2 != 0) || (x % 2 != 0 && z % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector3(x, 0, z)] = spawnedTile;
            }
        }
    }

    public Tile GetTileAtPosition(Vector3 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
