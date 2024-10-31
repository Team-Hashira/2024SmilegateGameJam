using System;
using System.Collections.Generic;
using UnityEngine;

namespace Doryu.Dialogue
{
    public class SingleNodeSO : NodeSO
    {
        public NodeSO childNodeSO;

        public Action onEndEvent;

        protected override void End()
        {
            if (childNodeSO)
                childNodeSO.Enter();
            else
                onEndEvent?.Invoke();
        }
    }
}
