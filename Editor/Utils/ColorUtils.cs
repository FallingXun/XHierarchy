using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public class ColorUtils
    {
        private static readonly Color m_LightSkinColor = new Color(0.541f, 0.541f, 0.541f, 1.0f);
        private static readonly Color m_DarkSkinColor = new Color(0.098f, 0.098f, 0.098f, 1.0f);
        public static Color SkinBackgroundColor
        {
            get
            {
                return EditorGUIUtility.isProSkin ? m_DarkSkinColor : m_LightSkinColor;

            }

        }

    }
}