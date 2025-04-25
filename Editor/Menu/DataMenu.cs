using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;

namespace XHierarchy
{
    public class DataMenu : Editor
    {
        private const string XHIERARCHY = "XHierarchy/";

        private const string GROUP_DATA = XHIERARCHY + "Data";
        private const string DATA_CREATE_HIERARCHY_DATA_ASSET = XHIERARCHY + "Create Hierarchy Data Asset";
        private const string DATA_SET_CUSTOM_CONFIG = XHIERARCHY + "Set Custom Config";
        private const string DATA_CLEAR_CUSTOM_CONFIG = XHIERARCHY + "Clear Custom Config";


        private const string GROUP_FUNCTION = XHIERARCHY + "Function";
        private const string FUNCTION_ACTIVE = XHIERARCHY + "Show Item Active Toggle";
        private const string FUNCTION_ADDITIONAL = XHIERARCHY + "Open Item Icon Click";
        private const string FUNCTION_HIERARCHY_LINE = XHIERARCHY + "Show Hierarchy Line";
        private const string FUNCTION_IDENTIFIER = XHIERARCHY + "Show Item Identifier";
        private const string FUNCTION_NOTE = XHIERARCHY + "Show Item Note";
        private const string FUNCTION_SCRIPT_ICONS = XHIERARCHY + "Show Script Icons";
        private const string FUNCTION_SEARCH = XHIERARCHY + "Open Additional Search";


        [MenuItem(GROUP_DATA, false, 1)]
        private static void _GROUP_DATA()
        {

        }

        [MenuItem(GROUP_DATA, true, 1)]
        private static bool __GROUP_DATA()
        {
            return false;
        }

        [MenuItem(DATA_CREATE_HIERARCHY_DATA_ASSET, false, 2)]
        private static void _DATA_CREATE_HIERARCHY_DATA_ASSET()
        {
            var configDataSO = ScriptableObject.CreateInstance<ConfigData>();
            var configDirPath = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), Const.DEFAULT_CONFIG_PATH));
            if (Directory.Exists(configDirPath) == false)
            {
                Directory.CreateDirectory(configDirPath);
            }
            AssetDatabase.CreateAsset(configDataSO, Const.DEFAULT_CONFIG_PATH);
            AssetDatabase.Refresh();


            var hierarchyDataSO = ScriptableObject.CreateInstance<HierarchyData>();
            var configData = AssetDatabase.LoadAssetAtPath<ConfigData>(Const.DEFAULT_CONFIG_PATH);
            hierarchyDataSO.SetDefaultConfig(configData);
            var hierarchyDataDirPath = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), Const.HIERARCHY_ASSET_PATH));
            if (Directory.Exists(hierarchyDataDirPath) == false)
            {
                Directory.CreateDirectory(hierarchyDataDirPath);
            }
            AssetDatabase.CreateAsset(hierarchyDataSO as HierarchyData, Const.HIERARCHY_ASSET_PATH);
            AssetDatabase.Refresh();

            Utils.CreateCustomConfigDataScript();
            AssetDatabase.Refresh();
        }

        [MenuItem(DATA_SET_CUSTOM_CONFIG, false, 3)]
        private static void _DATA_SET_CUSTOM_CONFIG()
        {
            var type = Assembly.Load("Assembly-CSharp-Editor").GetType("XHierarchy.CustomConfigData");
            Debug.Log(type);
            if (type == null)
            {
                return;
            }
            var hierarchyData = AssetDatabase.LoadAssetAtPath<HierarchyData>(Const.HIERARCHY_ASSET_PATH);
            if (hierarchyData != null && hierarchyData.Config.GetType() == type)
            {
                return;
            }

            var customConfigDataSO = ScriptableObject.CreateInstance(type);
            var customConfigDirPath = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), Const.CUSTOM_CONFIG_PATH));
            if (Directory.Exists(customConfigDirPath) == false)
            {
                Directory.CreateDirectory(customConfigDirPath);
            }
            AssetDatabase.CreateAsset(customConfigDataSO, Const.CUSTOM_CONFIG_PATH);
            AssetDatabase.Refresh();

            hierarchyData.SetCustomConfig(customConfigDataSO as IConfig);
            EditorUtility.SetDirty(hierarchyData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            HierarchyPatch.InitConfig();
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(DATA_CLEAR_CUSTOM_CONFIG, false, 4)]
        private static void _DATA_REMOVE_CUSTOM_CONFIG()
        {
            var hierarchyData = AssetDatabase.LoadAssetAtPath<HierarchyData>(Const.HIERARCHY_ASSET_PATH);
            hierarchyData.SetCustomConfig(null);
            HierarchyPatch.InitConfig();
            EditorApplication.RepaintHierarchyWindow();
        }


        [MenuItem(GROUP_FUNCTION, false, 101)]
        private static void _GROUP_FUNCTION()
        {

        }

        [MenuItem(GROUP_FUNCTION, true, 101)]
        private static bool __GROUP_FUNCTION()
        {
            return false;
        }


        [MenuItem(FUNCTION_ACTIVE, false, 102)]
        private static void _FUNCTION_ACTIVE()
        {
            var enabled = Utils.GetModuleEnabled(Const.KEY_ACTIVE);
            Utils.SetModuleEnabled(Const.KEY_ACTIVE, !enabled);
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(FUNCTION_ACTIVE, true, 102)]
        private static bool __FUNCTION_ACTIVE()
        {
            Menu.SetChecked(FUNCTION_ACTIVE, Utils.GetModuleEnabled(Const.KEY_ACTIVE));
            return true;
        }


        [MenuItem(FUNCTION_ADDITIONAL, false, 103)]
        private static void _FUNCTION_ADDITIONAL()
        {
            var enabled = Utils.GetModuleEnabled(Const.KEY_ADDITIONAL);
            Utils.SetModuleEnabled(Const.KEY_ADDITIONAL, !enabled);
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(FUNCTION_ADDITIONAL, true, 103)]
        private static bool __FUNCTION_ADDITIONAL()
        {
            Menu.SetChecked(FUNCTION_ADDITIONAL, Utils.GetModuleEnabled(Const.KEY_ADDITIONAL));
            return true;
        }


        [MenuItem(FUNCTION_HIERARCHY_LINE, false, 104)]
        private static void _FUNCTION_HIERARCHY_LINE()
        {
            var enabled = Utils.GetModuleEnabled(Const.KEY_HIERARCHY_LINE);
            Utils.SetModuleEnabled(Const.KEY_HIERARCHY_LINE, !enabled);
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(FUNCTION_HIERARCHY_LINE, true, 104)]
        private static bool __FUNCTION_HIERARCHY_LINE()
        {
            Menu.SetChecked(FUNCTION_HIERARCHY_LINE, Utils.GetModuleEnabled(Const.KEY_HIERARCHY_LINE));
            return true;
        }



        [MenuItem(FUNCTION_IDENTIFIER, false, 105)]
        private static void _FUNCTION_IDENTIFIER()
        {
            var enabled = Utils.GetModuleEnabled(Const.KEY_IDENTIFIER);
            Utils.SetModuleEnabled(Const.KEY_IDENTIFIER, !enabled);
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(FUNCTION_IDENTIFIER, true, 105)]
        private static bool __FUNCTION_IDENTIFIER()
        {
            Menu.SetChecked(FUNCTION_IDENTIFIER, Utils.GetModuleEnabled(Const.KEY_IDENTIFIER));
            return true;
        }



        [MenuItem(FUNCTION_NOTE, false, 106)]
        private static void _FUNCTION_NOTE()
        {
            var enabled = Utils.GetModuleEnabled(Const.KEY_NOTE);
            Utils.SetModuleEnabled(Const.KEY_NOTE, !enabled);
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(FUNCTION_NOTE, true, 106)]
        private static bool __FUNCTION_NOTE()
        {
            Menu.SetChecked(FUNCTION_NOTE, Utils.GetModuleEnabled(Const.KEY_NOTE));
            return true;
        }



        [MenuItem(FUNCTION_SCRIPT_ICONS, false, 107)]
        private static void _FUNCTION_SCRIPT_ICONS()
        {
            var enabled = Utils.GetModuleEnabled(Const.KEY_SCRIPT_ICONS);
            Utils.SetModuleEnabled(Const.KEY_SCRIPT_ICONS, !enabled);
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(FUNCTION_SCRIPT_ICONS, true, 107)]
        private static bool __FUNCTION_SCRIPT_ICONS()
        {
            Menu.SetChecked(FUNCTION_SCRIPT_ICONS, Utils.GetModuleEnabled(Const.KEY_SCRIPT_ICONS));
            return true;
        }



        [MenuItem(FUNCTION_SEARCH, false, 108)]
        private static void _FUNCTION_SEARCH()
        {
            var enabled = Utils.GetModuleEnabled(Const.KEY_SEARCH);
            Utils.SetModuleEnabled(Const.KEY_SEARCH, !enabled);
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(FUNCTION_SEARCH, true, 108)]
        private static bool __FUNCTION_SEARCH()
        {
            Menu.SetChecked(FUNCTION_SEARCH, Utils.GetModuleEnabled(Const.KEY_SEARCH));
            return true;
        }

    }

}
