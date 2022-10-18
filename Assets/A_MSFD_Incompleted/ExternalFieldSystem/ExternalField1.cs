using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.Utilities;

[System.Serializable]
public class ExternalField1<T>
{
#if UNITY_EDITOR
    public GameObject targetGO;
#endif
    public Component targetComponent;
    [ShowInInspector]
    [SerializeField]
    List<string> fullProperty = new List<string>();
    [SerializeField]
    object memberParent;
    [SerializeField]
    MemberInfo member;
    [SerializeField]
    bool isInitialized = false;

    [Button]
    public object GetRawValue()
    {
        if(!isInitialized)
        {
            FieldUtilities.GetMemberByPath(targetComponent, fullProperty, out memberParent, out member);
            /*object obj = */
            /*if (obj is T)
                value = (T)obj;*/
            isInitialized = true;
        }
        return RawValueAccess();
    }

    object RawValueAccess()
    {
        if(member.MemberType == MemberTypes.Method)
        {
            return (member as MethodInfo).Invoke(memberParent, null);
        }
        else
            return member.GetMemberValue(memberParent);
        /*else if(member.DeclaringType.IsValueType)*/
    }
    [Button]
    public T GetValue()
    {
        return (T)GetRawValue();
    }
    [Button]
    public void DebugLogObject()
    {
        Debug.Log(GetValue().ToString());
    }
    [Button]
    public void SetPropertyPath(List<string> path)
    {
        fullProperty = path;
        isInitialized = false;
    }
    public void SetPropertyPath(int propertyNesting, string value)
    {
        if (fullProperty.Count <= propertyNesting)
        {
            fullProperty.Add(value);
        }
        else
        {
            if (fullProperty[propertyNesting] == value)
            {
                return;
            }
            else
            {
                fullProperty[propertyNesting] = value;
                fullProperty.RemoveRange(propertyNesting + 1, fullProperty.Count - 1 - propertyNesting);
            }
        }
    }
    public string GetPropertyAtIndex(int index)
    {
        if (index < fullProperty.Count)
            return fullProperty[index];
        else
            return "";
    }
    public void EditorSetComponent(Component component)
    {
        targetComponent = component;
    }
}
