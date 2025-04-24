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

        private List<Type> m_ComponentTypes = new List<Type>();

        private IHandler m_Handler = null;

        #region IConfig

        public List<IModule> Modules
        {
            get
            {
                return m_Modules;
            }
        }

        public List<Type> ComponentTypes
        {
            get
            {
                return m_ComponentTypes;
            }
        }

        public IHandler Handler
        {
            get
            {
                return m_Handler;
            }
        }


        public void Init()
        {
            m_Modules.Clear();
            m_ComponentTypes.Clear();

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
                            m_ComponentTypes.Add(type);
                        }
                    }
                }
            }
            m_Modules.Sort((a, b) => 
            {
                return a.Priority.CompareTo(b.Priority);
            });

            m_Handler = new DefaultHandler();
        }

        #endregion
    }
}

