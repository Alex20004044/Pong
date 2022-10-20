using CorD.SparrowInterfaceField;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class AxisMovement : NetworkBehaviour, IObserver<float>
    {
        [SerializeField]
        float acceleration = 10;
        [SerializeField]
        float maxSpeed = 10;
        [SerializeField]
        float moveTreshold = 0.1f;
        [SerializeField]
        Vector3 axis = new Vector3(1, 0, 0);
        [SerializeField]
        Bounds moveBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(5, 0, 0));

        float speed = 0;

        float desiredDirection = 0;

        private void Awake()
        {
            GetComponent<IObservable<float>>().Subscribe(this).AddTo(this);
            axis.Normalize();
        }

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                enabled = false;
                return;
            }
        }

        private void FixedUpdate()
        {
            bool isChangeDirection = speed * desiredDirection < 0;
            if (Mathf.Abs(desiredDirection) < moveTreshold || isChangeDirection)
            {
                speed = 0;
                return;
            }
            else
            {
                speed += acceleration * desiredDirection * Time.fixedDeltaTime;
                speed = Math.Clamp(speed, -maxSpeed, maxSpeed);
                
                transform.position = MSFD.AS.Coordinates.Clamp(transform.position + axis * speed * Time.fixedDeltaTime, moveBounds);
            }
        }


        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            Debug.LogError(error.Message);
        }

        public void OnNext(float value)
        {
            desiredDirection = Math.Clamp(value,-1,1);
        }
    }
}