using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XHierarchy
{
    public interface IData 
    {
        List<IModule> GetModules();

        Vector2 GetAvailableRange();
    }

}

