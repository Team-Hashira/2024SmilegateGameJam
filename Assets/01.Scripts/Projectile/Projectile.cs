using Crogen.CrogenPooling;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolingObject
{
    public string OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }
    
    public void OnPop()
    {
        
    }

    public void OnPush()
    {
    }
}
