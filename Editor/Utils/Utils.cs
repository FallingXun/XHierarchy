using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace XHierarchy
{
    public class Utils
    {
        public static bool GetModuleEnabled(string name)
        {
            return PlayerPrefs.GetInt(name, 0) > 0;
        }

        public static void SetModuleEnabled(string name, bool enabled)
        {
            PlayerPrefs.SetInt(name, enabled ? 1 : 0);

        }


        public static string GetPackageRootPath()
        {
            var path = Path.GetFullPath(Const.PACKAGE_NAME);
            return path;
        }

    }

}
