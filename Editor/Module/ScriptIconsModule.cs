using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using System.Reflection;

namespace XHierarchy
{
    public class ScriptIconsModule : IModule
    {
        private List<Component> m_ComponentList = new List<Component>();
        private List<IconData> m_IconDataList = new List<IconData>();
        private Dictionary<Type, int> m_TypeDict = new Dictionary<Type, int>();
        private Dictionary<Type, Texture> m_TypeIconDict = new Dictionary<Type, Texture>();
        private Texture m_DefaultIcon = null;
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
                if (GUI.Button(iconData.rect, new GUIContent(iconData.icon), StyleUtils.IconButton))
                {
                    var position = EditorGUIUtility.GUIToScreenPoint(new Vector2(iconData.rect.xMax, iconData.rect.y));
                    ComponentWindow.Create(position, iconData.component, iconData.icon);
                }
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
                byte[] rawData = (content.image as Texture2D).GetRawTextureData();
                for (int i = 0; i < rawData.Length; i++)
                {
                    if (i > 0 && i % 3 == 0)
                    {
                        continue;
                    }
                    rawData[i] = (byte)(rawData[i] * 0.7f);
                }
                Texture2D newTexture = new Texture2D(content.image.width, content.image.height);
                newTexture.LoadRawTextureData(rawData);
                newTexture.Apply();
                m_TypeIconDict[type] = newTexture;
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
