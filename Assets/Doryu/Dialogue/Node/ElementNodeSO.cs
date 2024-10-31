using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

namespace Doryu.Dialogue
{
    public class ElementNodeSO : SingleNodeSO
    {
        public TextNodeSO OwnerNodeSO { get; private set; }
        public string TotalText { get; private set; }

        public virtual void Init(TextNodeSO NodeSO)
        {
            OwnerNodeSO = NodeSO;
        }
        public virtual void AfterInit() { }
    }

}