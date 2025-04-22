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
        private List<IModule> m_Modules = new List<IModule>();

        private List<Type> m_HandleComponentTypes = new List<Type>();

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
            m_Modules.Clear();
            m_HandleComponentTypes.Clear();

            var typeIModule = typeof(IModule);
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
                    if (typeIModule.IsAssignableFrom(type))
                    {
                        var module = Activator.CreateInstance(type) as IModule;
                        m_Modules.Add(module);
                    }
                    else
                    {
                        if (type.IsSubclassOf(typeComponent))
                        {
                            m_HandleComponentTypes.Add(type);
                        }
                    }
                }
            }
        }

        #endregion
    }
}

