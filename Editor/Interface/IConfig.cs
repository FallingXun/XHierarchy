using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace XHierarchy
{
    public interface IConfig 
    {
        void Init();
        
        List<IModule> Modules { get; }

        /// <summary>
        /// 需要处理的 Component 类型
        /// </summary>
        List<Type> ComponentTypes { get; }


        IHandler Handler { get; }
    }

}
