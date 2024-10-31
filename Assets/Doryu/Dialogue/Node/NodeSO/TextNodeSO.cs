using Doryu.Dialogue;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "TextNode", menuName = "SO/Dialogue/TextNode")]
public class TextNodeSO : SingleNodeSO
{
    [SerializeField] private ElementNodeSO elementStartNode;
    private ElementNodeSO elementEndNode;

    public TextEffectMachine TextMachine { get; private set; }

    public string _actorName;
    private ActorData _actor;

    protected override void Start()
    {
        base.Start();

        List<ElementNodeSO> elementNodeSOs = new List<ElementNodeSO>();
        elementStartNode.TravelAcion(childNodeSO =>
        {
            elementNodeSOs.Add(childNodeSO as ElementNodeSO);
            childNodeSO.SetController(_controller);
        });

        _actor.titleTextMesh.text = _actor.name;
        TextMachine = new TextEffectMachine(_actor.talkTextMesh);

        StringBuilder textSB = new StringBuilder();
        elementNodeSOs.ForEach(node =>
        {
            if (node is TextElementNodeSO textElement)
            {
                textSB.Append(textElement.GetText());
            }
        });
        TextMachine.SetText(textSB.ToString());

        int startIdx = 0;
        elementNodeSOs.ForEach(node =>
        {
            node.Init(this);
            if (node is TextElementNodeSO textElement)
            {
                startIdx += textElement.GetText().Length;
            }
        });

        StringBuilder tagTextSB = new StringBuilder();
        elementNodeSOs.ForEach(node =>
        {
            if (node is TextElementNodeSO textElement)
            {
                tagTextSB.Append(textElement.GetText(true));
            }
        });
        TextMachine.Init(tagTextSB.ToString());

        elementStartNode.Enter();
        elementEndNode = elementNodeSOs[^1];
        elementEndNode.onEndEvent += Exit;
    }

    protected override void Update()
    {
        base.Update();

        TextMachine.TextUpdate();
    }

    public override void SetController(DialogueSystemController controller)
    {
        base.SetController(controller);
        _actor = _controller.GetActor(_actorName);
    }

    protected override void End()
    {
        base.End();

        TextMachine = null;
        elementEndNode.onEndEvent -= Exit;
    }
}