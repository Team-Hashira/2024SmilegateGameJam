using UnityEngine;

namespace Doryu.Dialogue
{
    [CreateAssetMenu(fileName = "RootNodeSO", menuName = "SO/Dialogue/RootNode")]
    public class RootNodeSO : SingleNodeSO
    {
        protected override void Start()
        {
            base.Start();
            childNodeSO.TravelAcion(childNodeSO => childNodeSO.SetController(_controller));
            Exit();
        }
    }
}