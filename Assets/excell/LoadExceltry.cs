using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif
public class LoadExceltry : MonoBehaviour
{
    public Item BlankItem;
    public List<Item> ItemDatabase = new List<Item>();

    public TextAsset text;

    public string textName, teamName;
    
    public void loadItemData()
    {
        ItemDatabase.Clear();

        List<Dictionary<string, object>> data = CSVReader.Read(text.ToString());
        
        for (var i = 0; i < data.Count; i++) 
        {
            if (data[i].ContainsKey(textName))
            {
                string text = data[i][textName].ToString();
                
                AddItem(text);       
            }
            else
            {
                Debug.Log(" help");
            }
        }

        
    }
    
    void AddItem(string text)
    {
        Item tempItem = new Item(BlankItem);
        
        tempItem.text = text;
        
        ItemDatabase.Add(tempItem);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LoadExceltry))] public class ItemEditor1 : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LoadExceltry loadExceltry = (LoadExceltry)target;
        
        GUILayout.Label(" Reload Item DataBase", EditorStyles.boldLabel);
        if (GUILayout.Button("reload Items"))
        {
            loadExceltry.loadItemData();
        }
    }
}
#endif
