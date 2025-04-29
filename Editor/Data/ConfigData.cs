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
        [Tooltip("If turn on,'AdditionalDataRecorder' component will be added to gameObject when call fuction 'SetNote' or 'SetIdentifier")]
        [SerializeField]
        private bool m_UseAdditionalComponent = false;

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
            if (m_UseAdditionalComponent)
            {
                var recorder = go.GetComponent<AdditionalDataRecorder>();
                if (recorder != null)
                {
                    return recorder.Note;
                }
            }
            return null;
        }

        public void SetNote(GameObject go, string note)
        {
            if (m_UseAdditionalComponent)
            {
                var recorder = go.GetComponent<AdditionalDataRecorder>();
                if (recorder == null)
                {
                    recorder = go.AddComponent<AdditionalDataRecorder>();
                }
                recorder.Note = note;
                EditorUtility.SetDirty(go);
            }
        }

        public int GetIdentifier(GameObject go)
        {
            if (m_UseAdditionalComponent)
            {
                var recorder = go.GetComponent<AdditionalDataRecorder>();
                if (recorder != null)
                {
                    return recorder.Identifier;
                }
            }
            return 0;
        }

        public void SetIdentifier(GameObject go, int identifier)
        {
            if (m_UseAdditionalComponent)
            {
                var recorder = go.GetComponent<AdditionalDataRecorder>();
                if (recorder == null)
                {
                    recorder = go.AddComponent<AdditionalDataRecorder>();
                }
                recorder.Identifier = identifier;
                EditorUtility.SetDirty(go);
            }
        }

        public Vector2 GetItemGUIRange(GameObject go)
        {
            return Vector2.zero;
        }

        public void Init()
        {
            m_ComponentTypes.Clear();

            var typeComponent = typeof(Component);

            var assemblyList = new List<Assembly>();
            assemblyList.Add(Assembly.GetExecutingAssembly());
            var assemblyNameList = new List<string>()
            {
                "UnityEngine.CoreModule",
                "UnityEngine.UI",
                "Assembly-CSharp-Editor",
                "Assembly-CSharp",
            };
            foreach (var name in assemblyNameList)
            {
                try
                {
                    var assembly = Assembly.Load(name);
                    if (assembly != null)
                    {
                        assemblyList.Add(assembly);
                    }
                }
                catch (Exception e)
                {

                }
            }

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

