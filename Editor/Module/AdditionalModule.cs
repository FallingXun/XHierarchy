using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

namespace XHierarchy
{
    public class AdditionalModule : IModule
    {
        private IConfig m_Config;

        public string Name
        {
            get
            {
                return typeof(AdditionalModule).FullName;
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

        }

        public Rect OnItemGUI(GameObject go, Rect selectionRect, Rect availableRect)
        {
            var rect = selectionRect.SetWidth(Const.ICON_SIZE);
            if (GUI.Button(rect, "", GUIStyle.none))
            {
                var position = EditorGUIUtility.GUIToScreenPoint(new Vector2(rect.xMax, rect.y));
                AdditionalWindow.Create(position, m_Config, go);
            }
            return availableRect;
        }

        public Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }
    }
}

