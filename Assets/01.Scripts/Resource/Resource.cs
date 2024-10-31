using System;
using UnityEngine;
using UnityEngine.Events;

public enum ResourceType
{
    None, Gold, Wheat 
}

[Serializable]
public class Resource
{
    public ResourceType type;
    public UnityEvent<string> OnAmountChangedEvent;
    public int count;
}
