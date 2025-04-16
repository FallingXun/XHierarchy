using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

namespace XHierarchy
{
    public class ScriptIconsModule : IModule
    {
        private List<Component> m_ComponentList = new List<Component>();
        private List<IconData> m_IconDataList = new List<IconData>();
        private Dictionary<Type, int> m_TypeDict = new Dictionary<Type, int>();
        private Dictionary<Type, Texture> m_TypeIconDict = new Dictionary<Type, Texture>();
        private Texture m_DefaultIcon = null;
        private Color m_HoverBackgroundColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        private IConfig m_Config = null;

        public string Name
        {
            get
            {
                return typeof(ScriptIconsModule).FullName;
            }
        }

        public bool Enabled
        {
            get
            {
                return true;
                return Utils.GetModuleEnabled(Name);
            }
            set
            {
                Utils.SetModuleEnabled(Name, value);
            }
        }

        public void Init(IConfig config)
        {
            m_Config = config;
            m_TypeDict.Clear();
            for (int i = 0; i < config.HandleComponentTypes.Count; i++)
            {
                m_TypeDict[config.HandleComponentTypes[i]] = i;
            }
            m_DefaultIcon = EditorGUIUtility.IconContent("cs Script Icon").image;
        }

        public Rect OnItemGUI(GameObject go, Rect selectionRect, Rect availableRect)
        {
            m_ComponentList.Clear();
            m_IconDataList.Clear();
            go.GetComponents(m_ComponentList);
            if (m_ComponentList.Count > 0)
            {
                for (int i = 0; i < m_ComponentList.Count; i++)
                {
                    if (availableRect.width < Const.ICON_SIZE)
                    {
                        break;
                    }
                    if (m_TypeDict.TryGetValue(m_ComponentList[i].GetType(), out int value) == false)
                    {
                        continue;
                    }
                    var iconData = new IconData();
                    iconData.component = m_ComponentList[i];
                    iconData.index = value;
                    iconData.rect = availableRect.SetWidthFromRight(Const.ICON_SIZE);
                    iconData.icon = GetIcon(m_ComponentList[i]);
                    m_IconDataList.Add(iconData);
                    availableRect = availableRect.AddWidth(-Const.ICON_SIZE);
                }
            }
            m_IconDataList.Sort((a, b) =>
            {
                return a.index.CompareTo(b.index);
            });
            foreach (var iconData in m_IconDataList)
            {
                if (iconData.rect.IsHovered())
                {
                    iconData.rect.DrawBackground(m_HoverBackgroundColor);
                }
                var color = GUI.color;
                GUI.color = Color.gray;
                iconData.rect.DrawIcon(iconData.icon);
                GUI.color = color;
            }
            return availableRect;
        }

        public Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect)
        {
            return availableRect;
        }

        private Texture GetIcon(Component component)
        {
            var type = component.GetType();
            if (m_TypeIconDict.TryGetValue(type, out Texture texture))
            {
                return texture;
            }
            var content = EditorGUIUtility.ObjectContent(component, type);
            if (content != null && content.image != null)
            {
                m_TypeIconDict[type] = content.image;
            }
            return Texture2D.grayTexture;
        }
    }

    public struct IconData
    {
        public Component component;
        public int index;
        public Rect rect;
        public Texture icon;
    }

}
