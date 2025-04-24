using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Reflection;

namespace XHierarchy
{
    [InitializeOnLoad]
    public class HierarchyPatch
    {
        private static bool m_Init = false;
        private static IConfig m_Config = null;
        private static EditorWindow m_SceneHierarchyWindow = null;
        private static Action<EditorWindow> m_GUIBegin = null;
        private static Action<EditorWindow> m_GUIEnd = null;

        [InitializeOnLoadMethod]
        private static void Init()
        {
            if (m_Init == false)
            {
                EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemOnGUI;
                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
                EditorApplication.update = OnUpdate;
                EditorApplication.update += OnUpdate;
                m_GUIBegin = OnGUIBegin;
                m_GUIEnd = OnGUIEnd;

                var hierarchyData = AssetDatabase.LoadAssetAtPath<HierarchyData>(Const.HIERARCHY_ASSET_PATH);
                if (hierarchyData == null)
                {
                    throw new Exception("Hierarchy Data Asset is missing, please use 'XHierarchy/Data/Create Hierarchy Data Asset' to generate!");
                }
                m_Config = hierarchyData.Config;
                if (m_Config == null)
                {
                    throw new Exception("Hierarchy Config is missing, please delete the 'HierarchyData.asset' and use 'XHierarchy/Data/Create Hierarchy Data Asset' to regenerate!");
                }
                m_Config.Init();

                foreach (var module in m_Config.Modules)
                {
                    module.Init(m_Config);
                }
                InitSceneHierarchyWindow();

                m_Init = true; 
            }
        }

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            if (m_Init == false)
            {
                return;
            }
            var availableRect = selectionRect;
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (obj is GameObject go)
            {
                var nameSize = GUI.skin.label.CalcSize(new GUIContent(go.name));
                var availableWidth = availableRect.width - Const.ICON_SIZE - nameSize.x;
                var range = m_Config.Handler.GetItemGUIRange(go);
                var offsetFromLeft = range.x >= 0 ? range.x : (availableWidth - range.x).Max(0);
                var offsetFromRight = range.y >= 0 ? range.y : (availableWidth - range.y).Max(0);
                availableRect = availableRect.SetWidthFromRight(availableWidth).MoveX(offsetFromLeft)
                                            .AddWidth(-(offsetFromLeft + offsetFromRight));
                foreach (var module in m_Config.Modules)
                {
                    if (module.Enabled)
                    {
                        availableRect = module.OnItemGUI(go, selectionRect, availableRect);
                    }
                }
            }
            else
            {
                if (obj != null)
                {
                    Debug.Log(obj.name + "  " + obj.GetType() + "   " + selectionRect);
                }
                for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                {
                    var scene = EditorSceneManager.GetSceneAt(i);
                    if (scene.GetHashCode() == instanceID)
                    {
                        foreach (var module in m_Config.Modules)
                        {
                            if (module.Enabled)
                            {
                                availableRect = module.OnSceneGUI(scene, selectionRect, availableRect);
                            }
                        }
                    }
                }
            }
        }


        private static void OnUpdate()
        {
            if (m_Init == false)
            {
                return;
            }
            WrapOnGUI(true);
        }

        private static void InitSceneHierarchyWindow()
        {
            if (m_SceneHierarchyWindow == null)
            {
                var windows = Resources.FindObjectsOfTypeAll(ReflectUtils.SceneHierarchyWindow);
                if (windows != null && windows.Length > 0)
                {
                    m_SceneHierarchyWindow = windows[0] as EditorWindow;
                }
                WrapOnGUI(true);
            }
        }

        private static void WrapOnGUI(bool wrap)
        {
            if (m_SceneHierarchyWindow == null)
            {
                return;
            }
            var hostView = ReflectUtils.EditorWindow_m_Parent.GetValue(m_SceneHierarchyWindow);
            if (hostView == null)
            {
                return;
            }
            if (ReflectUtils.HostView_m_OnGUI == null)
            {
                return;
            }
            var del = ReflectUtils.HostView_m_OnGUI.GetValue(hostView) as Delegate;
            if (del == null)
            {
                return;
            }

            if (ReflectUtils.HostView_EditorWindowDelegate == null)
            {
                return;
            }
            if (wrap)
            {
                if (ReflectUtils.HiererchyPatch_OnGUI == null)
                {
                    return;
                }
                if (del.Method == ReflectUtils.HiererchyPatch_OnGUI)
                {
                    return;
                }
                var wrapDel = Delegate.CreateDelegate(ReflectUtils.HostView_EditorWindowDelegate, m_SceneHierarchyWindow, ReflectUtils.HiererchyPatch_OnGUI);
                ReflectUtils.HostView_m_OnGUI.SetValue(hostView, wrapDel);
            }
            else
            {
                if (ReflectUtils.SceneHierarchyWindow_OnGUI == null)
                {
                    return;
                }
                if (del.Method == ReflectUtils.SceneHierarchyWindow_OnGUI)
                {
                    return;
                }
                if (ReflectUtils.HostView_CreateDelegate == null)
                {
                    return;
                }
                var baseDel = ReflectUtils.HostView_CreateDelegate.Invoke(hostView, new object[] { "OnGUI" });
                ReflectUtils.HostView_m_OnGUI.SetValue(hostView, baseDel);
            }

            m_SceneHierarchyWindow.Repaint();
        }


        private static void OnGUI(EditorWindow window)
        {
            m_GUIBegin?.Invoke(window);
            ReflectUtils.SceneHierarchyWindow_OnGUI.Invoke(window, null);
            m_GUIEnd?.Invoke(window);
        }

        private static void OnGUIBegin(EditorWindow window)
        {
            foreach (var module in m_Config.Modules)
            {
                if (module.Enabled)
                {
                    module.OnGUIBegin(window);
                }
            }
        }

        private static void OnGUIEnd(EditorWindow window)
        {
            foreach (var module in m_Config.Modules)
            {
                if (module.Enabled)
                {
                    module.OnGUIEnd(window);
                }
            }
        }
    }

}
