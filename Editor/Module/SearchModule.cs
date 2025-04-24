using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;


namespace XHierarchy
{
    public class SearchModule : IModule
    {
        private IConfig m_Config = null;

        public string Name
        {
            get
            {
                return Const.KEY_SEARCH;
            }
        }

        public bool Enabled
        {
            get
            {
                return Utils.GetModuleEnabled(Name);
            }
            set
            {
                Utils.SetModuleEnabled(Name, value);
            }
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }

        public void Init(IConfig config)
        {
            m_Config = config;
        }

        public void OnGUIBegin(EditorWindow window)
        {

        }

        public void OnGUIEnd(EditorWindow window)
        {

            var rect = window.position.SetPosition(40, 0).SetWidth(20).SetHeight(20);
            //GUILayout.BeginHorizontal(EditorStyles.toolbar);

            if (GUI.Button(rect, ContentUtils.OpenSearchWindowContent))
            {
                var position = EditorGUIUtility.GUIToScreenPoint(new Vector2(window.position.SetPosition(0, 0).xMax, window.position.SetPosition(0, 0).yMin));
                SearchWindow.Create(position, m_Config);
            }

            //GUILayout.EndHorizontal();
        }

        public Rect OnItemGUI(GameObject go, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }

        public Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }
    }

}
