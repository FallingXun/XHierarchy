using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace XHierarchy
{
    public class ComponentWindow : EditorWindow
    {
        private static ComponentWindow m_InstanceWindow = null;
        private bool IsInstanceWindow
        {
            get
            {
                return m_InstanceWindow != null && m_InstanceWindow == this;
            }
        }

        private Component m_Component = null;
        private Texture m_Icon = null;
        private Editor m_Editor = null;
        private bool m_IsLocked = false;
        private bool m_IsHorizontalResizing = false;
        private bool m_IsVerticalResizing = false;
        private float m_HorizontalStartMouseX = 0;
        private float m_HorizontalStartWidth = 0;
        private float m_VerticalStartMouseY = 0;
        private float m_VerticalStartHeight = 0;
        private bool m_IsMoving = false;
        private Vector2 m_MoveStartWindowPostion = Vector2.zero;
        private Vector2 m_MoveStartMousePosition = Vector2.zero;
        private Vector2 m_ScrollPosition = Vector2.zero;

        public static void Create(Vector2 position, Component component, Texture icon)
        {
            if (m_InstanceWindow == null)
            {
                m_InstanceWindow = CreateInstance<ComponentWindow>();
            }
            m_InstanceWindow.position = Rect.zero.SetPosition(position).SetWidth(350).SetHeight(300);
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

            position.SetPosition(0, 0).DrawOutline(ColorUtils.WindowOutlineColor);
            //HandleResize();

            GUILayout.Label("", GUILayout.Height(20), GUILayout.ExpandWidth(true));
            var rect = GUILayoutUtility.GetLastRect();
            OnTitleGUI(rect);

            HandleDrag(rect);

            OnContentGUI();

            GUILayout.Space(5);
        }

        private void OnDestroy()
        {
            if (m_Editor != null)
            {
                DestroyImmediate(m_Editor);
                m_Editor = null;
            }
            if (IsInstanceWindow)
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

        private void SetLock(bool isLocked)
        {
            if (isLocked == m_IsLocked)
            {
                return;
            }
            if (isLocked)
            {
                if (IsInstanceWindow)
                {
                    m_InstanceWindow = null;
                }
            }
            else
            {
                if (m_InstanceWindow != null)
                {
                    m_InstanceWindow.Close();
                }
                m_InstanceWindow = this;
            }

            m_IsLocked = isLocked;
        }

        private void HandleResize()
        {
            if (m_IsMoving)
            {
                return;
            }
            var left = position.SetPosition(0, 0).SetWidth(4);
            var right = position.SetPosition(0, 0).SetWidthFromRight(4);
            var top = position.SetPosition(0, 0).SetHeight(4);
            var bottom = position.SetPosition(0, 0).SetHeightFromBottom(4);
            left = right;
            top = bottom;
            EditorGUIUtility.AddCursorRect(left, MouseCursor.ResizeHorizontal);
            EditorGUIUtility.AddCursorRect(right, MouseCursor.ResizeHorizontal);

            EditorGUIUtility.AddCursorRect(top, MouseCursor.ResizeVertical);
            EditorGUIUtility.AddCursorRect(bottom, MouseCursor.ResizeVertical);

            if (left.IsMouseDrag() || right.IsMouseDrag())
            {
                m_IsHorizontalResizing = true;
                m_HorizontalStartMouseX = EditorGUIUtility.GUIToScreenPoint(Event.current.mousePosition).x;
                m_HorizontalStartWidth = position.width;
            }
            if (top.IsMouseDrag() || bottom.IsMouseDrag())
            {
                m_IsVerticalResizing = true;
                m_VerticalStartMouseY = EditorGUIUtility.GUIToScreenPoint(Event.current.mousePosition).y;
                m_VerticalStartHeight = position.height;
            }
            if (m_IsHorizontalResizing || m_IsVerticalResizing)
            {
                var mousePosition = EditorGUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                if (Event.current.type == EventType.Repaint)
                {
                    var rect = position;
                    if (m_IsHorizontalResizing)
                    {
                        rect = rect.SetWidth(m_HorizontalStartWidth + mousePosition.x - m_HorizontalStartMouseX);
                    }
                    if (m_IsVerticalResizing)
                    {
                        rect = rect.SetHeight(m_VerticalStartHeight + mousePosition.y - m_VerticalStartMouseY);
                    }
                    position = rect;
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    m_IsHorizontalResizing = false;
                    m_IsVerticalResizing = false;
                }
            }

        }

        private void HandleDrag(Rect rect)
        {
            if (m_IsHorizontalResizing || m_IsVerticalResizing)
            {
                return;
            }
            if (m_IsMoving == false)
            {
                if (rect.IsMouseDrag())
                {
                    m_IsMoving = true;

                    m_MoveStartWindowPostion = position.position;
                    m_MoveStartMousePosition = EditorGUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                    SetLock(true);
                }
            }

            if (m_IsMoving)
            {
                var dragPosition = m_MoveStartWindowPostion + EditorGUIUtility.GUIToScreenPoint(Event.current.mousePosition) - m_MoveStartMousePosition;
                if (Event.current.type != EventType.Repaint)
                {
                    position = position.SetPosition(dragPosition);
                }

                if (Event.current.type == EventType.MouseUp)
                {
                    m_IsMoving = false;
                }
            }
        }

        private void OnTitleGUI(Rect rect)
        {
            var lastRect = rect.SetWidthFromRight(20);
            var closeBtnRect = lastRect;

            lastRect = lastRect.MoveX(-20);
            var lockBtnRect = lastRect;

            lastRect = lastRect.MoveX(-20);
            var gameObjectBtnRect = lastRect;

            lastRect = lastRect.MoveX(-20);
            var pasteBtnRect = lastRect;

            lastRect = lastRect.MoveX(-20);
            var copyBtnRect = lastRect;


            GUI.Box(rect, GUIContent.none, StyleUtils.WindowTitle);
            rect = rect.SetWidth(20);
            GUI.Label(rect, m_Icon);
            rect = rect.MoveX(22);
            if (m_Component is Behaviour behaviour)
            {
                var enabled = behaviour.enabled;
                if (GUI.Toggle(rect, enabled, "") != enabled)
                {
                    Undo.RecordObject(m_Component, "");
                    behaviour.enabled = !enabled;
                }
            }
            rect = rect.MoveX(18);
            rect = rect.SetWidth(lastRect.x - rect.x);
            var name = EditorGUIUtility.ObjectContent(m_Component, m_Component.GetType()).text;
            GUI.Label(rect, name, StyleUtils.BoldLabel);

            if (GUI.Button(gameObjectBtnRect, ContentUtils.SelectContent, StyleUtils.IconButton))
            {
                Selection.activeGameObject = m_Component.gameObject;
            }

            var isLocked = GUI.Toggle(lockBtnRect, m_IsLocked, GUIContent.none, StyleUtils.ToggleLock);
            SetLock(isLocked);

            if (GUI.Button(closeBtnRect, GUIContent.none, StyleUtils.BtnClose))
            {
                Close();
            }

            if (GUI.Button(copyBtnRect, new GUIContent("C"), StyleUtils.IconButton))
            {
                ComponentUtility.CopyComponent(m_Component);
            }

            if (GUI.Button(pasteBtnRect, new GUIContent("P"), StyleUtils.IconButton))
            {
                Undo.RecordObject(m_Component, "");
                ComponentUtility.PasteComponentValues(m_Component);
            }

        }

        private void OnContentGUI()
        {
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

            GUILayout.BeginHorizontal();
            GUILayout.Space(8);
            GUILayout.BeginVertical();

            m_Editor?.OnInspectorGUI();

            GUILayout.Label("", GUILayout.Height(-10), GUILayout.ExpandWidth(true));

            if (Event.current.type == EventType.Repaint && m_IsVerticalResizing == false)
            {
                position = position.SetHeight((GUILayoutUtility.GetLastRect().y + 30).Min(300));
            }
            GUILayout.EndVertical();
            GUILayout.Space(5);
            GUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();


        }

    }

}
