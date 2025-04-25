using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XHierarchy
{
    public class ContentUtils 
    {
        private static GUIContent m_SearchTitleContent;
        public static GUIContent SearchTitleContent
        {
            get
            {
                if(m_SearchTitleContent == null)
                {
                    m_SearchTitleContent = new GUIContent("Customize Search Bar");
                }
                return m_SearchTitleContent;
            }
        }


        private static GUIContent m_AdditionalTitleContent;
        public static GUIContent AdditionalTitleContent
        {
            get
            {
                if (m_AdditionalTitleContent == null)
                {
                    m_AdditionalTitleContent = new GUIContent("Additional Info");
                }
                return m_AdditionalTitleContent;
            }
        }

        private static GUIContent m_ObjectContent;
        public static GUIContent ObjectContent
        {
            get
            {
                if (m_ObjectContent == null)
                {
                    m_ObjectContent = new GUIContent("Object");
                }
                return m_ObjectContent;
            }
        }

        private static GUIContent m_NoteContent;
        public static GUIContent NoteContent
        {
            get
            {
                if (m_NoteContent == null)
                {
                    m_NoteContent = new GUIContent("Note");
                }
                return m_NoteContent;
            }
        }

        private static GUIContent m_IdentifierContent;
        public static GUIContent IdentifierContent
        {
            get
            {
                if (m_IdentifierContent == null)
                {
                    m_IdentifierContent = new GUIContent("Identifier");
                }
                return m_IdentifierContent;
            }
        }

        private static GUIContent m_ApplyBtnContent;
        public static GUIContent ApplyBtnContent
        {
            get
            {
                if (m_ApplyBtnContent == null)
                {
                    m_ApplyBtnContent = new GUIContent("Apply");
                }
                return m_ApplyBtnContent;
            }
        }

        private static GUIContent m_OpenSearchWindowContent;
        public static GUIContent OpenSearchWindowContent
        {
            get
            {
                if (m_OpenSearchWindowContent == null)
                {
                    m_OpenSearchWindowContent = new GUIContent("Open Search Window");
                }
                return m_OpenSearchWindowContent;
            }
        }

        private static GUIContent m_SelectContent;
        public static GUIContent SelectContent
        {
            get
            {
                if (m_SelectContent == null)
                {
                    m_SelectContent = EditorGUIUtility.IconContent("d_scenepicking_pickable-mixed");
                }
                return m_SelectContent;
            }
        }

        private static GUIContent m_CSharpContent;
        public static GUIContent CSharpContent
        {
            get
            {
                if (m_CSharpContent == null)
                {
                    m_CSharpContent = EditorGUIUtility.IconContent("cs Script Icon");
                }
                return m_CSharpContent;
            }
        }


        private static GUIContent m_CreateAddNewContent;
        public static GUIContent CreateAddNewContent
        {
            get
            {
                if (m_CreateAddNewContent == null)
                {
                    m_CreateAddNewContent = EditorGUIUtility.IconContent("CreateAddNew");
                }
                return m_CreateAddNewContent;
            }
        }

        private static GUIContent m_SearchContent;
        public static GUIContent SearchContent
        {
            get
            {
                if(m_SearchContent == null)
                {
                    m_SearchContent = EditorGUIUtility.IconContent("d_Search Icon");
                }
                return m_SearchContent;
            }
        }
    }

}
