using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace XHierarchy
{
    public interface IConfig 
    {
        void Init();

        /// <summary>
        /// 需要处理的 Component 类型
        /// </summary>
        List<Type> ComponentTypes { get; }

        /// <summary>
        /// 获取 Item 名字后可用的范围（左偏移，右偏移）
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        Vector2 GetItemGUIRange(GameObject go);

        void SetItemGUIRange(GameObject go, Vector2 range);

        string GetNote(GameObject go);

        void SetNote(GameObject go, string note);

        int GetIdentifier(GameObject go);

        void SetIdentifier(GameObject go, int identifier);
    }

}
