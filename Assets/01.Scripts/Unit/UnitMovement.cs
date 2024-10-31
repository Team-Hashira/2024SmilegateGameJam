using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Unit _owner;
    private List<Vector3> _path;

    [SerializeField]
    private float _speed = 5;

    private Coroutine _moveCoroutine;

    public void Initalize(Unit owner)
    {
        _owner = owner;
    }

    public void SetDestination(Vector3 position)
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
        _owner.AStarAgentCompo.SetDestination(position);
        _path = _owner.AStarAgentCompo.GetPath();
        _path[_path.Count - 1] += (Vector3)Random.insideUnitCircle * 0.8f;
        _moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        for (int i = 0; i < _path.Count; i++)
        {
            float percent = 0;
            Vector3 origin = transform.position;
            while (percent < 1)
            {
                percent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(origin, (Vector2)_path[i], percent);
                yield return null;
            }
        }
    }
}
