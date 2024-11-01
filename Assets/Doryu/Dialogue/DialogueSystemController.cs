using Doryu.Dialogue;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public struct ActorData
{
    public string name;
    public TMP_Text titleTextMesh;
    public TMP_Text talkTextMesh;
    public Sprite[] images;
}
[Serializable]
public enum EActor
{
    Player,
    TestActor,
}

public class DialogueSystemController : MonoBehaviour
{
    public ActorData[] actorDatas;

    public RootNodeSO rootNodeSO;
    public event Action<EDialogueEvent> onDialogueEvent;

    private Dictionary<string, ActorData> _actorDataDict
         = new Dictionary<string, ActorData>();

    private void Awake()
    {
        foreach (ActorData actorData in actorDatas)
        {
            _actorDataDict.Add(actorData.name, actorData);
        }
    }

    private void Start()
    {
        rootNodeSO.SetController(this);
        rootNodeSO.Enter();
    }

    public ActorData GetActor(string name)
    {
        return _actorDataDict[name];
    }

    public void DialogueEvent(EDialogueEvent eDialogueEvent)
    {
        Debug.Log(eDialogueEvent.ToString() + "이벤트 발생!");
        onDialogueEvent?.Invoke(eDialogueEvent);

        if (eDialogueEvent == EDialogueEvent.ChangeScene)
        {
            Debug.Log("Scene변경");
        }
    }
}