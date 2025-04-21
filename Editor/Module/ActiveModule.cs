using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;


namespace XHierarchy
{
    public class ActiveModule : IModule
    {

        public string Name
        {
            get
            {
                return typeof(ActiveModule).FullName;
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
            if (selectionRect.x > Const.ITEM_START_X + Const.ITEM_INDENT && selectionRect.SetXMin(0).IsHovered())
            {
                var rect = selectionRect.SetX(Const.ITEM_START_X).SetWidth(Const.ITEM_INDENT);
                var active = go.activeSelf;
                if (GUI.Toggle(rect, go.activeSelf, "") != active)
                {
                    Undo.RecordObject(go, "");
                    go.SetActive(!active);
                }
            }
            return availableRect;
        }

        public Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }
    }

}