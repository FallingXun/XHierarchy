using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public class DataMenu : Editor
    {
        [MenuItem("XHierarchy/Data/Create Hierarchy Data Asset")]
        public static void CreateHierarchyData()
        {
            var configDataSO = ScriptableObject.CreateInstance<ConfigData>();
            AssetDatabase.CreateAsset(configDataSO as ConfigData, Const.DEFAULT_CONFIG_PATH);
            AssetDatabase.Refresh();


            var hierarchyDataSO = ScriptableObject.CreateInstance<HierarchyData>();
            var configData = AssetDatabase.LoadAssetAtPath<ConfigData>(Const.DEFAULT_CONFIG_PATH);
            hierarchyDataSO.SetDefaultConfig(configData);
            AssetDatabase.CreateAsset(hierarchyDataSO as HierarchyData, Const.HIERARCHY_ASSET_PATH);
            AssetDatabase.Refresh();

        }

    }

}
