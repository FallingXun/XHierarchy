using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace XHierarchy
{
    public interface IConfig 
    {

        List<IModule> Modules { get; }

        /// <summary>
        /// 获取 Item 名字后从左边起可用的偏移量
        /// </summary>
        float GameObjectGUILeftOffset { get; }

        /// <summary>
        /// 获取 Item 名字后从右边起可用的偏移量
        /// </summary>
        float GameObjectGUIRightOffset { get; }

        /// <summary>
        /// 需要处理的 Component 类型
        /// </summary>
        List<Type> HandleComponentTypes { get; }
    }

}
