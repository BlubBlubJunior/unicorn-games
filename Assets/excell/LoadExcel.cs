using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class LoadExcel : MonoBehaviour
{
    public Item BlankItem;
    public List<Item> ItemDatabase = new List<Item>();

    public void loadItemData()
    {
        ItemDatabase.Clear();
        
        List<Dictionary<string, object>> data = CSVReader.Read("ItemDatabase");
        
        for (var i = 0; i < data.Count; i++)
        {
            int id = int.Parse(data[i]["id"].ToString(), System.Globalization.NumberStyles.Integer);
            string name = data[i]["name"].ToString();
            string descrioption = data[i]["descrioption"].ToString();

            AddItem(id, name, descrioption);
            print(id + name + descrioption);
        }
    }

    void AddItem(int id, string name, string descrioption)
    {
        Item tempItem = new Item(BlankItem);
    print(" gafwoif");
        tempItem.id = id;
        tempItem.name = name;
        tempItem.descrioption = descrioption;
        
        ItemDatabase.Add(tempItem);
    }
}
[CustomEditor(typeof(LoadExcel))] public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LoadExcel loadExcel = (LoadExcel)target;
        
        GUILayout.Label(" Reload Item DataBase", EditorStyles.boldLabel);
        if (GUILayout.Button("reload Items"))
        {
           loadExcel.loadItemData();
        }
    }
}
