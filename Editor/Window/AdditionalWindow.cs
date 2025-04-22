using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public class AdditionalWindow : EditorWindow
    {
        private static readonly GUIContent m_TitleContent = ContentUtils.AdditionalTitleContent;
        private static readonly GUIContent m_TargetContent = ContentUtils.ObjectContent;
        private static readonly GUIContent m_NoteContent = ContentUtils.NoteContent;
        private static readonly GUIContent m_IdentifierContent = ContentUtils.IdentifierContent;
        private static readonly GUIContent m_ApplyBtnContent = ContentUtils.ApplyBtnContent;

        private IConfig m_Config;
        private GameObject m_Target;
        private string m_Note;
        private int m_Identifier;
        private Vector2 m_ScrollPosition = Vector2.zero;

        public static void Create(Vector2 position, IConfig config, GameObject go)
        {
            var window = GetWindow<AdditionalWindow>();
            window.position = Rect.zero.SetPosition(position).SetWidth(350).SetHeight(300);
            window.titleContent = m_TitleContent;
            window.Init(config, go);
        }

        private void Init(IConfig config, GameObject go)
        {
            m_Config = config;
            m_Target = go;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(m_TargetContent, GUILayout.Width(60));
            EditorGUILayout.ObjectField(m_Target, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(m_NoteContent, GUILayout.Width(60));
            m_Note = EditorGUILayout.TextField(m_Note);
            if (GUILayout.Button(m_ApplyBtnContent))
            {
                m_Config.NoteApplyAction?.Invoke(m_Target, m_Note);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(m_IdentifierContent, GUILayout.Width(60));
            m_Identifier = EditorGUILayout.IntField(m_Identifier);
            if (GUILayout.Button(m_ApplyBtnContent))
            {
                m_Config.IdentifierApplyAction?.Invoke(m_Target, m_Identifier);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }

        private void OnLostFocus()
        {
            EditorApplication.delayCall -= DelayClose;
            EditorApplication.delayCall += DelayClose;
        }

        private void DelayClose()
        {
            if(focusedWindow != this)
            {
                Close();
            }
        }
    }
}

