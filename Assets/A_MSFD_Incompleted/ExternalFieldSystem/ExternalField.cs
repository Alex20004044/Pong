
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ExternalField : MonoBehaviour
{
    public GameObject target;
    public Component selectedComponent;
    public object value;

    [SerializeField]
    List<string> fullProperty = new List<string>();

    public void AddPoperty(string propertyName)
    {
        fullProperty.Add(propertyName);
    }
    public string GetPropertyAtIndex(int index)
    {
        if (index < fullProperty.Count)
            return fullProperty[index];
        else
            return "";
    }
    public void SetPropertyAtIndex(int index, string value)
    {
        if (fullProperty.Count <= index)
        {
            fullProperty.Add(value);
        }
        else
        {
            if (fullProperty[index] == value)
            {
                return;
            }
            else
            {
                fullProperty[index] = value;
                fullProperty.RemoveRange(index + 1, fullProperty.Count - 1 - index);
            }
        }
    }
    public void SetComponent(Component component)
    {

        if(selectedComponent != component)
        {
            Debug.Log(nameof(SetComponent));
            fullProperty = new List<string>();
            fullProperty.Add("");
            selectedComponent = component;
        }
    }
    [Button]
    public void DisplayValue()
    {

        value = GetProperty(selectedComponent, fullProperty);
        Debug.Log(value.ToString());
    }
    public static object GetProperty(object root, List<string> fullPropertyPath)
    {
        object currentProperty = root;

        for(int i =0; i < fullPropertyPath.Count; i++)
        {
            if (fullPropertyPath[i] == "")
                break;
            currentProperty = GetProperty(root, fullPropertyPath[i]);
        }
        return currentProperty;
    }
    private const System.Reflection.BindingFlags bflags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
    static object GetProperty(object source, string propertyName)
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
