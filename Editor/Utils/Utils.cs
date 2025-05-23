using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace XHierarchy
{
    public class Utils
    {
        public static bool GetPatchEnabled()
        {
            return PlayerPrefs.GetInt(Const.KEY_XHIERARCHY_PATCH, 0) > 0;
        }


        public static void SetPatchEnabled(bool enabled)
        {
            PlayerPrefs.SetInt(Const.KEY_XHIERARCHY_PATCH, enabled ? 1 : 0);
            HierarchyPatch.Enabled = enabled;
        }


        public static bool GetModuleEnabled(string name)
        {
            return PlayerPrefs.GetInt(name, 0) > 0;
        }

        public static void SetModuleEnabled(string name, bool enabled)
        {
            PlayerPrefs.SetInt(name, enabled ? 1 : 0);
            HierarchyPatch.SetModuleEnabled(name, enabled);
        }


        public static string GetPackageRootPath()
        {
            var path = Path.GetFullPath(Const.PACKAGE_NAME);
            return path;
        }

        public static void CreateScript(string assetsPath, string content)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), Const.ASSET_PATH, assetsPath);
            if (File.Exists(path))
            {
                return;
            }
            var dir = Path.GetDirectoryName(path);
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }
            using (var file = File.CreateText(path))
            {
                file.Write(content);
            }
        }
    }

}
