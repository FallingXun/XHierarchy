using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XHierarchy
{
    public class StyleUtils
    {
        private static readonly GUIStyle m_BtnClose = Application.platform == RuntimePlatform.OSXEditor ? "WinBtnCloseMac" : "WinBtnClose";
        public static GUIStyle BtnClose
        {
            get
            {
                return m_BtnClose;
            }
        }

        private static readonly GUIStyle m_ToggleLock = "IN LockButton";
        public static GUIStyle ToggleLock
        {
            get
            {
                return m_ToggleLock;
            }
        }
    }
}

