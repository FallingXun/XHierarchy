using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}

