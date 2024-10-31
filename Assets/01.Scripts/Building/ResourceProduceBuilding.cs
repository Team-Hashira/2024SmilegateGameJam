using System;
using UnityEngine;

public class ResourceProduceBuilding : MonoBehaviour
{
    private Building _owner;
    public bool canProduce = true;

    public ResourceType resourceType;    
    public float _delay = 2.0f;
    private float _curTime = 0.0f;
    public int amount = 1;
    
    public event Action<float> OnProducePercentEvent; 
    public event Action OnProduceCompleteEvent;
    
    private void Awake()
    {
        _owner = GetComponent<Building>();
    }

    private void Update()
    {
        _curTime += Time.deltaTime;
        OnProducePercentEvent?.Invoke(_curTime/_delay);
        if (_curTime >= _delay)
        {
            _curTime = 0.0f;
            ResourceManager.Instance.AddResource(resourceType, amount);   
            OnProduceCompleteEvent?.Invoke();
        }
    }
}
