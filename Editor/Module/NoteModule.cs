using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

namespace XHierarchy
{
    public class NoteModule : IModule
    {
        private IConfig m_Config;

        public string Name
        {
            get
            {
                return Const.KEY_NOTE;
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
                return 2;
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
            var note = m_Config.GetNote(go);
            if (string.IsNullOrWhiteSpace(note) == false)
            {
                var rect = availableRect.SetMinWidth(GUI.skin.button.CalcSize(new GUIContent(note)).x);
                if (GUI.Button(rect, note, StyleUtils.NoteButon))
                {
                    var position = EditorGUIUtility.GUIToScreenPoint(new Vector2(rect.xMax, rect.y));
                    AdditionalWindow.Create(position, m_Config, go);
                }
                availableRect = availableRect.MoveX(rect.width).AddWidth(-rect.width);
            }
            return availableRect;
        }

        public Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }
    }
}

