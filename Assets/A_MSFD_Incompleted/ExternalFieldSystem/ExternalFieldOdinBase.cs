using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Sirenix.Utilities;
using UnityEditor;
using OikosTools;

[System.Serializable]
public class ExternalFieldOdinBase
{

    public GameObject targetGO;

    [ValueDropdown(nameof(GetTargetComponents))]
    [SerializeField]
    Component targetComponent;
    [CustomValueDrawer(nameof(MyCustomDrawerInstance))]
    //[ValueDropdown(nameof(VDTest))]
    [ShowInInspector]
    [SerializeField]
    List<string> fullProperty = new List<string>();
    [ValueDropdown(nameof(GetMembersPath))]
    [SerializeField]
    string memberPath;

    [ValueDropdown(nameof(VD_Member_0))]
    [SerializeField]
    string member_0;
    [ValueDropdown(nameof(VD_Member_1))]
    [SerializeField]
    string member_1;

    [SerializeField]
    int nestLevel = 1;

    [SerializeField]
    bool isInitialized = false;

    object memberParent;
    MemberInfo member;
    [Button]
    public object GetRawValue()
    {
        if (!isInitialized)
        {
            FieldUtilities.GetMemberByPath(targetComponent, fullProperty, out memberParent, out member);
            isInitialized = true;
        }
        return RawValueAccess();
    }
    object RawValueAccess()
    {
        if (member.MemberType == MemberTypes.Method)
        {
            return (member as MethodInfo).Invoke(memberParent, null);
        }
        else
            return member.GetMemberValue(memberParent);
    }
    [Button]
    public void DebugLogObject()
    {
        Debug.Log(GetRawValue().ToString());
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
    private ValueDropdownList<Component> GetTargetComponents()
    {
        if (targetGO != null)
        {
            Component[] components = targetGO.GetComponents<Component>();
            ValueDropdownList<Component> valueDropdownList = new ValueDropdownList<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                string label = i + ": " + components[i].GetType().ToString();
                ValueDropdownItem<Component> valueDropdownItem = new ValueDropdownItem<Component>(label, components[i]);
                valueDropdownList.Add(valueDropdownItem);
            }
            return valueDropdownList;
        }
        else
        {
            return null;
        }
    }
    ValueDropdownList<string> VDTest()
    {
        ValueDropdownList<string> members = new ValueDropdownList<string>();
        foreach (var path in "STRING")
        {
            members.Add(path.ToString());
        }
        return members;
    }
    ValueDropdownList<string> GetMembersPath()
    {
        ValueDropdownList<string> members = new ValueDropdownList<string>();
        if(nestLevel <= 0)
        {
            memberPath = "";
            members.Add("", "None");
        }
        List<string> paths = GetAllNestedMembersPaths(targetComponent, nestLevel - 1);
        for (int i = 0; i < nestLevel; i++)
        { 
            members.Add(paths[i]);
        }

        return members;
    }
    object[] GetNestedMembersValues(object source)
    {
        List<MemberInfo> members = new List<MemberInfo>( source.GetType().GetMembers(FieldUtilities.bflags));// source.GetType().GetMembers(FieldUtilities.bflags);
        List<object> memObjs = new List<object>();

        MethodInfo[] methods = source.GetType().GetMethods(FieldUtilities.bflags);
        foreach(var x in methods)
        {
            if( x.GetParameters().Length == 0 && x.ReturnType == typeof(object))
            {
                //members.Add(x);
                memObjs.Add(x);
            }
        }

        for (int i = 0; i < members.Count; i++)
        {
            if(members[i].MemberType == MemberTypes.Method)
            {
                //members[i].
            }

            else if (members[i].MemberType == MemberTypes.Field || members[i].MemberType == MemberTypes.Property)
            {
                try
                {
                    object member = members[i].GetMemberValue(source);
                    if (member == null)
                    {

                    }
                    else
                    {
                        memObjs.Add(member);
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        return memObjs.ToArray();
    }

    List<string> GetAllNestedMembersPaths(object source, int level)
    {
        List<string> paths = new List<string>();
        object[] nestedMembers = GetNestedMembersValues(source);
        if(source.GetType().IsValueType)
        {
            paths.Add(source.GetType().Name);
        }
        else if (level == 0)
        {
            foreach (var name in nestedMembers)
            {
                paths.Add(source.GetType().Name + "/" + name);
            }
        }
        else
        {
            foreach (var x in nestedMembers)
            {
                foreach (var name in GetAllNestedMembersPaths(x, level - 1))
                {
                    paths.Add(source.GetType().Name + "/" + name);
                }
            }
        }
        return paths;
    }
    private string MyCustomDrawerInstance(string value, GUIContent label)
    {
#if UNITY_EDITOR
        return EditorGUILayout.TextField(label, value);//, this.From, this.To);
#endif
        return null;
    }

    private ValueDropdownList<string> VD_Member_0()
    {
        ValueDropdownList<string> dropdownItems = new ValueDropdownList<string>();
        if(targetComponent == null)
        {
            dropdownItems.Add("None", null);
        }
        else
        {
            DisplaySubProperty(targetComponent, 0);
            object[] members = GetNestedMembersValues(targetComponent);
            foreach(var x in members)
            {
                dropdownItems.Add( x.GetType().Name + ": (" + x.GetType().Name + ")", x.GetType().Name);
            }
        }
        return dropdownItems;
    }
    object DisplaySubProperty(object currentProperty, int index)
    {
#if UNITY_EDITOR
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
            int propertyIndex = EditorGUILayout.Popup("Property", properties.IndexOf(GetPropertyAtIndex(index)), propertiesLabels);
            if (propertyIndex >= 0)
            {
                //SetPropertyAtIndex(index, properties[propertyIndex]);
            }

            if (propertyIndex <= 0)
                return null;
            else
                return GetProperty(currentProperty, properties[propertyIndex]);
        }
        else
#endif
            return null;
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
    private ValueDropdownList<string> VD_Member_1()
    {
        ValueDropdownList<string> dropdownItems = new ValueDropdownList<string>();
        if (member_0 == null)
        {
            dropdownItems.Add("None", null);
        }
        else
        {
            MemberInfo memberInfo;
            object source = targetComponent.GetType().GetMember(member_0, FieldUtilities.bflags).GetType();//FieldUtilities.GetMemberValueFromSource(targetComponent, member_0, out memberInfo);
            object[] members = GetNestedMembersValues(source);
            foreach (var x in members)
            {
                dropdownItems.Add(x.GetType().Name, x.GetType().Name);
            }
        }
        return dropdownItems;
    }
}
