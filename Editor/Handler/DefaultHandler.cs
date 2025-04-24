using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XHierarchy
{
    public class DefaultHandler : IHandler
    {
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
    }

}
