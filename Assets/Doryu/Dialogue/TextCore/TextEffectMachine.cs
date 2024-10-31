using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Doryu.Dialogue
{
    public struct TextMoveData
    {
        public int index;
        public float power;
        public float speed;
        public float lastMoveTime;
    }
    public enum ETextEffectType
    {
        Shake,
        Visible,
        Wave,
    }

    public class TextEffectMachine
    {
        public string Text { get; private set; }

        private Dictionary<Type, TextEffector> _effectorDict;

        public bool IsUIText { get; private set; }

        #region TMP
        private Mesh _mesh;
        private Vector3[] _startVertices;
        private Vector3[] _vertices;
        private Color[] _startColors;
        private Color[] _colors;
        private TMP_Text _textMesh;
        #endregion

        public T GetEffector<T>() where T : TextEffector
        {
            return _effectorDict[typeof(T)] as T;
        }

        public TextEffectMachine(TMP_Text tmp)
        {
            _textMesh = tmp;
            IsUIText = tmp.canvasRenderer != null;

            _effectorDict = new Dictionary<Type, TextEffector>();
            foreach (ETextEffectType effectType in Enum.GetValues(typeof(ETextEffectType)))
            {
                Debug.Log($"Text{effectType}Effector");
                Type type = Type.GetType($"Text{effectType}Effector");
                TextEffector textEffector = Activator.CreateInstance(type) as TextEffector;
                _effectorDict.Add(type, textEffector);
            }
        }

        public void SetText(string text)
        {
            Text = text;
        }

        public void Init(string tagText)
        {
            _textMesh.text = tagText;
            _textMesh.ForceMeshUpdate();

            if (IsUIText)
                _mesh = _textMesh.mesh;
            else
                _mesh = _textMesh.textInfo.meshInfo[0].mesh;

            _startVertices = _textMesh.mesh.vertices;
            _startColors = _textMesh.mesh.colors;

            _vertices = _mesh.vertices;
            _colors = _mesh.colors;

            foreach (TextEffector effector in _effectorDict.Values)
            {
                effector.Init(_startVertices, _startColors, ref _vertices, ref _colors);
            }

            _mesh.vertices = _vertices;
            _mesh.colors = _colors;

            if (IsUIText)
                _textMesh.canvasRenderer.SetMesh(_mesh);
            else
                _textMesh.UpdateGeometry(_mesh, 0);
        }

        public void TextUpdate()
        {
            if (IsUIText)
                _mesh = _textMesh.mesh;
            else
                _mesh = _textMesh.textInfo.meshInfo[0].mesh;

            _vertices = _mesh.vertices;
            _colors = _mesh.colors;

            Vector3[] verticesMovement = new Vector3[_vertices.Length];
            foreach (TextEffector effector in _effectorDict.Values)
            {
                effector.Update(_startVertices, _startColors, ref verticesMovement, ref _colors);
            }
            for (int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] = _startVertices[i] + verticesMovement[i];
            }

            _mesh.vertices = _vertices;
            _mesh.colors = _colors;

            if (IsUIText)
                _textMesh.canvasRenderer.SetMesh(_mesh);
            else
                _textMesh.UpdateGeometry(_mesh, 0);
        }
    }

}