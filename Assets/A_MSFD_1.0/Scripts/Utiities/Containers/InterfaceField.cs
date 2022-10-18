using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace MSFD
{
    [System.Obsolete("Use Sparrow InterfaceField instead")]
    [InlineProperty()]
    [System.Serializable]
    public class InterfaceField<T>
    {
        //[InlineButton("@"+nameof(FindInterfaceAutomatically)+"()")]
        [HideLabel]
        [ValidateInput("@" + nameof(Filter) +"()", "@" + nameof(ErrorMessage) + "()", InfoMessageType.Error)]
        [SerializeField]
        Object objRef;
        T interfaceField;
        bool isInit = false;
        public InterfaceField()
        {

        }

        public T i
        {
            get
            {
                return Get();
            }
        }
        public bool Filter()
        {
            if (objRef == null)
            {
                return false;
            }
            else
            {
                GameObject go = objRef as GameObject;
                if(go != null)
                {
                    interfaceField = go.GetComponent<T>();
                }
                else
                {
                    interfaceField = (T)(object)objRef;
                    //interfaceField = unRef as T;
                }

                return interfaceField != null;
            }
        }
        public string ErrorMessage()
        {
            return typeof(InterfaceField<T>).GenericTypeArguments[0].Name + " is required";
        }
        public T Get()
        {
            if (isInit == false)
            {
                isInit = true;
                GameObject go = objRef as GameObject;
                if (go != null)
                {
                    interfaceField = go.GetComponent<T>();
                }
                else if(objRef != null)
                {
                    interfaceField = (T)(object)objRef;// as T;
                }
            }
            /*Probably null value is correct, because we can try to check null or not in external code
            if (interfaceField == null)
                Debug.LogError("Interface field null reference");*/
            return interfaceField;
        }
        /// <summary>
        /// Manual interface initialization
        /// </summary>
        /// <param name="objRef"></param>
        public void Set(Object objRef)
        {
            this.objRef = objRef;
            isInit = false;
        }
    }
}