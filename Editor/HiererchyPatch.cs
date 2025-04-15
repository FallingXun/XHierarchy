using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace XHierarchy
{
    [InitializeOnLoad]
    public class HiererchyPatch
    {
        private static List<IModule> m_ModuleList = new List<IModule>
        {
            new HierarchyLinesModule(),
        };

        private static bool m_Init = false;

        public HiererchyPatch()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemGUI;
            EditorApplication.hierarchyWindowItemOnGUI = OnHierarchyWindowItemGUI + EditorApplication.hierarchyWindowItemOnGUI;
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
        }


        private static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
        {
            if (m_Init == false)
            {
                foreach (var module in m_ModuleList)
                {
                    module.Init();
                }
                m_Init = true;
            }

            foreach (var module in m_ModuleList)
            {
                if (module.Enabled)
                {
                    var obj = EditorUtility.InstanceIDToObject(instanceID);
                    if (obj is GameObject go)
                    {
                        module.OnItemGUI(go, selectionRect);
                    }
                    else
                    {
                        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                        {
                            var scene = EditorSceneManager.GetSceneAt(i);
                            if (scene.GetHashCode() == instanceID)
                            {
                                module.OnSceneGUI(scene, selectionRect);
                            }
                        }
                    }
                }
            }

        }

        private static void OnUpdate()
        {

        }
    }

}
