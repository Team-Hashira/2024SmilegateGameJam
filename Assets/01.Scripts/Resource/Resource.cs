using System;
using UnityEngine;

public enum ResourceType
{
    None, Wood, Metal, food, wheat 
}

[Serializable]
public class Resource
{
    public ResourceType type;
    public int count;
}
