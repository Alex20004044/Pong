using MSFD;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class BallMovementSolidBody : MonoBehaviour, IVector2Settable
    {
        [SerializeField]
        float maxSpeed = 40;
        [SerializeField]
        float acceleration = 0.5f;
        [SerializeField]
        float speed = 10;
        Vector2 moveDirection;

        bool isActivated = false;

        SolidBody2D solidBody;
        private void Awake()
        {
            solidBody = GetComponent<SolidBody2D>();
        }
        public void SetDirection(Vector2 direction)
        {
            this.moveDirection = direction;
            isActivated = true;
        }
        private void FixedUpdate()
        {
            if (!isActivated) return;

            Vector2 displacement = speed * moveDirection * Time.fixedDeltaTime;

            speed += acceleration * Time.fixedDeltaTime;
            if (speed >= maxSpeed)
                speed = maxSpeed;
            
            //Check is collide with player
            solidBody.Move(transform.position + MSFD.AS.Coordinates.ConvertVector2ToVector3(displacement, convertMode: MSFD.AS.Coordinates.ConvertV2ToV3Mode.y_to_y));
        }

        public void ReflectHorizontalDirection()
        {
            moveDirection.x = -moveDirection.x;
        }        
        public void ReflectVerticalDirection()
        {
            moveDirection.y = -moveDirection.y;
        }
    }
}