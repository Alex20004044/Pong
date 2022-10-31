using CorD.SparrowInterfaceField;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    [RequireComponent(typeof(SolidBody2D))]
    public class AxisMovementSolidBody2D : NetworkBehaviour, IObserver<float>
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
        bool isRestrictMovemenetInBounds = false;
        [ShowIf(nameof(isRestrictMovemenetInBounds))]
        [SerializeField]
        Bounds moveBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(5, 5, 0));

        float speed = 0;

        float desiredDirection = 0;

        SolidBody2D solidBody;
        IBumper bumper;
        private void Awake()
        {
            GetComponent<IObservable<float>>().Subscribe(this).AddTo(this);
            axis.Normalize();
            solidBody = GetComponent<SolidBody2D>();
            bumper = GetComponent<IBumper>();
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

                Vector3 targetPosition = transform.position + axis * speed * Time.fixedDeltaTime;
                if (isRestrictMovemenetInBounds)
                {
                    Bounds targetBounds = bumper.GetBounds();
                    targetBounds.center = targetPosition;
                    targetPosition = MSFD.AS.Coordinates.Clamp(targetBounds, moveBounds);
                }

                solidBody.Move(targetPosition);
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
            desiredDirection = Math.Clamp(value, -1, 1);
        }
    }
}