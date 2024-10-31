using Doryu.Dialogue;
using UnityEngine;


[CreateAssetMenu(fileName = "WaitNodeSO", menuName = "SO/Dialogue/WaitNode")]
public class WaitNodeSO : ElementNodeSO
{
    public float time;
    private float _startTime;

    protected override void Start()
    {
        base.Start();
        _startTime = Time.time;
    }

    protected override void Update()
    {
        base.Update();
        if (_startTime + time < Time.time)
        {
            Exit();
        }
    }
}