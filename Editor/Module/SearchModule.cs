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
            // 先绘制点击按钮区域，拦截点击事件
            var rect = GetSearchButtonRect(window);
            if (GUI.Button(rect, GUIContent.none))
            {
                var position = EditorGUIUtility.GUIToScreenPoint(new Vector2(window.position.SetPosition(0, 0).xMax, window.position.SetPosition(0, 0).yMin));
                SearchWindow.Create(position, m_Config);
                Event.current.Use();
            }
        }

        public void OnGUIEnd(EditorWindow window)
        {
            // 仅绘制按钮表现，不做事件处理
            var rect = GetSearchButtonRect(window);
            if (GUI.Button(rect, ContentUtils.SearchContent, StyleUtils.IconButton))
            {
                
            }
        }

        public Rect OnItemGUI(GameObject go, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }

        public Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }

        private Rect GetSearchButtonRect(EditorWindow window)
        {
            var rect = window.position.SetPosition(window.position.width - 20, 3).SetWidth(14).SetHeight(14);
            return rect;
        }
    }

}
