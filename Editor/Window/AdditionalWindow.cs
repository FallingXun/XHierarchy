using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public class AdditionalWindow : EditorWindow
    {
        private IConfig m_Config;
        private GameObject m_Target;
        private string m_Note;
        private int m_Identifier;
        private Vector2 m_ScrollPosition = Vector2.zero;

        public static void Create(Vector2 position, IConfig config, GameObject go)
        {
            var window = GetWindow<AdditionalWindow>();
            window.position = Rect.zero.SetPosition(position).SetWidth(350).SetHeight(300);
            window.titleContent = ContentUtils.AdditionalTitleContent;
            window.Init(config, go);
        }

        private void Init(IConfig config, GameObject go)
        {
            m_Config = config;
            m_Target = go;
            m_Note = config.GetNote(go);
            m_Identifier = config.GetIdentifier(go);
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(ContentUtils.ObjectContent, GUILayout.Width(60));
            EditorGUILayout.ObjectField(m_Target, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(ContentUtils.NoteContent, GUILayout.Width(60));
            m_Note = EditorGUILayout.TextField(m_Note);
            if (GUILayout.Button(ContentUtils.ApplyBtnContent))
            {
                m_Config.SetNote(m_Target, m_Note);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(ContentUtils.IdentifierContent, GUILayout.Width(60));
            m_Identifier = EditorGUILayout.IntField(m_Identifier);
            if (GUILayout.Button(ContentUtils.ApplyBtnContent))
            {
                m_Config.SetIdentifier(m_Target, m_Identifier);
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

