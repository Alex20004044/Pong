/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OikosTools;
using System.Reflection;
[CustomEditor(typeof(ExternalField1<>))]
public class ExternalField1Editor : Editor
{
    ExternalField1<object> script;
    void OnEnable()
    {
        script = (ExternalField1<object>)target;
    }
    public override void OnInspectorGUI()
    {
        #region Init
        script.targetGO = (GameObject)EditorGUILayout.ObjectField(script.targetGO, typeof(GameObject), true);
        GameObject theTarget = script.targetGO;
        if (script.targetGO == null)
        {
            EditorGUILayout.HelpBox("No Target Object defined, using itself.", MessageType.Info);
            theTarget = script.gameObject;
        }
        #endregion
        #region Choose Component
        // get the object's components
        Component[] components = theTarget.GetComponents<Component>();

        // get the index of the currently selected component
        int componentIndex = 0;
        if (components.Length > 0 && script.targetComponent != null && System.Array.IndexOf(components, script.targetComponent) >= 0)
            componentIndex = System.Array.IndexOf(components, script.targetComponent);

        // make a list of names to display
        string[] componentsLabels = new string[components.Length];
        for (int i = 0; i < components.Length; i++)
        {
            componentsLabels[i] = i + ": " + components[i].GetType().ToString();
        }

        componentIndex = EditorGUILayout.Popup("Component", componentIndex, componentsLabels);
        if (componentIndex >= 0)
        {
            script.EditorSetComponent(components[componentIndex]);
        }
        else
        {
            Debug.LogWarning("Can't find the component anymore, Target Object changed or the component was removed", this);
        }

        #endregion

        object currentProperty = script.targetComponent;
        int cycleIndex = 0;
        //Cycle
        while (true)
        {
            object subProperty = DisplaySubProperty(currentProperty, cycleIndex);
            if (subProperty == null)
            {
                break;
            }
            else
            {
                currentProperty = subProperty;
                cycleIndex++;
            }
        }
    }
    object DisplaySubProperty(object currentProperty, int index)
    {
        List<string> properties = PropertyChange.GetProperties(currentProperty);
        string[] propertiesLabels = new string[properties.Count + 1];
        if (properties.Count > 0)
        {
            #region Define Property Labels
            properties.Insert(0, "");
            propertiesLabels[0] = "None";
            for (int i = 1; i < properties.Count; i++)
            {
                string propertyLabelName;
                if (PropertyChange.GetValue(currentProperty, properties[i]) == null)
                {
                    propertyLabelName = "object";
                }
                else
                {
                    propertyLabelName = PropertyChange.GetValue(currentProperty, properties[i]).GetType().Name;
                }

                propertiesLabels[i] = string.Format("{0} ({1})", properties[i], propertyLabelName);
            }
            #endregion
            int propertyIndex = EditorGUILayout.Popup("Property", properties.IndexOf(script.GetPropertyAtIndex(index)), propertiesLabels);
            if (propertyIndex >= 0)
            {
                script.SetPropertyPath(index, properties[propertyIndex]);
            }

            if (propertyIndex <= 0)
                return null;
            else
                return GetProperty(currentProperty, properties[propertyIndex]);
        }
        else
        {
            return null;
        }
    }

    private const System.Reflection.BindingFlags bflags = BindingFlags.Public | BindingFlags.Instance;
    public static object GetProperty(object source, string propertyName)
    {
        object val = null;
        if (source != null && propertyName != null)
        {
            if (source.GetType().GetProperty(propertyName, bflags) != null)
                val = source.GetType().GetProperty(propertyName, bflags).GetValue(source, null);
            if (source.GetType().GetField(propertyName, bflags) != null)
                val = source.GetType().GetField(propertyName, bflags).GetValue(source);
        }
        return val;
    }
}
*/