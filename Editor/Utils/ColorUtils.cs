using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public class ColorUtils
    {
        private static readonly Color m_LightBackgroundSkinColor = new Color(0.541f, 0.541f, 0.541f, 1.0f);
        private static readonly Color m_DarkBackgroundSkinColor = new Color(0.098f, 0.098f, 0.098f, 1.0f);
        public static Color SkinBackgroundColor
        {
            get
            {
                return EditorGUIUtility.isProSkin ? m_DarkBackgroundSkinColor : m_LightBackgroundSkinColor;

            }
        }

        private static readonly Color m_LightBackgroundHoverColor = new Color(0.3f, 0.3f, 0.3f, 1.0f);
        private static readonly Color m_DrakBackgroundHoverColor = new Color(0.3f, 0.3f, 0.3f, 1.0f);
        public static Color HoverBackgroundColor
        {
            get
            {
                return EditorGUIUtility.isProSkin ? m_DrakBackgroundHoverColor : m_LightBackgroundHoverColor;
            }
        }

        private static readonly Color m_LightWindowTitleBackgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.2f);
        private static readonly Color m_DrakWindowTitleBackgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.2f);
        public static Color WindowTitleBackgroundColor
        {
            get
            {
                return EditorGUIUtility.isProSkin ? m_DrakWindowTitleBackgroundColor : m_LightWindowTitleBackgroundColor;
            }
        }

        private static readonly Color m_LightWindowOutlineColor = new Color(0.1f, 0.1f, 0.1f, 1f);
        private static readonly Color m_DrakWindowOutlineColor = new Color(0.1f, 0.1f, 0.1f, 1f);
        public static Color WindowOutlineColor
        {
            get
            {
                return EditorGUIUtility.isProSkin ? m_DrakWindowOutlineColor : m_LightWindowOutlineColor;
            }
        }
    }
}