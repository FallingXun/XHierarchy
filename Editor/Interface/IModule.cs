using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace XHierarchy
{
    public interface IModule 
    {
        string Name { get; }

        bool Enabled { get; set; }

        void Init(IConfig config);

        void OnGUIBegin(EditorWindow window);

        Rect OnItemGUI(GameObject go, Rect selectionRect, Rect availableRect);

        Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect);

        void OnGUIEnd(EditorWindow window);

    }
}

