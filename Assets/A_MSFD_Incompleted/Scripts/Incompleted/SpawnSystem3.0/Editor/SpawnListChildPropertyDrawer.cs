using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
namespace MSFD.SpawnSystem
{
    [CustomPropertyDrawer(typeof(SpawnListChild), true)]
    public class SpawnListChildPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            if (property.objectReferenceValue == null)
            {
                EditorGUI.LabelField(position, "Its a child");

                base.OnGUI(position, property, label);
            }

            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 20;
        }
    }
}*/