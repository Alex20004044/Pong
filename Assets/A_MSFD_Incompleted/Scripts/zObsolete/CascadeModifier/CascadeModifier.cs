using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System.Reflection;

namespace MSFD
{
    [Obsolete("Use CascadeModifierCore or ModifiableField instead")]
    [Serializable]
    public class CascadeModifier<T>
    {
        SortedDictionary<int, List<Func<T, T>>> valueModFuncs = new SortedDictionary<int, List<Func<T, T>>>();
        [HorizontalGroup("Main")]
        [SerializeField]
        T value;

#if UNITY_EDITOR
        [Obsolete(EditorConstants.editorOnly)]
        [OnStateUpdate("@" + nameof(__modifiedValue) + "=" + nameof(GetValue) + "()")]
        [HorizontalGroup("Main")]
        [ReadOnly]
        [ShowInInspector]
        T __modifiedValue;
#endif
        [FoldoutGroup(serviceGroup, 10)]
        [OnValueChanged("@" + nameof(SetStartedModifiers) + "()", includeChildren: true, InvokeOnUndoRedo = true)]
        [TableList]
        [SerializeField]
        List<ModifierData<T>> startModifiers = new List<ModifierData<T>>();

        Action onModifierChanged;

        bool isWasInitialized = false;
        public CascadeModifier()
        {
            Initialize();
        }
        public CascadeModifier(T _value)
        {       
            value = _value;
            Initialize();
        }
        public CascadeModifier(T _value, ref Action<T> SetNewValueFunc)
        {
            value = _value;
            SetNewValueFunc = (T _newValue) => { value = _newValue; onModifierChanged?.Invoke(); };
            Initialize();
        }
        void Initialize()
        {
            if(isWasInitialized)
            {
                return;
            }
            isWasInitialized = true;
            SetStartedModifiers();
        }

        [OnInspectorInit]
/*        [FoldoutGroup(serviceGroup)]
        [Button]*/
        void SetStartedModifiers()
        {
            ////
            valueModFuncs = new SortedDictionary<int, List<Func<T, T>>>();
            foreach (ModifierData<T> modifierData in startModifiers)
            {
#if UNITY_EDITOR
                try
                {
#endif
                    var modifier = modifierData.GetIModifier().GetModifier();
                    if (modifier == null)
                        continue;
                    AddModifier(modifier, modifierData.GetPriority());

#if UNITY_EDITOR
                }
                catch
                {

                }
#endif
            }
            startModifiers.Sort((x, y) => y.GetPriority()-x.GetPriority()  );
        }

        /// <summary>
        /// Get value without modifiers
        /// </summary>
        /// <returns></returns>
        public T GetCleanValue()
        {
            return value;
        }
        //[HorizontalGroup("Main")]
        //[Button]
        public T GetValue()
        {
            return CalculateWithModifiers(value);
        }
        /// <summary>
        /// This method can be used to calculate some value with currently installed modifiers
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual T CalculateWithModifiers(T value)
        {
/*            if (!isWasInitialized)
                Initialize();*/
            foreach (var x in valueModFuncs)
            {
                List<Func<T, T>> modifiers = x.Value;
                for (int i = 0; i < modifiers.Count; i++)
                {
                    value = modifiers[i].Invoke(value);
                }
            }
            return value;
        }
        /// <summary>
        /// Higher priority functions are called first
        /// </summary>
        /// <param name="func"></param>
        /// <param name="priority"></param>
        public void AddModifier(Func<T, T> func, int priority = 0)
        {
            priority = -priority;
            List<Func<T, T>> modifiers;
            if (!valueModFuncs.TryGetValue(priority, out modifiers))
            {
                modifiers = new List<Func<T, T>>();
                valueModFuncs.Add(priority, modifiers);
            }
            modifiers.Add(func);
            onModifierChanged?.Invoke();
        }
        public void RemoveModifier(Func<T, T> func, int priority = 0)
        {
            priority = -priority;
            List<Func<T, T>> modifiers;
            if (valueModFuncs.TryGetValue(priority, out modifiers))
            {
                modifiers.Remove(func);
                onModifierChanged?.Invoke();
            }
            else
            {
                Debug.Log("There are no modifier funcs with " + priority + " priority");
            }
  
        }
        public static implicit operator T(CascadeModifier<T> cascadeModifier)
        {
            //Можно сохранять просчитанные значения и выводить их, если не было изменений, но это может привести к неправильному результату, если функциии внутри зависят от внешних условий
            return cascadeModifier.CalculateWithModifiers(cascadeModifier.value);
        }
        /// <summary>
        /// OnModiferChanged is invoked when valueModFuncs are changed or when new value is set
        /// </summary>
        /// <param name="listener"></param>
        public void AddListenerOnModifierChanged(System.Action listener)
        {
            onModifierChanged += listener;
        }
        public void RemoveListenerOnModifierChanged(System.Action listener)
        {
            onModifierChanged -= listener;
        }
#if UNITY_EDITOR
        [FoldoutGroup(serviceGroup)]
        [Obsolete(EditorConstants.editorOnly)]
        [Button]
        void ShowInstalledModifiers()
        {
            string log = "CascadeModifier<"  + typeof(T) + ">\n";
            foreach(var x in valueModFuncs)
            {
                log += "Priority: " + x.Key;
                foreach (var func in x.Value)
                {
                    log += " Modifier name: " + func.GetMethodInfo().Name;
                }
                log += "\n";
            }
            Debug.Log(log);
        }

#endif
        const string serviceGroup = "Service";
    }
}