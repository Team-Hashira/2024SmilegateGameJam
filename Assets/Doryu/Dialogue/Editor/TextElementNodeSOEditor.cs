using System;
using UnityEngine;

namespace Doryu.Dialogue.Editor
{
    using UnityEditor;
    [CustomEditor(typeof(TextElementNodeSO))]
    public class TextElementNodeSOEditor : Editor
    {
        private TextElementNodeSO _textElementNodeSO;
        private SerializedObject _serializedObj;
        private static int _currentSelectedIndex;
        private static TextEffectSO _currentSelectedObject;

        private void OnEnable()
        {
            _textElementNodeSO = target as TextElementNodeSO;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Child Node SO");            
                _textElementNodeSO.childNodeSO = EditorGUILayout.ObjectField(_textElementNodeSO.childNodeSO, typeof(NodeSO)) as NodeSO;
            }
            GUILayout.EndHorizontal();
            
            GUILayout.BeginVertical();
            {
                SerializedProperty textProperty = serializedObject.FindProperty("_text");
                EditorGUILayout.PropertyField(textProperty, new GUIContent("Text"));
                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndVertical();
            
            GUILayout.BeginVertical();
            {
                SerializedProperty textProperty = serializedObject.FindProperty("_writeDelay");
                EditorGUILayout.PropertyField(textProperty, new GUIContent("Write Delay"));
                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndVertical();
            
            GUILayout.BeginVertical();
            {
                SerializedProperty textProperty = serializedObject.FindProperty("_fontScale");
                EditorGUILayout.PropertyField(textProperty, new GUIContent("Font Scale"));
                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndVertical();
            
            //여기서부턴 리스트
            GUILayout.Space(25);
            
            for (int i = 0; i < _textElementNodeSO.effectSOList.Count; ++i)
            {
                for (int j = i+1; j < _textElementNodeSO.effectSOList.Count; ++j)
                {
                    if (_textElementNodeSO.effectSOList[j] == null) continue; 
                    if (_textElementNodeSO.effectSOList[i] == _textElementNodeSO.effectSOList[j])
                        Debug.LogWarning("같은 Effect SO가 포함되어 있습니다. List를 확인해주세요.");
                }

                GUILayout.BeginHorizontal();

                if (_currentSelectedIndex == i)
                {
                    GUI.color = Color.green;
                    _currentSelectedObject = _textElementNodeSO.effectSOList[i];
                }
                if(GUILayout.Button("Select"))
                {
                    SelectObject(i);
                }

                _textElementNodeSO.effectSOList[i] = EditorGUILayout.ObjectField(_textElementNodeSO.effectSOList[i], typeof(TextEffectSO), false) as TextEffectSO;

                GUI.color = Color.white;

                GUILayout.EndHorizontal();
            }
            
            if (GUILayout.Button("+"))
            {
                TextEffectCreateWindowEditor.ShowWindow();
                TextEffectCreateWindowEditor.OnCreateCompleteEvent += x =>
                {
                    _textElementNodeSO.effectSOList.Add(x);
                    SelectObject(_textElementNodeSO.effectSOList.Count - 1);
                };
            }
            if (GUILayout.Button("-"))
            {
                _textElementNodeSO.effectSOList.Remove(_currentSelectedObject);
                SelectObject(_textElementNodeSO.effectSOList.Count - 1);
            }
            GUILayout.Space(20);
            
            for (int i = 0; i < _textElementNodeSO.effectSOList.Count; i++)
            {
                if (_textElementNodeSO.effectSOList[i] == _currentSelectedObject)
                {
                    if (_textElementNodeSO.effectSOList[i] == null) return;
                    _serializedObj = new SerializedObject(_textElementNodeSO.effectSOList[i]);
                    DrawActiveSOData(_textElementNodeSO.effectSOList[i]);
                }
            }
        }

        private void SelectObject(int index)
        {
            try
            {
                _currentSelectedObject = _textElementNodeSO.effectSOList[index];
                _currentSelectedIndex = index;
            }
            catch (System.Exception) { }
        }

        private void DrawActiveSOData(TextEffectSO textEffectSO)
        {
            _serializedObj.Update();

            // 모든 필드를 기본 Inspector 스타일로 표시
            SerializedProperty property = _serializedObj.GetIterator();
            property.NextVisible(true);
            while (property.NextVisible(false))
            {
                EditorGUILayout.PropertyField(property, true);
            }

            _serializedObj.ApplyModifiedProperties();
        }
    }
}