using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Doryu.Dialogue.Editor
{
    public class TextEffectCreateWindowEditor : EditorWindow
    {
        private static List<Type> classTypeList;
        private static TextEffectCreateWindowEditor window;
        private TextEffectSO _textEffectSO;
        public static event Action<TextEffectSO> OnCreateCompleteEvent;
        
        
        public static void ShowWindow()
        {
            window = CreateInstance<TextEffectCreateWindowEditor>();
            
            Vector2 windowSize = new Vector2(300, 300);
            Vector2 windowPosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            window.position = new Rect(windowPosition, windowSize);
            window.ShowPopup();
            
            // 찾고자 하는 부모 클래스 타입 지정 (예: ScriptableObject)
            Type baseType = typeof(TextEffectSO);  // 검색할 부모 클래스를 여기서 설정합니다.

            // 어셈블리에서 모든 타입 가져오기
            classTypeList = new List<Type>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(baseType) && !type.IsAbstract)
                    {
                        classTypeList.Add(type);
                    }
                }
            }
            OnCreateCompleteEvent = null;
        }

        private void OnGUI()
        {
            GUILayout.Space(25);
            AssetDatabase.Refresh();

            if (classTypeList == null)
            {
                window?.Close();
                return;
            }
            
            for (int i = 0; i < classTypeList.Count; i++)
            {
                if (GUILayout.Button($"{classTypeList[i].Name}"))
                {
                    TextEffectSO soData = ScriptableObject.CreateInstance(classTypeList[i]) as TextEffectSO;
                    _textEffectSO = CreateAsset(soData);
                    OnCreateCompleteEvent?.Invoke(_textEffectSO);
                    OnCreateCompleteEvent = null;
                    window.Close();
                }
            } 
            
            GUILayout.Space(25);
            GUI.color = Color.red;
            {
                if(GUILayout.Button("Close"))
                    Close();
            }
            GUI.color  = Color.white;        }

        private TextEffectSO CreateAsset(TextEffectSO asset)
        {
            string fileName = $"{asset.GetType().Name}";
            var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath($"Assets/{fileName}.asset");
            AssetDatabase.CreateAsset(asset, uniqueFileName);
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return asset;
        }
    }
}