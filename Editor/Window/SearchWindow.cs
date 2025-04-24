using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.SceneManagement;

namespace XHierarchy
{
    public class SearchWindow : EditorWindow
    {
        private Dictionary<GameObject, string> m_NoteDict = new Dictionary<GameObject, string>();
        private Dictionary<GameObject, string> m_IdentifierDict = new Dictionary<GameObject, string>();
        private Stack<Transform> m_Stack = new Stack<Transform>();

        private IConfig m_Config;

        private GUIStyle[] m_Styles;
        private GUIContent[] m_Contents;
        private Dictionary<GameObject, string>[] m_Dicts;
        private int m_SelectIndex = 0;
        private string m_SearchFilter = "";
        private Vector2 m_ScrollPosition = Vector2.zero;
        private PrefabStage m_LastStage = null;
        private List<GameObject> m_Roots = new List<GameObject>();

        public static void Create(Vector2 position, IConfig config)
        {
            var window = GetWindow<SearchWindow>();
            window.position = Rect.zero.SetPosition(position).SetWidth(514).SetHeight(500);
            window.titleContent = ContentUtils.SearchTitleContent;
            window.Init(config);
        }

        private void Init(IConfig config)
        {
            m_Styles = new GUIStyle[] { StyleUtils.NoteButon, StyleUtils.IdentifierButon };
            m_Contents = new GUIContent[] { ContentUtils.NoteContent, ContentUtils.IdentifierContent };
            m_Dicts = new Dictionary<GameObject, string>[] { m_NoteDict, m_IdentifierDict };

            m_Config = config;

        }

        private void OnGUI()
        {
            if (m_Config == null)
            {
                Close();
                return;
            }
            Collect();
            EditorGUILayout.BeginHorizontal();
            m_SearchFilter = EditorGUILayout.TextField(m_SearchFilter, StyleUtils.SearchTextField);
            m_SelectIndex = EditorGUILayout.Popup(m_SelectIndex, m_Contents, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            foreach (var item in m_Dicts[m_SelectIndex])
            {
                if (string.IsNullOrEmpty(m_SearchFilter) || item.Value.Contains(m_SearchFilter))
                {

                    EditorGUILayout.BeginHorizontal(StyleUtils.GroupBox);
                    EditorGUILayout.LabelField(item.Key.name, GUILayout.Width(200));
                    EditorGUILayout.LabelField(item.Value, m_Styles[m_SelectIndex], GUILayout.Width(250));
                    if (GUILayout.Button(ContentUtils.SelectContent, StyleUtils.IconButton))
                    {
                        Selection.activeGameObject = item.Key;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void Collect()
        {
            var stage = PrefabStageUtility.GetCurrentPrefabStage();
            if (m_LastStage != null && stage == m_LastStage)
            {
                return;
            }
            m_NoteDict.Clear();
            m_IdentifierDict.Clear();
            m_Roots.Clear();
            m_Stack.Clear();
            if (stage != null)
            {
                m_Roots.Add(stage.prefabContentsRoot);

            }
            else
            {
                var scene = SceneManager.GetActiveScene();
                if (scene != null)
                {
                    scene.GetRootGameObjects(m_Roots);
                }
            }
            for (int i = m_Roots.Count - 1; i >= 0; i--)
            {
                m_Stack.Push(m_Roots[i].transform);
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

        private void LoadAdditionalData(GameObject go)
        {
            if (m_Config == null || go == null)
            {
                return;
            }
            var note = m_Config.Handler.GetNote(go);
            if (string.IsNullOrEmpty(note) == false)
            {
                m_NoteDict[go] = note;
            }
            var identifier = m_Config.Handler.GetIdentifier(go);
            if (identifier > 0)
            {
                m_IdentifierDict[go] = identifier.ToString();
            }
        }
    }
}

