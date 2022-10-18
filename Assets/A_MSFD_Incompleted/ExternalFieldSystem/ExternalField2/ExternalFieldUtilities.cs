using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.OdinInspector;
using MSFD;
using System.Xml;
using System;
using Sirenix.Utilities;

public class ExternalFieldUtilities: MonoBehaviour
{
    float _float = 1;
    public float _publicfloat = 2;
    static float _stFloat = 3;
    public static float _stPublicFloat = 4;

    public ClipCore clipCore;

    [Button]
    public void CreateNewClipCore()
    {
        clipCore = new ClipCore();
    }
    public float FuncFloat()
    {
        return 1;
    }
    float PrivateFuncFloat()
    {
        return -1;
    }
    float PrivateFuncFloatparametres(int one)
    {
        
        return -1;
    }

    float PropertFloat { get; set; } = 5;
    public float PublicPropertFloat { get; set; } = 6;
    public float PublicPropertFloatNormal { get { return _float; } set { _float = value; } }
    [Button]
    public void Test()
    {
        var members = GetAllMembers<float>(this);
        string log = "Float:\n";
        foreach(var x in members)
        {
            log += x.Name + "\n";
        }
        Debug.Log(log);


        members = GetAllMembers<Transform>(this);
        log = "Transform:\n";
        foreach (var x in members)
        {
            log += x.Name + "\n";
        }
        Debug.Log(log);
    }
    public static List<MemberInfo> GetAllMembers<T>(object source, bool isReturnNotPrimitiveAnyType = true)
    {
        List<MemberInfo> outMembers = new List<MemberInfo>();

        var members = source.GetType().GetMembers(bflags);
        foreach (MemberInfo x in members)
        {
            if (x.MemberType == MemberTypes.Method)
            {
                MethodInfo mi = x as MethodInfo;
                if (mi.GetParameters().Length == 0 && (mi.ReturnType == typeof(T) || mi.ReturnType.IsSubclassOf(typeof(T))))
                {
                    ObsoleteAttribute obsoleteAttribute = mi.GetCustomAttribute<ObsoleteAttribute>();
                    if (obsoleteAttribute == null)
                        outMembers.Add(x);
                }
            }
            else if (x.MemberType == MemberTypes.Field)
            {
                FieldInfo fi = x as FieldInfo;/*fi.FieldType == typeof(T) || fi.FieldType.IsSubclassOf(typeof(T)) ||*/
                if ((fi.FieldType.IsAssignableFrom(typeof(T)) || (!fi.FieldType.IsPrimitive && isReturnNotPrimitiveAnyType))) 
                {
                    ObsoleteAttribute obsoleteAttribute = fi.GetCustomAttribute<ObsoleteAttribute>();
                    if (obsoleteAttribute == null)
                        outMembers.Add(x);
                }
            }
            else if(x.MemberType == MemberTypes.Property)
            {
                PropertyInfo pi = x as PropertyInfo;
                if ((pi.PropertyType.IsAssignableFrom(typeof(T)) || (!pi.PropertyType.IsPrimitive && isReturnNotPrimitiveAnyType)))
                {
                    ObsoleteAttribute obsoleteAttribute = pi.GetCustomAttribute<ObsoleteAttribute>();
                    if (obsoleteAttribute == null)
                        outMembers.Add(x);
                }
            }
        }
        return outMembers;
    }

    public static object GetMemberObject(object source, string memberName)
    {
        MemberInfo memberInfo;
        return GetMemberObject(source, memberName, out memberInfo);
    }
    /// <summary>
    /// Return null if object not found or if object is method
    /// </summary>
    /// <param name="source"></param>
    /// <param name="memberName"></param>
    /// <param name="memberInfo"></param>
    /// <returns></returns>
    public static object GetMemberObject(object source, string memberName, out MemberInfo memberInfo)
    {
        memberInfo = null;
        object val = null;

        if (source != null && memberName != null)
        {
            memberInfo = source.GetType().GetProperty(memberName, bflags);
            if (memberInfo != null)
            {
                val = source.GetType().GetProperty(memberName, bflags).GetValue(source, null);
            }
            else
            {
                memberInfo = source.GetType().GetField(memberName, bflags);
                if(memberInfo != null)
                    val = source.GetType().GetField(memberName, bflags).GetValue(source);
                else
                {
                    memberInfo = source.GetType().GetMethod(memberName, bflags);
                }
            }
        }
        return val;
    }
    public static object GetMemberObjectByPath(object source, string path, out MemberInfo memberInfo, out object parent)
    {
        List<string> names = new List<string>(path.Split('/'));
        parent = null;
        memberInfo = null;
        foreach(var x in names)
        {
            parent = source;
            source = GetMemberObject(source, x, out memberInfo);
        }
        return source;
    }

    public const BindingFlags bflags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static 
        | BindingFlags.NonPublic;// | BindingFlags.DeclaredOnly;
}
