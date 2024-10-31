using Crogen.HealthSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    public NavMeshAgent NavAgentCompo { get; protected set; }
    public Collider2D ColliderCompo { get; protected set; }
    public HealthSystem HealthSystemCompo { get; protected set; }

    [SerializeField]
    private HPBar _hpBar;

    private void Awake()
    {
        HealthSystemCompo = GetComponent<HealthSystem>();
        _hpBar.Initialize(HealthSystemCompo);
    }

    public void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            HealthSystemCompo.Hp -= 10;
        }
    }
}
