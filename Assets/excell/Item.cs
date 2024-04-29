using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string descrioption;

    public Item(Item d)
    {
        id = d.id;
        name = d.name;
        descrioption = d.descrioption;
    }
}
