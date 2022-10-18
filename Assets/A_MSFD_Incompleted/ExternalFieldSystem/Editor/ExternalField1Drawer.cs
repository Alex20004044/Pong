/*using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//[CustomPropertyDrawer(typeof(ExternalField1<>))]
public class ExternalField1Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
             
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        SerializedProperty targetGO_sp = property.FindPropertyRelative("targetGO");
        GameObject targetGO = targetGO_sp.objectReferenceValue as GameObject;
        EditorGUILayout.PropertyField(targetGO_sp);

        SerializedProperty targetComponent_sp = property.FindPropertyRelative("targetComponent");
        Component targetComponent = targetComponent_sp.objectReferenceValue as Component;
        #region Choose Component
        // get the object's components
        Component[] components = targetGO.GetComponents<Component>();

        // get the index of the currently selected component
        int componentIndex = 0;
        if (components.Length > 0 && targetComponent != null && System.Array.IndexOf(components, targetComponent) >= 0)
            componentIndex = System.Array.IndexOf(components, targetComponent);

        // make a list of names to display
        string[] componentsLabels = new string[components.Length];
        for (int i = 0; i < components.Length; i++)
        {
            componentsLabels[i] = i + ": " + components[i].GetType().ToString();
        }

        componentIndex = EditorGUILayout.Popup("Component", componentIndex, componentsLabels);
        if (componentIndex >= 0)
        {
            (property.serializedObject.targetObject as ExternalField1<object>).EditorSetComponent(components[componentIndex]);
            //targetComponent = components[componentIndex];
            //script.SetComponent(components[componentIndex]);
        }
        else
        {
            Debug.LogWarning("Can't find the component anymore, Target Object changed or the component was removed", property.objectReferenceValue);
        }

        #endregion

        //EditorGUILayout.PropertyField(property.FindPropertyRelative("targetComponent"));
        //EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);

        string s = "s";
        s = EditorGUILayout.TextField(s);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
*/