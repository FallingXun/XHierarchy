using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Reflection;

namespace XHierarchy
{
    public class HierarchyWindow : Editor
    {
        private static Texture2D m_LineTexture = null;
        static Type t_SceneHierarchyWindow = typeof(Editor).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        static MethodInfo md;
        static object treeViewController;
        static int a = 0;
        static bool s = true;
        static HierarchyLinesModule module = new HierarchyLinesModule();
        [InitializeOnLoadMethod]
        private static void Init()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemGUI;
            EditorApplication.hierarchyWindowItemOnGUI = OnHierarchyWindowItemGUI + EditorApplication.hierarchyWindowItemOnGUI;
            EditorApplication.update -= CheckIfFocusedWindowChanged;
            EditorApplication.update += CheckIfFocusedWindowChanged;


            if (m_LineTexture == null)
            {
                m_LineTexture = new Texture2D(1, 1);
                m_LineTexture.SetPixels(new Color[] { Color.white });
            }

            module.Init();
        }

        static void CheckIfFocusedWindowChanged()
        {
            var window = EditorWindow.focusedWindow;
            if (EditorWindow.focusedWindow?.GetType() == t_SceneHierarchyWindow)
            {
                var sceneHierarchy = window.GetType().GetField("m_SceneHierarchy", (BindingFlags)62).GetValue(window);
                treeViewController = sceneHierarchy.GetType().GetField("m_TreeView", (BindingFlags)62).GetValue(sceneHierarchy);
                md = treeViewController.GetType().GetMethod("ChangeFoldingForSingleItem", (BindingFlags)62);
            }

        }



        private static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
        {
            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (go == null)
            {
                return;
            }
            //Debug.Log(go.name + "    " + go.GetInstanceID() + "    " + selectionRect);
            module.OnItemGUI(go, selectionRect);
            //var size = GUI.skin.label.CalcSize(new GUIContent(go.name));
            //if (GUI.Button(new Rect(selectionRect.xMin + size.x + 14, selectionRect.yMin, 16, 16), "T"))
            //{
            //    if (go.name == "Image")
            //    {
            //        md?.Invoke(treeViewController, new object[] { instanceID, s });
            //        s = !s;
            //    }
            //}



        }

        private static void DrawHierarchyLine(GameObject go)
        {
            var tf = go.transform;
        }
    }
}



