using Crogen.CrogenPooling;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolingObject
{
    [SerializeField] protected TeamType _team;
    public string OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }
    
    public void OnPop()
    {
        
    }

    public void OnPush()
    {
    }
}
