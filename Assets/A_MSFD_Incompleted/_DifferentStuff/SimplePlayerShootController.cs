/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [RequireComponent(typeof(AutoAim))]
    public class SimplePlayerShootController : MonoBehaviour, IVectorInput
    {
        [SerializeField]
        JoystickInputController shootJoystickInputController;
        [SerializeField]
        string[] targetTags;
        
        AutoAim autoAim;
        IShoot shoot;
        Vector3 vectorInput;
        void Awake()
        {
            shootJoystickInputController.SubscribeToInputController(this);
            shoot = GetComponent<IShoot>();
            shoot.SetTargetTags(targetTags);

            autoAim = GetComponent<AutoAim>();
            autoAim.SetTargetTags(targetTags);
            autoAim.onAimComplete.AddListener(TryShoot);
        }
        void IVectorInput.SetVectorInput(Vector3 input)
        {
            vectorInput = input;
        }
        public void TryShoot()//Subscribe to onAimComplete
        {
            if(vectorInput != Vector3.zero)
            {
                shoot.TryShoot();
            }
        }
    }
}*/