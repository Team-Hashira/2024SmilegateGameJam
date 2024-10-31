using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doryu.Dialogue
{
    public class NodeSO : ScriptableObject
    {
        protected DialogueSystemController _controller;

        private Coroutine _runCoroutine;

        protected virtual void Start() { }
        protected virtual void End() { }
        protected virtual void Update() { }

        public void Enter()
        {
            _runCoroutine = _controller.StartCoroutine(RunCoroutine());
            Start();
        }
        public void Exit()
        {
            End();
            _controller.StopCoroutine(_runCoroutine);
        }

        public virtual void SetController(DialogueSystemController controller)
        {
            _controller = controller;
        }

        private IEnumerator RunCoroutine()
        {
            while (true)
            {
                yield return null;
                Update();
            }
        }

        public void TravelAcion(Action<NodeSO> action)
        {
            action?.Invoke(this);

            List<NodeSO> children = new List<NodeSO>();

            if (this is SingleNodeSO singleNodeSO &&
                singleNodeSO.childNodeSO != null)
            {
                children.Add(singleNodeSO.childNodeSO);
            }
            else if (this is BifurcationNodeSO bifurcationNodeSO)
            {
                children = bifurcationNodeSO.childrenNodeSOs;
            }

            children.ForEach(child => child.TravelAcion(action));
        }
    }

}