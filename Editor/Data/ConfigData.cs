using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace XHierarchy
{
    public class ConfigData : ScriptableObject, IConfig
    {
        private List<IModule> m_Modules = new List<IModule>()
        {
            new HierarchyLinesModule(),
            new ScriptIconsModule(),
        };

        private List<Type> m_HandleComponentTypes = new List<Type>()
        {
            typeof(RectTransform),
            typeof(Image),
            typeof(RawImage),
            typeof(Text),
            typeof(ScrollRect),
        };


        public List<IModule> Modules
        {
            get
            {
                return m_Modules;
            }
        }

        public float GameObjectGUILeftOffset
        {
            get
            {
                return 0;
            }
        }

        public float GameObjectGUIRightOffset
        {
            get
            {
                return 0;
            }
        }

        public List<Type> HandleComponentTypes
        {
            get
            {
                return m_HandleComponentTypes;
            }
        }
    }
}

