using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public static class RectExtension
    {
        #region Rect Set
        public static Rect SetX(this Rect rect, float x)
        {
            rect.x = x;
            return rect;
        }

        public static Rect SetY(this Rect rect, float y)
        {
            rect.y = y;
            return rect;
        }

        public static Rect SetXMin(this Rect rect, float xMin)
        {
            rect.xMin = xMin;
            return rect;
        }

        public static Rect SetYMin(this Rect rect, float yMin)
        {
            rect.yMin = yMin;
            return rect;
        }

        public static Rect SetXMax(this Rect rect, float xMax)
        {
            rect.xMax = xMax;
            return rect;
        }

        public static Rect SetYMax(this Rect rect, float yMax)
        {
            rect.yMax = yMax;
            return rect;
        }


        public static Rect SetWidth(this Rect rect, float width)
        {
            rect.width = width;
            return rect;
        }

        public static Rect SetHeight(this Rect rect, float height)
        {
            rect.height = height;
            return rect;
        }

        public static Rect SetWidthFromRight(this Rect rect, float width)
        {
            rect.x += rect.width;
            rect.width = width;
            rect.x -= width;
            return rect;
        }

        public static Rect SetHeightFromBottom(this Rect rect, float height)
        {
            rect.y += rect.height;
            rect.height = height;
            rect.y -= height;
            return rect;
        }

        public static Rect SetPosition(this Rect rect, Vector2 postion)
        {
            rect.position = postion;
            return rect;
        }

        public static Rect SetCenter(this Rect rect, Vector2 center)
        {
            rect.center = center;
            return rect;
        }

        public static Rect SetMin(this Rect rect, Vector2 min)
        {
            rect.min = min;
            return rect;
        }

        public static Rect SetMax(this Rect rect, Vector2 max)
        {
            rect.max = max;
            return rect;
        }

        public static Rect SetSize(this Rect rect, Vector2 size)
        {
            rect.size = size;
            return rect;
        }


        #endregion

        #region Rect Modify

        public static Rect MoveX(this Rect rect, float x)
        {
            rect.x += x;
            return rect;
        }

        public static Rect MoveY(this Rect rect, float y)
        {
            rect.y += y;
            return rect;
        }

        public static Rect AddWidth(this Rect rect, float width)
        {
            rect.width = Mathf.Max(rect.width + width, 0);
            return rect;
        }

        public static Rect AddHeight(this Rect rect, float height)
        {
            rect.height = Mathf.Max(rect.height + height, 0);
            return rect;
        }

        #endregion

        #region Rect Draw

        public static Rect DrawLine(this Rect rect, Color color)
        {
            EditorGUI.DrawRect(rect, color);

            return rect;
        }

        public static Rect DrawIcon(this Rect rect, Texture texture)
        {
            GUI.DrawTexture(rect, texture);

            return rect;
        }

        public static Rect DrawBackground(this Rect rect, Color color)
        {
            EditorGUI.DrawRect(rect, color);

            return rect;
        }

        #endregion


        #region Rect Event
        public static bool IsHovered(this Rect rect)
        {
            return rect.Contains(Event.current.mousePosition);
        }

        #endregion
    }
}

