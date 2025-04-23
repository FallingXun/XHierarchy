using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public class StyleUtils
    {
        private static GUIStyle m_BtnClose;
        public static GUIStyle BtnClose
        {
            get
            {
                if (m_BtnClose == null)
                {
                    m_BtnClose = Application.platform == RuntimePlatform.OSXEditor ? "WinBtnCloseMac" : "WinBtnClose";
                }
                return m_BtnClose;
            }
        }

        private static GUIStyle m_ToggleLock;
        public static GUIStyle ToggleLock
        {
            get
            {
                if (m_ToggleLock == null)
                {
                    m_ToggleLock = "IN LockButton";
                }
                return m_ToggleLock;
            }
        }

        private static GUIStyle m_IconButton;
        public static GUIStyle IconButton
        {
            get
            {
                if (m_IconButton == null)
                {
                    m_IconButton = "IconButton";
                }
                return m_IconButton;
            }
        }

        private static GUIStyle m_WindowTitle;
        public static GUIStyle WindowTitle
        {
            get
            {
                if (m_WindowTitle == null)
                {
                    m_WindowTitle = "OL Title";
                }
                return m_WindowTitle;
            }
        }

        private static GUIStyle m_BoldLabel;
        public static GUIStyle BoldLabel
        {
            get
            {
                if (m_BoldLabel == null)
                {
                    m_BoldLabel = "BoldLabel";
                }
                return m_BoldLabel;
            }
        }

        private static GUIStyle m_NoteButon;
        public static GUIStyle NoteButon
        {
            get
            {
                if (m_NoteButon == null)
                {
                    m_NoteButon = new GUIStyle(GUI.skin.button);
                    m_NoteButon.normal.textColor = Color.yellow;
                }
                return m_NoteButon;
            }
        }


        private static GUIStyle m_IdentifierButon;
        public static GUIStyle IdentifierButon
        {
            get
            {
                if (m_IdentifierButon == null)
                {
                    m_IdentifierButon = new GUIStyle(GUI.skin.button);
                    m_IdentifierButon.normal.textColor = Color.green;
                }
                return m_IdentifierButon;
            }
        }

        private static GUIStyle m_ToolbarCreateAddNewDropDown;
        public static GUIStyle ToolbarCreateAddNewDropDown
        {
            get
            {
                if(m_ToolbarCreateAddNewDropDown == null)
                {
                    var styleName = "ToolbarCreateAddNewDropDown";
                    m_ToolbarCreateAddNewDropDown = GUI.skin.FindStyle(styleName) ?? EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle(styleName);
                }
                    return m_ToolbarCreateAddNewDropDown;
            }
        }
    }
}

