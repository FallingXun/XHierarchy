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

        void Init();

        void OnItemGUI(GameObject go, Rect rect);

        void OnSceneGUI(Scene scene, Rect rect);
    }
}

