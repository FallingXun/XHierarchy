using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;


namespace XHierarchy
{
    public class SearchModule : IModule
    {
        public string Name
        {
            get
            {
                return typeof(SearchModule).FullName;
            }
        }

        public bool Enabled
        {
            get
            {
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
            return availableRect;
        }

        public Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }
    }

}
