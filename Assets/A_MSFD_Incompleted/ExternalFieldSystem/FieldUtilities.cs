using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.Utilities;

public static class FieldUtilities
{
    public static object GetMemberByPath(object root, List<string> fullPropertyPath, out object memberParent, out MemberInfo memberInfo)
    {
        memberParent = root;
        object currentProperty = root;
        memberInfo = null;
        for (int i = 0; i < fullPropertyPath.Count; i++)
        {
            if(i == fullPropertyPath.Count-2)
            {
                memberParent = currentProperty;
            }
            currentProperty = GetMemberValueFromSource(currentProperty, fullPropertyPath[i], out memberInfo);
        }
        return currentProperty;
    }
    
    public static object GetMemberValueFromSource(object source, string propertyName, out MemberInfo memberInfo)
    {
        object val = null;
        memberInfo = null;
        if (source != null && propertyName != null)
        {
            memberInfo = source.GetType().GetMember(propertyName, bflags)[0];

            if (source.GetType().GetProperty(propertyName, bflags) != null)
                val = source.GetType().GetProperty(propertyName, bflags).GetValue(source, null);
            if (source.GetType().GetField(propertyName, bflags) != null)
                val = source.GetType().GetField(propertyName, bflags).GetValue(source);
            if (source.GetType().GetMethod(propertyName, bflags) != null)
                val = source.GetType().GetMethod(propertyName, bflags);
        }
        return val;
    }
    public const BindingFlags bflags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
}
