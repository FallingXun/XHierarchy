using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;

namespace XHierarchy
{
    public class ReflectUtils
    {
        private static BindingFlags m_BindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

        #region UnityEditor.SceneHierarchyWindow

        private static Type m_SceneHierarchyWindow = null;
        public static Type SceneHierarchyWindow
        {
            get
            {
                if (m_SceneHierarchyWindow == null)
                {
                    m_SceneHierarchyWindow = typeof(Editor).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
                }
                return m_SceneHierarchyWindow;
            }
        }

        private static PropertyInfo m_SceneHierarchyWindow_lastInteractedHierarchyWindow = null;
        public static PropertyInfo SceneHierarchyWindow_lastInteractedHierarchyWindow
        {
            get
            {
                if(m_SceneHierarchyWindow_lastInteractedHierarchyWindow == null)
                {
                    m_SceneHierarchyWindow_lastInteractedHierarchyWindow = SceneHierarchyWindow?.GetProperty("lastInteractedHierarchyWindow", m_BindingFlags);
                }
                return m_SceneHierarchyWindow_lastInteractedHierarchyWindow;
            }
        }

        private static MethodInfo m_SceneHierarchyWindow_OnGUI = null;
        public static MethodInfo SceneHierarchyWindow_OnGUI
        {
            get
            {
                if (m_SceneHierarchyWindow_OnGUI == null)
                {
                    m_SceneHierarchyWindow_OnGUI = SceneHierarchyWindow?.GetMethod("OnGUI", m_BindingFlags);
                }
                return m_SceneHierarchyWindow_OnGUI;
            }
        }

        private static MethodInfo m_SceneHierarchyWindow_GetAllSceneHierarchyWindows = null;
        public static MethodInfo SceneHierarchyWindow_GetAllSceneHierarchyWindows
        {
            get
            {
                if (m_SceneHierarchyWindow_GetAllSceneHierarchyWindows == null)
                {
                    m_SceneHierarchyWindow_GetAllSceneHierarchyWindows = SceneHierarchyWindow?.GetMethod("GetAllSceneHierarchyWindows", m_BindingFlags);
                }
                return m_SceneHierarchyWindow_GetAllSceneHierarchyWindows;
            }
        }

        private static FieldInfo m_EditorWindow_m_Parent = null;
        public static FieldInfo EditorWindow_m_Parent
        {
            get
            {
                if(m_EditorWindow_m_Parent == null)
                {
                    m_EditorWindow_m_Parent = typeof(EditorWindow).GetField("m_Parent", m_BindingFlags);
                }
                return m_EditorWindow_m_Parent;
            }
        }

        #endregion

        #region UnityEditor.HostView

        private static FieldInfo m_HostView_m_OnGUI = null;
        public static FieldInfo HostView_m_OnGUI
        {
            get
            {
                if (m_HostView_m_OnGUI == null)
                {
                    m_HostView_m_OnGUI = EditorWindow_m_Parent?.FieldType.GetField("m_OnGUI", m_BindingFlags);
                }
                return m_HostView_m_OnGUI;
            }
        }

        private static Type m_HostView_EditorWindowDelegate = null;
        public static Type HostView_EditorWindowDelegate
        {
            get
            {
                if (m_HostView_EditorWindowDelegate == null)
                {
                    m_HostView_EditorWindowDelegate = EditorWindow_m_Parent?.FieldType.GetNestedType("EditorWindowDelegate", m_BindingFlags);
                }
                return m_HostView_EditorWindowDelegate;
            }
        }

        private static MethodInfo m_HostView_CreateDelegate = null;
        public static MethodInfo HostView_CreateDelegate
        {
            get
            {
                if (m_HostView_CreateDelegate == null)
                {
                    m_HostView_CreateDelegate = EditorWindow_m_Parent?.FieldType.GetMethod("CreateDelegate", m_BindingFlags);
                }
                return m_HostView_CreateDelegate;
            }
        }

        #endregion


        #region UnityEngine.GUIClip

        private static Type m_GUIClip = null;
        public static Type GUIClip
        {
            get
            {
                if (m_GUIClip == null)
                {
                    m_GUIClip = typeof(GUI).Assembly.GetType("UnityEngine.GUIClip");
                }
                return m_GUIClip;
            }
        }

        private static MethodInfo m_GUIClip_UnclipToWindow = null;
        public static MethodInfo GUIClip_UnclipToWindow
        {
            get
            {
                if (m_GUIClip_UnclipToWindow == null)
                {
                    m_GUIClip_UnclipToWindow = GUIClip?.GetMethod("UnclipToWindow", m_BindingFlags, null, new[] { typeof(Rect) }, null);
                }
                return m_GUIClip_UnclipToWindow;
            }
        }

        #endregion


        #region UnityEditor.GUIView

        private static Type m_GUIView = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");
        public static Type GUIView
        {
            get
            {
                if (m_GUIView == null)
                {
                    m_GUIView = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");
                }
                return m_GUIView;
            }
        }

        private static PropertyInfo m_GUIView_current = null;
        public static PropertyInfo GUIView_current
        {
            get
            {
                if (m_GUIView_current == null)
                {
                    m_GUIView_current = GUIView?.GetProperty("current", m_BindingFlags);
                }
                return m_GUIView_current;
            }

        }

        private static MethodInfo m_GUIView_MarkHotRegion = null;
        public static MethodInfo GUIView_MarkHotRegion
        {
            get
            {
                if (m_GUIView_MarkHotRegion == null)
                {
                    m_GUIView_MarkHotRegion = GUIView?.GetMethod("MarkHotRegion", m_BindingFlags);
                }
                return m_GUIView_MarkHotRegion;
            }
        }
        #endregion

        #region XHierarchy
        private static MethodInfo m_HiererchyPatch_OnGUI = null;
        public static MethodInfo HiererchyPatch_OnGUI
        {
            get
            {
                if (m_HiererchyPatch_OnGUI == null)
                {
                    m_HiererchyPatch_OnGUI = typeof(HierarchyPatch)?.GetMethod("OnGUI", m_BindingFlags);
                }
                return m_HiererchyPatch_OnGUI;
            }
        }

        #endregion

    }

}

