using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public class AdditionalWindow : EditorWindow
    {
        private static readonly GUIContent m_TargetContent = new GUIContent("对象");
        private static readonly GUIContent m_NoteContent = new GUIContent("注释");
        private static readonly GUIContent m_IdentifierContent = new GUIContent("标识号");
        private static readonly GUIContent m_ApplyBtnContent = new GUIContent("更新");

        private IConfig m_Config;
        private GameObject m_Target;
        private string m_Note;
        private int m_Identifier;
        private Vector2 m_ScrollPosition = Vector2.zero;

        public static void Create(Vector2 position, IConfig config, GameObject go)
        {
            var window = GetWindow<AdditionalWindow>();
            window.position = Rect.zero.SetPosition(position).SetWidth(350).SetHeight(300);
            window.Init(config, go);
        }

        private void Init(IConfig config, GameObject go)
        {
            m_Config = config;
            m_Target = go;
        }

        private void OnGUI()
        {
            EditorGUILayout.ObjectField(m_TargetContent, m_Target, typeof(GameObject), false);

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            EditorGUILayout.BeginHorizontal();

            m_Note = EditorGUILayout.TextField(m_NoteContent, m_Note);
            if (GUILayout.Button(m_ApplyBtnContent))
            {
                m_Config.NoteApplyAction?.Invoke(m_Target, m_Note);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            m_Identifier = EditorGUILayout.IntField(m_IdentifierContent, m_Identifier);
            if (GUILayout.Button(m_ApplyBtnContent))
            {
                m_Config.IdentifierApplyAction?.Invoke(m_Target, m_Identifier);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }

    }
}

