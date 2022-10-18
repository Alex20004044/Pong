using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MSFD
{
    public class ActivateZone : MonoBehaviour
    {
        [SerializeField]
        string[] targetTags;

        private void OnTriggerEnter(Collider other)
        {
            if (AuxiliarySystem.CompareTags(other.tag, targetTags))
            {
                IActivator activator = other.gameObject.GetComponent<IActivator>();
                if (activator != null)
                {
                    activator.TryActivate();
                    //Debug.Log( "Activate " + other.name);
                }
               /* else
                {
                    //Debug.Log("Is not Activated " + other.name);
                }*/
            }
            /*else
                Debug.Log("Is not Activated " + other.name);*/
        }
    }
}