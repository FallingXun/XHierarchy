using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XHierarchy
{
    public class HierarchyLinesModule : IModule
    {
        private Dictionary<int, bool> m_DepthLastDict = new Dictionary<int, bool>();

        public string Name
        {
            get
            {
                return typeof(HierarchyLinesModule).FullName;
            }
        }

        public bool Enabled
        {
            get
            {
                return true;
                return Utils.GetModuleEnabled(Name);
            }
            set
            {
                Utils.SetModuleEnabled(Name, value);
            }
        }

        public void Init(IConfig config)
        {

        }

        public Rect OnItemGUI(GameObject go, Rect selectionRect, Rect availableRect)
        {
            var depth = GetDepth(selectionRect);

            m_DepthLastDict.Clear();
            var tf = go.transform.parent;
            var d = depth - 1;
            while (tf != null)
            {
                if (IsLastIndex(tf))
                {
                    m_DepthLastDict[d] = true;
                }
                d--;
                tf = tf.parent;
            }

            bool isLastIndex = IsLastIndex(go.transform);
            bool hasChildren = HasChildren(go.transform);
            bool isRoot = IsRoot(go.transform);
            // 根节点不绘制
            if (isRoot == false)
            {
                for (int i = 1; i <= depth; i++)
                {
                    if (m_DepthLastDict.ContainsKey(i))
                    {
                        // 当前深度的最后一个节点在此节点之前，此节点不需要绘制此深度
                        continue;
                    }
                    selectionRect.SetX(Const.ITEM_START_X + (i - 0.5f) * Const.ITEM_INDENT)
                        .SetWidth(Const.HIERARCHY_LINE_THICKNESS)
                        .SetHeight(isLastIndex && i == depth ? Const.ITEM_HEIGHT / 2 : Const.ITEM_HEIGHT)
                        .DrawLine(Color.gray);
                }
                if (depth > 0)
                {
                    selectionRect.SetX(Const.ITEM_START_X + (depth - 0.5f) * Const.ITEM_INDENT)
                        .MoveY(Const.ITEM_HEIGHT / 2)
                        .SetWidth(hasChildren ? Const.ITEM_INDENT / 2 : Const.ITEM_INDENT)
                        .SetHeight(Const.HIERARCHY_LINE_THICKNESS)
                        .DrawLine(Color.gray);
                }

            }
            return availableRect;
        }

        public Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }



        private bool IsLastIndex(Transform tf)
        {
            if (tf.parent != null && tf.parent.childCount - 1 == tf.GetSiblingIndex())
            {
                return true;
            }
            return false;
        }

        private bool IsRoot(Transform tf)
        {
            if (tf.parent == null)
            {
                return true;
            }
            return false;
        }

        private bool HasChildren(Transform tf)
        {
            if (tf.childCount > 0)
            {
                return true;
            }
            return false;
        }

        private int GetDepth(Rect rect)
        {
            var value = (rect.x - Const.ITEM_START_X - Const.ITEM_INDENT) / Const.ITEM_INDENT;
            return value.RoundToInt();
        }

        private int GetDepth(Transform tf)
        {
            var value = 0;
            while (tf.parent != null)
            {
                value++;
                tf = tf.parent;
            }
            return value;
        }
    }

}
