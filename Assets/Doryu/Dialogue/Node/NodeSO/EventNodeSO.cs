using Doryu.Dialogue;
using UnityEngine;

public enum EDialogueEvent
{
    Close
}

[CreateAssetMenu(fileName = "EventNodeSO", menuName = "SO/Dialogue/EventNode")]
public class EventNodeSO : SingleNodeSO
{
    public EDialogueEvent eDialogueEvent;
    protected override void Start()
    {
        base.Start();

        _controller.DialogueEvent(eDialogueEvent);
        Exit();
    }
}