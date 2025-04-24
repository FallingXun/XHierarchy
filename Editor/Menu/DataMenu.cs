using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace XHierarchy
{
    public class DataMenu : Editor
    {
        [MenuItem("XHierarchy/Data/Create Hierarchy Data Asset")]
        public static void CreateHierarchyData()
        {
            var configDataSO = ScriptableObject.CreateInstance<ConfigData>();
            var configDirPath = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), Const.DEFAULT_CONFIG_PATH));
            if (Directory.Exists(configDirPath) == false)
            {
                Directory.CreateDirectory(configDirPath);
            }
            AssetDatabase.CreateAsset(configDataSO as ConfigData, Const.DEFAULT_CONFIG_PATH);
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

        }

    }

}
