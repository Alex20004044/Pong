/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [RequireComponent(typeof(JoystickInputController))]
    public class AutoAim : MonoBehaviour, IVectorInput
    {
        [SerializeField]
        JoystickInputController joystickInputController;


        public string[] debugString;
        public float aimCoefficient = 10f;
        [SerializeField]
        Transform turret;

        [SerializeField]
        string[] targetTags;

        public float rotateSpeed = 5f;
        public float aimRange = 30f;
        public float deltaAngleWhenAimIsComplete = 5f;
        public UnityEngine.Events.UnityEvent onAimComplete;
        public bool IsAimComplete { get; private set; }
        Transform target;
        Transform previousTarget;

        List<Transform> targets;

        Vector3 targetAimDirection;
        Vector3 desiredAimDirection;
        void Awake()
        {
            joystickInputController.SubscribeToInputController(this);
        }
        private void Start()
        {
            
            FindTargets();
            InvokeRepeating("FindTargets", 0, GameValuesCH.aimUpdateRate);
        }

        void IVectorInput.SetVectorInput(Vector3 input)
        {
            desiredAimDirection = input;
        }
        public void Update()
        {
            targetAimDirection = CalculateTargetAimDirection();
            targetAimDirection = AuxiliarySystem.ResetYAxis(targetAimDirection);
            if (targetAimDirection != Vector3.zero)
            {
                Quaternion q = Quaternion.LookRotation(targetAimDirection);
                //Quaternion.
                q = Quaternion.RotateTowards(turret.rotation, q, rotateSpeed * Time.deltaTime);
                turret.rotation = q;// Quaternion.LookRotation(q * turret.forward);
            }
            else
            {
                Quaternion q;
                q = Quaternion.RotateTowards(turret.localRotation, Quaternion.identity, rotateSpeed * Time.deltaTime);
                turret.localRotation = q;
            }

            if ((Vector3.Angle(turret.forward, targetAimDirection) < deltaAngleWhenAimIsComplete))
            {                
                IsAimComplete = true;
                onAimComplete.Invoke();
            }
            else
            {
                IsAimComplete = false;
            }
        }

        Vector3 CalculateTargetAimDirection()
        {
            previousTarget = target;
            target = null;
            float targetValue = float.PositiveInfinity;
            if (targets == null)
            {
                return desiredAimDirection;// Vector3.zero;
            }
            //
            debugString = new string[targets.Count];

            for (int i = 0; i < targets.Count; i++)
            {
                float distance = (targets[i].position - turret.position).magnitude;
                float angleBetweenDesiredAndReal = Vector3.Angle(desiredAimDirection, targets[i].position - turret.position);
                float value = CalculateValue(distance, angleBetweenDesiredAndReal);
                if (value < targetValue)
                {
                    targetValue = value;
                    target = targets[i];
                }
                //
                debugString[i] = targets[i].gameObject.name + " " + value;
            }

            if (target != null)
            {
                return target.position - turret.position;
            }
            else
            {
                return desiredAimDirection;//;Vector3.zero;
            }
        }
        public void FindTargets()
        {
            targets = new List<Transform>();
            Collider[] colliders = Physics.OverlapSphere(AuxiliarySystem.ResetYAxis(turret.position, GameValuesCH.zeroLevel), aimRange, GameValuesCH.ReturnUnitLayerMask());
            for (int i = 0; i < colliders.Length; i++)
            {
                if (MSFD.AuxiliarySystem.CompareTags(colliders[i].tag, targetTags))
                {
                    targets.Add(colliders[i].transform);
                }
            }
        }
        float CalculateValue(float distance, float angleBetweenDesiredAndReal)
        {
            return distance + angleBetweenDesiredAndReal * aimCoefficient;//0.4 aim                                                                        //return distance + angleBetweenDesiredAndReal * aimCoefficient * aimCoefficient;//0.4aim
        }
        public Transform GetAimTarget()
        {
            return target;
        }
        public void SetTargetTags(string[] _targetTags)
        {
            targetTags = _targetTags;
        }
           
    }
}*/