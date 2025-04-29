using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XHierarchy
{
    public class AdditionalDataRecorder : MonoBehaviour
    {
#if UNITY_EDITOR
        public string Note;

        public int Identifier;

#endif
    }
}
