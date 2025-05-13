using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XHierarchy
{
    public class Const 
    {
        /// <summary>
        /// 第一行的起始位置
        /// </summary>
        public static readonly float ITEM_START_X = 32;
        /// <summary>
        /// 每一行的缩进（即三角箭头的大小）
        /// </summary>
        public static readonly float ITEM_INDENT = 14;
        /// <summary>
        /// 行高
        /// </summary>
        public static readonly float ITEM_HEIGHT = 16;
        /// <summary>
        /// 层级线粗细
        /// </summary>
        public static readonly float HIERARCHY_LINE_THICKNESS = 1;
        /// <summary>
        /// 图标尺寸
        /// </summary>
        public static readonly float ICON_SIZE = 16;

        public static readonly string PACKAGE_NAME = "Packages/com.xun.hierarchy";

        public static readonly string ASSET_PATH = "Assets/XHierarchy";

        public static readonly string HIERARCHY_ASSET_PATH = ASSET_PATH + "/Editor/HierarchyData.asset";

        public static readonly string DEFAULT_CONFIG_PATH = ASSET_PATH + "/Editor/ConfigData.asset";

        public static readonly string CUSTOM_CONFIG_PATH = ASSET_PATH + "/Editor/CustomConfigData.asset";


        public static readonly string KEY_XHIERARCHY_PATCH = "XHIERARCHY_PATCH";
        public static readonly string KEY_ACTIVE = "XHIERARCHY_KEY_ACTIVE";
        public static readonly string KEY_ADDITIONAL = "XHIERARCHY_KEY_ADDITIONAL";
        public static readonly string KEY_HIERARCHY_LINE = "XHIERARCHY_KEY_HIERARCHY_LINE";
        public static readonly string KEY_IDENTIFIER = "XHIERARCHY_KEY_IDENTIFIER";
        public static readonly string KEY_NOTE = "XHIERARCHY_KEY_NOTE";
        public static readonly string KEY_SCRIPT_ICONS = "XHIERARCHY_KEY_SCRIPT_ICONS";
        public static readonly string KEY_SEARCH = "XHIERARCHY_KEY_SEARCH";
    }

}
