using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XHierarchy
{
    public class Template 
    {
        public static readonly string CustomConfigData = @"
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XHierarchy
{
    public class CustomConfigData : ScriptableObject, IConfig
    {
        private List<Type> m_ComponentTypes = new List<Type>()
        {
            typeof(RectTransform),
            typeof(Canvas),
            typeof(CanvasGroup),
            typeof(Image),
            typeof(RawImage),
            typeof(Text),
            typeof(Outline),
            typeof(Shadow),
            typeof(Button),
            typeof(InputField),
            typeof(ScrollRect),
            typeof(GridLayoutGroup),
            typeof(HorizontalLayoutGroup),
            typeof(VerticalLayoutGroup),
            typeof(Toggle),
            typeof(ToggleGroup),
            typeof(ContentSizeFitter),
            typeof(Mask),

        };

        public List<Type> ComponentTypes => m_ComponentTypes;

        public string GetNote(GameObject go)
        {
            return null;
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

        }
    }
}
";
    }

}
