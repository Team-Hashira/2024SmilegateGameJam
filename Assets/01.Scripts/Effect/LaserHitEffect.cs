using Crogen.CrogenPooling;
using UnityEngine;

public class LaserHitEffect : MonoBehaviour, IPoolingObject
{
    [SerializeField] private Material _material;
    [SerializeField] private CircleDamageCaster2D _circleDamageCaster;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] 
    private readonly int _colorPropertyID = Shader.PropertyToID("_Color");

    public string OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }
    
    public void Init(TeamType teamType, int damage)
    {
        if(teamType == TeamType.Blue)
            _material.SetColor(_colorPropertyID, Color.blue);
        else 
            _material.SetColor(_colorPropertyID, Color.red);
        
        _circleDamageCaster.CastDamage(damage);
    }

    
    public void OnPop()
    {
        _particleSystem.Play(true);
    }

    public void OnPush()
    {
        
    }
}
