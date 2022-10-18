/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD
{
    [RequireComponent(typeof(JoystickInputController))]
    [RequireComponent(typeof(Rigidbody))]
    public class SimpleMovement : MonoBehaviour, IVectorInput
    {
        [SerializeField]
        JoystickInputController joystickInputController;
        
        [SerializeField]
        float maxAngleBeforeStartMove = 20f;
        [SerializeField]
        float maxSpeed = 20;
        [SerializeField]
        float rotSpeed = 500f;
        [SerializeField]
        float acceleration = 10;


        public float treshold = 0.5f;

        float sqrTreshold;
        float sqrMaxSpeed;

        //public UnityEvent onStartMoveForward;
        //public UnityEvent onStartMoveBack;
        //public UnityEvent onStop;

        DelegateCascade<float> maxSpeedMode = new DelegateCascade<float>();
        DelegateCascade<float> accelerationMode = new DelegateCascade<float>();
        Rigidbody rb;
        Vector3 desiredMoveDirection;
      
        void Awake()
        {
            joystickInputController.SubscribeToInputController(this);
            desiredMoveDirection = Vector3.forward;
        }
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            sqrTreshold = treshold * treshold;
            sqrMaxSpeed = maxSpeed * maxSpeed;         
        }
        void IVectorInput.SetVectorInput(Vector3 input)
        {
            //desiredMoveDirection = input;
        }

        private void FixedUpdate()
        {
            float calculatedMaxSpeed = maxSpeedMode.ValueCalculate(maxSpeed);
            float calculatedAcceleration = accelerationMode.ValueCalculate(acceleration);
            
            //Debug.Log("calcaulatedMaxSpeed " + calculatedMaxSpeed);
            if (Vector3.Angle(transform.forward, desiredMoveDirection) < maxAngleBeforeStartMove)
            {
                if (desiredMoveDirection.sqrMagnitude > sqrTreshold)
                {
                    rb.AddForce(transform.forward * calculatedAcceleration, ForceMode.Acceleration);
                }
            }
            if (desiredMoveDirection.sqrMagnitude > sqrTreshold)
            {
                Quaternion q;

                q = Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(desiredMoveDirection, Vector3.up), rotSpeed * Time.fixedDeltaTime);
                rb.MoveRotation(q);
            }
            
            if (rb.velocity.sqrMagnitude > calculatedMaxSpeed * calculatedMaxSpeed)
            {
                rb.velocity = rb.velocity.normalized * calculatedMaxSpeed;
            }
        }
        public void AddMaxSpeedMode(Func<float, float> func)
        {
            maxSpeedMode.Add(func);
        }
        public void RemoveMaxSpeedMode(Func<float, float> func)
        {
            maxSpeedMode.Remove(func);
        }
        public void AddAcceelerationMode(Func<float, float> func)
        {
            accelerationMode.Add(func);
        }
        public void RemoveAccelerationMode(Func<float, float> func)
        {
            accelerationMode.Remove(func);
        }
    }
}*/