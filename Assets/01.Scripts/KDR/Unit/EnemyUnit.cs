using UnityEngine;

public class EnemyUnit : Unit
{
    private Vector3 _targetPath;

    public void SetPath(Vector3 targetPos)
    {
        _targetPath = targetPos;
    }
}
