using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XHierarchy
{
    public interface IModule 
    {
        string Name { get; }

        bool Enabled { get; set; }

        void Init(IConfig config);

        Rect OnItemGUI(GameObject go, Rect selectionRect, Rect availableRect);

        Rect OnSceneGUI(Scene scene, Rect selectionRect, Rect availableRect);
    }
}

