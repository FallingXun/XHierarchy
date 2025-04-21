using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;

namespace XHierarchy
{
    public sealed class ConfigData : ScriptableObject, IConfig
    {
        private List<IModule> m_Modules = new List<IModule>()
        {
            new HierarchyLinesModule(),
            new ScriptIconsModule(),
            new IdentifierModule(),
            new NoteModule(),
            new AdditionalModule(),
            new ActiveModule(),
        };

        private List<Type> m_HandleComponentTypes = new List<Type>()
        {
            typeof(RectTransform),
            typeof(Image),
            typeof(RawImage),
            typeof(Text),
            typeof(ScrollRect),
        };

        private string GetNote(GameObject go)
        {
            return "";
        }

        private void SetNote(GameObject go, string note)
        {

        }

        private int GetIdentifier(GameObject go)
        {
            return 0;
        }

        private void SetIdentifier(GameObject go, int identifier)
        {
            
        }

        #region IConfig

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

        public Func<GameObject, string> NoteFunc
        {
            get
            {
                return GetNote;
            }
        }

        public Action<GameObject, string> NoteApplyAction
        {
            get
            {
                return SetNote;
            }
        }

        public Func<GameObject, int> IdentifierFunc
        {
            get
            {
                return GetIdentifier;
            }
        }

        public Action<GameObject, int> IdentifierApplyAction
        {
            get
            {
                return SetIdentifier;
            }
        }


        public void Init()
        {

        }

        #endregion
    }
}

