using Crogen.HealthSystem;
using Gondr.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    public AstarAgent AStarAgentCompo { get; protected set; }
    public Collider2D ColliderCompo { get; protected set; }
    public HealthSystem HealthSystemCompo { get; protected set; }

    public Transform settleTrm;
    [SerializeField]
    private HPBar _hpBar;
    [SerializeField]
    private float _speed = 5;

    private List<Vector3> _path;

    private void Awake()
    {
        HealthSystemCompo = GetComponent<HealthSystem>();
        AStarAgentCompo = GetComponent<AstarAgent>();
        AStarAgentCompo.Initialize(this);
        _hpBar.Initialize(HealthSystemCompo);
    }

    public void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            HealthSystemCompo.Hp -= 10;
        }
    }

    public void SetDestination(Vector3 position)
    {
        AStarAgentCompo.SetDestination(position);
        _path = AStarAgentCompo.GetPath();
        settleTrm.position = _path[_path.Count - 1];
        StartCoroutine(TestPathfinding());
    }

    private IEnumerator TestPathfinding()
    {
        for(int i = 0; i <  _path.Count; i++)
        {
            float percent = 0;
            Vector3 origin = transform.position;
            while(percent < 1)
            {
                percent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(origin, _path[i], percent);
                yield return null;
            }
        }
    }
}
