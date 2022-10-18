using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace MSFD
{
    public class ExtendedEditorWindow : EditorWindow
    {
        protected SerializedObject serializedObject;
        protected SerializedProperty currentProperty;

        private string selectedPropertyPath;
        protected SerializedProperty selectedProperty;
        protected void DrawObject(SerializedObject obj)
        {
            foreach (SerializedProperty p in obj.GetIterator())
            {
                DrawProperties(p, true);
            }
        }
        protected void DrawProperties(SerializedProperty prop, bool drawChildren)
        {
            if (prop.hasVisibleChildren)
            {
                foreach (SerializedProperty p in prop)
                {
                    if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
                    {
                        EditorGUILayout.BeginHorizontal();
                        p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                        EditorGUILayout.EndHorizontal();

                        if (p.isExpanded)
                        {
                            EditorGUI.indentLevel++;
                            DrawProperties(p, drawChildren);
                            EditorGUI.indentLevel--;
                        }
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(p, drawChildren);
                    }
                }
            }
            else
            {

                EditorGUILayout.PropertyField(prop, drawChildren);
            }

        }
        public static class BetterPropertyField
        {
            /// <summary>
            /// Draws a serialized property (including children) fully, even if it's an instance of a custom serializable class.
            /// Supersedes EditorGUILayout.PropertyField(serializedProperty, true);
            /// </summary>
            /// <param name="_serializedProperty">Serialized property.</param>
            public static void DrawSerializedProperty(SerializedProperty _serializedProperty)
            {
                if (_serializedProperty == null)
                {
                    EditorGUILayout.HelpBox("SerializedProperty was null!", MessageType.Error);
                    return;
                }
                var serializedProperty = _serializedProperty.Copy();
                int startingDepth = serializedProperty.depth;
                EditorGUI.indentLevel = serializedProperty.depth;
                DrawPropertyField(serializedProperty);

                //!! Becaouse some troubles with iteration
                if (!serializedProperty.isExpanded)
                {
                    return;
                }
                

                    while (serializedProperty.NextVisible(serializedProperty.isExpanded && !PropertyTypeHasDefaultCustomDrawer(serializedProperty.propertyType)) && serializedProperty.depth > startingDepth)
                    {
                        EditorGUI.indentLevel = serializedProperty.depth;
                        DrawPropertyField(serializedProperty);
                    }
            }

            static void DrawPropertyField(SerializedProperty serializedProperty)
            {
                if (serializedProperty.propertyType == SerializedPropertyType.Generic)
                {
                    serializedProperty.isExpanded = EditorGUILayout.Foldout(serializedProperty.isExpanded, serializedProperty.displayName, true);
                }
                else
                {
                    EditorGUILayout.PropertyField(serializedProperty);
                }
            }

            static bool PropertyTypeHasDefaultCustomDrawer(SerializedPropertyType type)
            {
                return
                type == SerializedPropertyType.AnimationCurve ||
                type == SerializedPropertyType.Bounds ||
                type == SerializedPropertyType.Color ||
                type == SerializedPropertyType.Gradient ||
                type == SerializedPropertyType.LayerMask ||
                type == SerializedPropertyType.ObjectReference ||
                type == SerializedPropertyType.Rect ||
                type == SerializedPropertyType.Vector2 ||
                type == SerializedPropertyType.Vector3;
            }
        }
        protected void DrawSidebar(SerializedProperty prop)
        {
            foreach(SerializedProperty p in prop)
            {
                if(GUILayout.Button(p.displayName))
                {
                    selectedPropertyPath = p.propertyPath;
                }
            }
            if(!string.IsNullOrEmpty(selectedPropertyPath))
            {
                selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
            }
        }
    }
}

        /*foreach (SerializedProperty p in prop)
                {
                    if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
                    {
                        EditorGUILayout.BeginHorizontal();
                        p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                        EditorGUILayout.EndHorizontal();

                        if (p.isExpanded)
                        {
                            EditorGUI.indentLevel++;
                            DrawProperties(p, drawChildren);
                            EditorGUI.indentLevel--;
                        }
                    }
                    else
                    {
                        // if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath))
                        // {
                        //    continue;
                        //}
                        //lastPropPath = p.propertyPath;
                        EditorGUILayout.PropertyField(p, drawChildren);
                    }
                }
        */
    
