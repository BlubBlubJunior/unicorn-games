using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Item
{
    public string text;
    public int team;
    public int question;
    public int madness;

    public Item(Item d)
    {
        text = d.text;
        team = d.team;
        question = d.question;
        madness = d.madness;
    }
}
