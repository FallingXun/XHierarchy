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
        private Type m_AdditionalDataRecorderType = null;
        private Type AdditionalDataRecorderType
        {
            get
            {
                if (m_AdditionalDataRecorderType == null)
                {
                    m_AdditionalDataRecorderType = Assembly.Load("Assembly-CSharp").GetType("XHierarchy.AdditionalDataRecorder");
                    if (m_AdditionalDataRecorderType != null)
                    {
                        m_NoteFieldInfo = m_AdditionalDataRecorderType.GetField("Note");
                        m_IdentifierFieldInfo = m_AdditionalDataRecorderType.GetField("Identifier");
                    }
                }
                return m_AdditionalDataRecorderType;
            }
        }
        private FieldInfo m_NoteFieldInfo = null;
        private FieldInfo m_IdentifierFieldInfo = null;

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
            if (AdditionalDataRecorderType != null && m_NoteFieldInfo != null)
            {
                var recorder = go.GetComponent(m_AdditionalDataRecorderType);
                if (recorder != null)
                {
                    return (string)m_NoteFieldInfo.GetValue(recorder);
                }
            }
            return null;
        }

        public void SetNote(GameObject go, string note)
        {
            if (AdditionalDataRecorderType != null && m_NoteFieldInfo != null)
            {
                var recorder = go.GetComponent(m_AdditionalDataRecorderType);
                if (recorder == null)
                {
                    recorder = go.AddComponent(m_AdditionalDataRecorderType);
                }
                m_NoteFieldInfo.SetValue(recorder, note);
                EditorUtility.SetDirty(go);
            }
            else
            {
                Debug.LogError("ConfigData need type 'AdditionalDataRecorder' but it is missing, please use 'XHierarchy/Data/Create Hierarchy Data Asset' to generate!");
            }
        }

        public int GetIdentifier(GameObject go)
        {
            if (AdditionalDataRecorderType != null && m_IdentifierFieldInfo != null)
            {
                var recorder = go.GetComponent(m_AdditionalDataRecorderType);
                if (recorder != null)
                {
                    return (int)m_IdentifierFieldInfo.GetValue(recorder);
                }
            }
            return 0;
        }

        public void SetIdentifier(GameObject go, int identifier)
        {
            if (AdditionalDataRecorderType != null && m_IdentifierFieldInfo != null)
            {
                var recorder = go.GetComponent(m_AdditionalDataRecorderType);
                if (recorder == null)
                {
                    recorder = go.AddComponent(m_AdditionalDataRecorderType);
                }
                m_IdentifierFieldInfo.SetValue(recorder, identifier);
                EditorUtility.SetDirty(go);
            }
            else
            {
                Debug.LogError("ConfigData need type 'AdditionalDataRecorder' but it is missing, please use 'XHierarchy/Data/Create Hierarchy Data Asset' to generate!");
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

