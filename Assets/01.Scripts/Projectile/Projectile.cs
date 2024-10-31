using System.Collections;
using Crogen.CrogenPooling;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolingObject
{
    [SerializeField] protected float _duration = 0.1f;
    [SerializeField] protected TeamType _team;
    public string OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }
    
    public void OnPop()
    {
        StartCoroutine(CoroutineOnDie());
    }

    public void OnPush()
    {
        StopAllCoroutines();
    }

    private IEnumerator CoroutineOnDie()
    {
        yield return new WaitForSeconds(_duration);
        this.Push();
    }
}
