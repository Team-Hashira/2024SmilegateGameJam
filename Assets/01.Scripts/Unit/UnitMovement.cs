using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UnitMovement : MonoBehaviour, IUnitComponent
{
    private Unit _owner;
    private List<Vector3> _path;

    [SerializeField]
    private float _speed = 5;

    private Coroutine _moveCoroutine;

    public event Action OnMoveEndEvent;

    public void AfterInit()
    {

    }

    public void Dispose()
    {

    }

    public void Initialize(Unit agent)
    {
        _owner = agent;
    }

    public void StopMove()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }

    public void SetDestination(Vector3 position)
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
        _owner.AStarAgentCompo.SetDestination(position);
        _path = _owner.AStarAgentCompo.GetPath();
        if (_path.Count == 0) return;
        _path[_path.Count - 1] += (Vector3)UnityEngine.Random.insideUnitCircle * 0.8f;
        _moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        for (int i = 0; i < _path.Count; i++)
        {
            float percent = 0;
            Vector3 origin = transform.position;
            Vector3 dir = _path[i] - transform.position;
            if (dir.x < 0)
                예외처리_owner.Flip(-1);
            else
                _owner.Flip(1);
            while (percent < 1)
            {
                percent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(origin, (Vector2)_path[i], percent);
                yield return null;
            }
        }
        OnMoveEndEvent?.Invoke();
    }
}
