using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;
using System.Reflection;

namespace XHierarchy
{
    public sealed class ConfigData : ScriptableObject, IConfig
    {
        private List<Type> m_ComponentTypes = new List<Type>();

        #region IConfig

        public List<Type> ComponentTypes
        {
            get
            {
                return m_ComponentTypes;
            }
        }

        public string GetNote(GameObject go)
        {
            return "";
        }

        public void SetNote(GameObject go, string note)
        {

        }

        public int GetIdentifier(GameObject go)
        {
            return 0;
        }

        public void SetIdentifier(GameObject go, int identifier)
        {

        }

        public Vector2 GetItemGUIRange(GameObject go)
        {
            return Vector2.zero;
        }

        public void SetItemGUIRange(GameObject go, Vector2 range)
        {

        }


        public void Init()
        {
            m_ComponentTypes.Clear();

            var typeComponent = typeof(Component);

            var assemblyList = new List<Assembly>();
            assemblyList.Add(Assembly.GetExecutingAssembly());
            assemblyList.Add(Assembly.Load("UnityEngine.CoreModule"));
            assemblyList.Add(Assembly.Load("UnityEngine.UI"));
            assemblyList.Add(Assembly.Load("Assembly-CSharp-Editor"));
            assemblyList.Add(Assembly.Load("Assembly-CSharp"));

            foreach (var assembly in assemblyList)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }
                    if (type.IsSubclassOf(typeComponent))
                    {
                        m_ComponentTypes.Add(type);
                    }
                }
            }
        }

        #endregion
    }
}

