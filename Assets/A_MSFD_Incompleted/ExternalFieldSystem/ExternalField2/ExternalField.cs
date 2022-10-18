using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Reflection;
using Sirenix.Utilities;
using System.Text;
using System.CodeDom;

namespace MSFD
{
    //[InlineProperty]
    [System.Serializable]
    public class ExternalField<T>
    {
        //[ShowIf("@" + nameof(infoBox) + "!= \"\"")]
        [InfoBox("$" + nameof(infoBox), InfoMessageType = InfoMessageType.Error, VisibleIf = "@" + nameof(infoBox) + "!= \"\"")]
        [LabelText("@" + nameof(ValueLable)+"()")]
        [ShowInInspector]
        string currentPathEditor;
        string infoBox;

        [OnValueChanged("@" + nameof(OnChangedProperty) + "(-2)")]
        [FoldoutGroup(serviceGroupName, GroupName = "@" + nameof(ServiceGroupName) + "()")]
        [SerializeField]
        GameObject targetGO;
        [FoldoutGroup(serviceGroupName)]
        [OnValueChanged("@" + nameof(OnChangedProperty) + "(-1)")]
        [ValueDropdown(nameof(GetTargetComponents))]
        [SerializeField]
        Component targetComponent;

        [FoldoutGroup(serviceGroupName)]
        [ShowIf("@targetComponent != null")]
        [OnValueChanged("@" + nameof(OnChangedProperty) + "(0)")]
        [ValueDropdown(nameof(GetProperty_0))]
        [SerializeField]
        string property_0 = "";

        [FoldoutGroup(serviceGroupName)]
        [ShowIf("@" + nameof(IsShowProperty_1) + "()")]
        [OnValueChanged("@" + nameof(OnChangedProperty) + "(1)")]
        [ValueDropdown(nameof(GetProperty_1))]
        [SerializeField]
        string property_1 = "";


        [FoldoutGroup(serviceGroupName)]
        [ShowIf("@" + nameof(IsShowProperty_2) + "()")]
        [OnValueChanged("@" + nameof(OnChangedProperty) + "(2)")]
        [ValueDropdown(nameof(GetProperty_2))]
        [SerializeField]
        string property_2 = "";

        MemberInfo targetMemberInfo;
        object targetParent;

        Func<T> method = null;


        int propertyLevel = -1;
        public void Initialize()
        {
            DefineTargetParentAndMemberInfo();
        }
        [FoldoutGroup(serviceGroupName)]
        [Button]
        public T GetValue()
        {
            return (T)GetValueWithoutCast();
        }
        [FoldoutGroup(serviceGroupName)]
        [Button]
        object GetValueWithoutCast()
        {
            if (property_0.IsNullOrWhitespace())
            {
                return targetComponent;
            }
            else
            {
                if (targetMemberInfo == null)
                {
                    DefineTargetParentAndMemberInfo();
                    if(targetMemberInfo == null)
                    {
                        return null;
                    }
                }
                if (targetMemberInfo.MemberType != MemberTypes.Method)
                {
                    return targetMemberInfo.GetMemberValue(targetParent);
                }
                else
                {
                    return method.Invoke();
                }
            }
        }

        /// <summary>
        /// Also this func init metod delegate if target is method
        /// </summary>
        [Button]
        void DefineTargetParentAndMemberInfo()
        {
            //ALARM!!! Dirty code
#if UNITY_EDITOR
            if (targetComponent == null)
            {
                return;
            }
#endif

            DefinePropertyLevel();
            string path = GetPath();
            //ALARM!!! Dirty code
#if UNITY_EDITOR
            if (path.IsNullOrWhitespace())
            {
                ExternalFieldUtilities.GetMemberObject(targetGO, targetComponent.ToString(), out targetMemberInfo);
                return;
            }
#endif
            ExternalFieldUtilities.GetMemberObjectByPath(targetComponent, path, out targetMemberInfo, out targetParent);
            if (targetMemberInfo != null)
            {
                //Init Delegate if it is necessary;
                if (targetMemberInfo.MemberType == MemberTypes.Method)
                {
                    MethodInfo methodInfo = targetMemberInfo as MethodInfo;
                    method = methodInfo.CreateDelegate(typeof(Func<T>), targetParent) as Func<T>;
                }
                /*else if(!targetMemberInfo.DeclaringType.IsValueType)
                {
                    value = (T) targetMemberInfo.GetMemberValue(targetParent);
                }*/
            }
            else
            {
                string logMessage = "Target member is not found in: " + this.GetType().FullName + "Path is: " + path;
                Debug.LogError(logMessage);
            }
        }


        #region Inspector Logic
        ValueDropdownList<Component> GetTargetComponents()
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
        ValueDropdownList<string> GetProperty_0()
        {

            if (targetComponent == null)
            {
                property_0 = "";
                return null;
            }
            else
            {
                return GetPropertyLabels(ExternalFieldUtilities.GetAllMembers<T>(targetComponent));
            }
        }
        ValueDropdownList<string> GetProperty_1()
        {
            if (targetComponent == null || property_0 == "")
            {
                property_1 = "";
                return null;
            }
            else
            {
                object propertyObj_0 = ExternalFieldUtilities.GetMemberObject(targetComponent, property_0);
                if (propertyObj_0 != null)
                {
                    return GetPropertyLabels(ExternalFieldUtilities.GetAllMembers<T>(propertyObj_0));
                }
                else
                {
                    //property_0 = "ERROR: OBJECT NOT FOUND";
                    property_1 = "";
                    return null;
                }
            }
        }
        ValueDropdownList<string> GetProperty_2()
        {
            if (targetComponent == null || property_0 == "" || property_1 == "")
            {
                property_2 = "";
                return null;
            }
            else
            {
                object propertyObj_1 = ExternalFieldUtilities.GetMemberObject(ExternalFieldUtilities.GetMemberObject(targetComponent, property_0), property_1);
                if (propertyObj_1 != null)
                {
                    return GetPropertyLabels(ExternalFieldUtilities.GetAllMembers<T>(propertyObj_1, false));
                }
                else
                {
                    //property_1 = "ERROR: OBJECT NOT FOUND";
                    property_2 = "";
                    return null;
                }
            }
        }


        void OnChangedProperty(int propertyLevel)
        {
            CorrectProperties(propertyLevel);
            UpdateInspector();
        }
        void CorrectProperties(int propertyLevel)
        {
            if (propertyLevel < 2)
            {
                property_2 = String.Empty;
            }
            if (propertyLevel < 1)
            {
                property_1 = String.Empty;
            }
            if (propertyLevel < 0)
            {
                property_0 = String.Empty;
            }
            if (propertyLevel < -1)
            {
                targetComponent = null;
            }
        }

        string GetPath()
        {
            string path = string.Join("/", property_0, property_1, property_2);
            path = path.Replace("//", "");
            path = path.TrimEnd('/');
            return path;
        }

        #endregion
        #region Inspector View
        ValueDropdownList<string> GetPropertyLabels(List<MemberInfo> members)
        {
            ValueDropdownList<string> valueDropdownList = new ValueDropdownList<string>();
            valueDropdownList.Add(new ValueDropdownItem<string>("None", ""));
            members.Sort((x, y) =>
            {
                return GetCompareWeight(y) - GetCompareWeight(x);
            });
            for (int i = 0; i < members.Count; i++)
            {
                string label;
                label = i + ": (" + MemberTypeOutput(members[i].MemberType) + ") " + members[i].Name + " | " + DeclarigTypeOutput(members[i]);
                ValueDropdownItem<string> valueDropdownItem = new ValueDropdownItem<string>(label, members[i].Name);
                valueDropdownList.Add(valueDropdownItem);
            }
            return valueDropdownList;
        }
        int GetCompareWeight(MemberInfo memberInfo)
        {
            if (memberInfo.MemberType == MemberTypes.Method)
            {
                return 3;
            }
            else if (memberInfo.MemberType == MemberTypes.Property)
            {
                return 2;
            }
            else if (memberInfo.MemberType == MemberTypes.Field)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        string MemberTypeOutput(MemberTypes memberType)
        {
            return memberType.ToString().Substring(0, 1);
        }
        string DeclarigTypeOutput(MemberInfo memberInfo)
        {

            if (memberInfo.MemberType == MemberTypes.Method)
            {
                MethodInfo mi = memberInfo as MethodInfo;
                //return mi.GetNiceName();
                return mi.GetReturnType().Name;
            }
            else if (memberInfo.MemberType == MemberTypes.Field)
            {
                FieldInfo fi = memberInfo as FieldInfo;
                return fi.FieldType.Name;
            }
            else if (memberInfo.MemberType == MemberTypes.Property)
            {
                PropertyInfo pi = memberInfo as PropertyInfo;
                return pi.PropertyType.Name;
            }
            else
            {
                return "UNK";
            }

        }
        void DefinePropertyLevel()
        {
            if (property_0.IsNullOrWhitespace())
            {
                propertyLevel = -1;
            }
            else if (property_1.IsNullOrWhitespace())
            {
                propertyLevel = 0;
            }
            else if (property_2.IsNullOrWhitespace())
            {
                propertyLevel = 1;
            }
        }
        bool IsShowProperty_1()
        {
            return !property_0.IsNullOrWhitespace();
        }

        bool IsShowProperty_2()
        {
            return !property_1.IsNullOrWhitespace();
        }
        [OnInspectorInit]
        void UpdateInspector()
        {
            DefineTargetParentAndMemberInfo();
            //DefineTargetParentAndMemberInfo();
            object currentValue;
            if (!IsCurrentValueValid(out currentValue))
            {
                infoBox = "Incorrect value type: " + typeof(T).Name + " != ";
                if (currentValue == null)
                {
                    infoBox += "Unknown Type";
                }
                else
                {
                    infoBox += currentValue.GetType().Name;
                }
            }
            else
            {
                infoBox = "";
            }
        }
        bool IsCurrentValueValid(out object currentValue)
        {
            currentValue = GetValueWithoutCast();
            return currentValue is T;
        }
        string Info()
        {
            if (targetComponent != null)
                currentPathEditor = targetComponent.name +"/"+ targetComponent.GetType().Name + "/" + GetPath();
            else
                currentPathEditor = "";

            return "Type: " + typeof(T).Name;
        }
        string ServiceGroupName()
        {
            return Info() + " | Path: " + currentPathEditor;
        }
        string ValueLable()
        {
            object currentValue;
            if (IsCurrentValueValid(out currentValue))
            {
                return "Value: " + currentValue.ToString();
            }
            else
            {
                return "Value has incorrect type!";
            }
        }

        #endregion

        #region OLD

        /*        void DefinePropertyLevel()
                {
                    if (property_0.IsNullOrWhitespace())
                    {
                        propertyLevel = -1;
                    }
                    else if(property_1.IsNullOrWhitespace())
                    {
                        propertyLevel = 0;
                    }
                }*/

        #endregion
        [FoldoutGroup(serviceGroupName)]
        [Button]
        public void DebugInfo()
        {
            Debug.Log(typeof(T));
            Debug.Log(nameof(targetParent) + ": " + targetParent);
            Debug.Log(nameof(targetMemberInfo) + ": " + targetMemberInfo);
        }
        const string serviceGroupName = "Service";
    }
}
