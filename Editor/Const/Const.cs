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
        public const float ITEM_START_X = 32;
        /// <summary>
        /// 每一行的缩进（即三角箭头的大小）
        /// </summary>
        public const float ITEM_INDENT = 14;
        /// <summary>
        /// 行高
        /// </summary>
        public const float ITEM_HEIGHT = 16;
        /// <summary>
        /// 层级线粗细
        /// </summary>
        public const float HIERARCHY_LINE_THICKNESS = 1;
        /// <summary>
        /// 图标尺寸
        /// </summary>
        public const float ICON_SIZE = 16;

        public const string PACKAGE_NAME = "Packages/com.xun.hierarchy";

        public const string ASSET_PATH = "Assets/XHierarchy";

        public const string HIERARCHY_ASSET_PATH = ASSET_PATH + "/Editor/HierarchyData.asset";

        public const string DEFAULT_CONFIG_PATH = ASSET_PATH + "/Editor/ConfigData.asset";

        public const string CUSTOM_CONFIG_PATH = ASSET_PATH + "/Editor/CustomConfigData.asset";

        public const string KEY_ACTIVE = "Show Item Active Toggle";
        public const string KEY_ADDITIONAL = "Open Item Icon Click";
        public const string KEY_HIERARCHY_LINE = "Show Hierarchy Line";
        public const string KEY_IDENTIFIER = "Show Item Identifier";
        public const string KEY_NOTE = "Show Item Note";
        public const string KEY_SCRIPT_ICONS = "Show Script Icons";
        public const string KEY_SEARCH = "Open Additional Search";
    }

}
