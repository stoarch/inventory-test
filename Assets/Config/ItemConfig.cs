using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Item1,
    Item2, 
    Item3
}

[CreateAssetMenu(fileName = "cfg", menuName = "ScriptableObjects/ItemConfig", order=1)]
public class ItemConfig : ScriptableObject
{
    public int Weight;
    public string Name;
    public Guid UID;
    public ItemType ItemType;
    public string Id;
    
}
