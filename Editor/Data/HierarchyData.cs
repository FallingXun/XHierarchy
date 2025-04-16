using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace XHierarchy
{
    public class HierarchyData: ScriptableObject
    {
        [SerializeField]
        private Object m_CustomConfig;

        [SerializeField]
        private Object m_DefaultConfig;


        public IConfig Config
        {
            get
            {
                if (m_CustomConfig is IConfig)
                {
                    return m_CustomConfig as IConfig;
                }
                return m_DefaultConfig as IConfig;
            }
        }

        public void SetDefaultConfig(IConfig config)
        {
            m_DefaultConfig = config as Object;
        }

        public void SetCustomConfig(IConfig config)
        {
            m_CustomConfig = config as Object;
        }

    }
}

