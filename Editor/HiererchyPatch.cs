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
    public class HiererchyPatch
    {
        private static bool m_Init = false;
        private static IConfig m_Config = null;

        [InitializeOnLoadMethod]
        private static void Init()
        {
            if (m_Init == false)
            {
                EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemOnGUI;
                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
                EditorApplication.update = OnUpdate;
                EditorApplication.update += OnUpdate;
                //EditorApplication.searchChanged -= OnSearchChanged;
                //EditorApplication.searchChanged += OnSearchChanged;

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
                m_Init = true;
                WrapOnGUI();
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
                availableRect = selectionRect.MoveX(Const.ICON_SIZE + nameSize.x + m_Config.GameObjectGUILeftOffset)
                                            .AddWidth(-(Const.ICON_SIZE + nameSize.x + m_Config.GameObjectGUILeftOffset + m_Config.GameObjectGUIRightOffset));
                foreach (var module in m_Config.Modules)
                {
                    //if (module.Enabled)
                    {
                        availableRect = module.OnItemGUI(go, selectionRect, availableRect);
                    }
                }
            }
            else
            {
                for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                {
                    var scene = EditorSceneManager.GetSceneAt(i);
                    Debug.Log(scene.name);
                    if (scene.GetHashCode() == instanceID)
                    {
                        foreach (var module in m_Config.Modules)
                        {
                            //if (module.Enabled)
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

        }

        private static void WrapOnGUI()
        {
            var sceneHierarchyWindow = ReflectUtils.SceneHierarchyWindow_lastInteractedHierarchyWindow.GetValue(null);
            var hostView = ReflectUtils.EditorWindow_m_Parent.GetValue(sceneHierarchyWindow);
            var del = ReflectUtils.HostView_m_OnGUI.GetValue(hostView) as Delegate;
            Debug.Log(del.Method);
        }

        private static void OnGUI(EditorWindow window)
        {

        }

    }

}
