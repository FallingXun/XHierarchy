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
                    m_IconButton.alignment = TextAnchor.MiddleCenter;
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

        private static GUIStyle m_GroupBox;
        public static GUIStyle GroupBox
        {
            get
            {
                if(m_GroupBox == null)
                {
                    m_GroupBox = new GUIStyle("GroupBox");
                }
                    return m_GroupBox;
            }
        }

        private static GUIStyle m_SearchTextField;
        public static GUIStyle SearchTextField
        {
            get
            {
                if (m_SearchTextField == null)
                {
                    m_SearchTextField = new GUIStyle("SearchTextField");
                }
                return m_SearchTextField;
            }
        }

    }
}

