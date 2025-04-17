using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public class ComponentWindow : EditorWindow
    {
        private static ComponentWindow m_InstanceWindow = null;

        private Component m_Component = null;
        private Texture m_Icon = null;
        private Editor m_Editor = null;
        private bool m_IsLocked = false;


        public static void Create(Vector2 position, Component component, Texture icon)
        {
            if (m_InstanceWindow == null)
            {
                m_InstanceWindow = CreateInstance<ComponentWindow>();
            }
            m_InstanceWindow.position = Rect.zero.SetPosition(position).SetWidth(300).SetHeight(200);
            m_InstanceWindow.ShowPopup();
            m_InstanceWindow.Init(component, icon);
        }

        private void Init(Component component, Texture icon)
        {
            if (m_Editor != null)
            {
                DestroyImmediate(m_Editor);
                m_Editor = null;
            }
            m_Component = component;
            m_Icon = icon;
            m_Editor = Editor.CreateEditor(component);
        }

        private void OnGUI()
        {
            if (m_Component == null)
            {
                Close();
                return;
            }
            GUILayout.Label("", GUILayout.Height(20), GUILayout.ExpandWidth(true));
            var rect = GUILayoutUtility.GetLastRect();
            var closeBtnRect = rect.SetWidthFromRight(18).MoveY(1).SetHeight(18);
            var lockBtnRect = closeBtnRect.MoveX(-18);
            rect.DrawBackground(new Color(0.2f, 0.2f, 0.2f, 1f));
            rect = rect.SetWidth(20);
            rect.DrawIcon(m_Icon);
            if (m_Component is Behaviour behaviour)
            {
                rect = rect.MoveX(20);
                var enabled = behaviour.enabled;
                if (GUI.Toggle(rect, enabled, "") != enabled)
                {
                    Undo.RecordObject(m_Component, "");
                    behaviour.enabled = !enabled;
                }
            }
            rect = rect.SetWidth(lockBtnRect.x - rect.x).MoveX(20);
            var name = EditorGUIUtility.ObjectContent(m_Component, m_Component.GetType()).text;
            GUI.Label(rect, name);

            //if (lockBtnRect.IsHovered())
            //{
            //    lockBtnRect.DrawBackground(Color.gray);
            //    lockBtnRect.MarkHotRegion();
            //}
            //GUI.Label(lockBtnRect, m_IsLocked ? EditorGUIUtility.IconContent("LockIcon-On") : EditorGUIUtility.IconContent("LockIcon"));
            GUI.Toggle(lockBtnRect, m_IsLocked, GUIContent.none,new GUIStyle("IN LockButton"));

            if (closeBtnRect.IsHovered())
            {
                closeBtnRect.DrawBackground(Color.gray);
                closeBtnRect.MarkHotRegion();
            }
            GUI.Label(closeBtnRect, EditorGUIUtility.IconContent("CrossIcon"));



            m_Editor?.OnInspectorGUI();
        }

        private void OnDestroy()
        {
            if (m_Editor != null)
            {
                DestroyImmediate(m_Editor);
                m_Editor = null;
            }
            if (this == m_InstanceWindow)
            {
                m_InstanceWindow = null;
            }

        }

        private void OnLostFocus()
        {
            if (m_IsLocked)
            {
                return;
            }
            Close();
        }
    }

}
