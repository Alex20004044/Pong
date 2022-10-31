using MSFD;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class BallMovementSolidBody : MonoBehaviour, IVector2Settable, IBallUpgradable
    {
        [SerializeField]
        float maxSpeed = 40;
        [SerializeField]
        float acceleration = 0.5f;
        [SerializeField]
        ModField<float> speed = new ModField<float>(10);

        Vector2 moveDirection;

        bool isActivated = false;

        SolidBody2D solidBody;
        private void Awake()
        {
            solidBody = GetComponent<SolidBody2D>();

            speed.AddMod((x) =>
            {
                if (x >= maxSpeed)
                    x = maxSpeed;
                return x;
            }, -100).AddTo(this);
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

            if(speed.BaseValue <= maxSpeed)
                speed.BaseValue += acceleration * Time.fixedDeltaTime;

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

        public IModifiable<float> GetSpeedModifiable()
        {
            return speed;
        }
    }
}