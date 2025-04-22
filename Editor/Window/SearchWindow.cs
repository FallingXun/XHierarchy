using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace XHierarchy
{
    public class SearchWindow : EditorWindow
    {
        private static readonly GUIContent m_TitleContent = ContentUtils.SearchTitleContent;
        private static readonly GUIContent m_NoteContent = ContentUtils.NoteContent;
        private static readonly GUIContent m_IdentifierContent = ContentUtils.IdentifierContent;
        private static readonly GUIContent m_SelectContent = ContentUtils.SelectContent;


        private Dictionary<GameObject, string> m_NoteDict = new Dictionary<GameObject, string>();
        private Dictionary<GameObject, string> m_IdentifierDict = new Dictionary<GameObject, string>();
        private Stack<Transform> m_Stack = new Stack<Transform>();

        private IConfig m_Config;
        private GameObject[] m_Roots;

        private GUIContent[] m_Contents;
        private Dictionary<GameObject, string>[] m_Dicts;
        private int m_SelectIndex = 0;
        private string m_SearchFilter = "";
        private Vector2 m_ScrollPosition = Vector2.zero;

        public static void Create(Vector2 position, IConfig config)
        {
            var window = GetWindow<SearchWindow>();
            window.position = Rect.zero.SetPosition(position).SetWidth(500).SetHeight(500);
            window.titleContent = m_TitleContent;
            window.Init(config);
        }

        private void Init(IConfig config)
        {
            m_Contents = new GUIContent[] { m_NoteContent, m_IdentifierContent };
            m_Dicts = new Dictionary<GameObject, string>[] { m_NoteDict, m_IdentifierDict };

            m_Config = config;
            m_NoteDict.Clear();
            m_IdentifierDict.Clear();
            m_Stack.Clear();
            var scene = SceneManager.GetActiveScene();
            m_Roots = scene.GetRootGameObjects();
            if (m_Roots != null)
            {
                for (int i = m_Roots.Length - 1; i >= 0; i--)
                {
                    m_Stack.Push(m_Roots[i].transform);
                }
            }
            while (m_Stack.Count > 0)
            {
                var tf = m_Stack.Pop();
                LoadAdditionalData(tf.gameObject);
                for (int i = tf.childCount - 1; i >= 0; i--)
                {
                    m_Stack.Push(tf.GetChild(i));
                }
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            m_SearchFilter = EditorGUILayout.TextField(m_SearchFilter);
            m_SelectIndex = EditorGUILayout.Popup(m_SelectIndex, m_Contents);
            EditorGUILayout.EndHorizontal();

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            foreach (var item in m_Dicts[m_SelectIndex])
            {
                if (item.Value.Contains(m_SearchFilter))
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(item.Key.name, GUILayout.Width(150));
                    EditorGUILayout.LabelField(item.Value, GUILayout.Width(250));
                    if (GUILayout.Button(m_SelectContent,StyleUtils.IconButton))
                    {
                        Selection.activeGameObject = item.Key;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView(); 
        }

        private void LoadAdditionalData(GameObject go)
        {
            if (m_Config == null || go == null)
            {
                return;
            }
            if (PrefabUtility.IsPartOfPrefabInstance(go) == false)
            {
                return;
            }
            var note = m_Config.NoteFunc(go);
            if (string.IsNullOrEmpty(note) == false)
            {
                m_NoteDict[go] = note;
            }
            var identifier = m_Config.IdentifierFunc(go);
            if (identifier > 0)
            {
                m_IdentifierDict[go] = identifier.ToString();
            }
        }
    }
}

