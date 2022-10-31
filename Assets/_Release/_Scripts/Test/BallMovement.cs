using MSFD;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField]
        float maxSpeed = 40;
        [SerializeField]
        float acceleration = 0.5f;
        [SerializeField]
        float speed = 10;
        Vector2 moveDirection;

        //Attention
        [SerializeField]
        float maxHorCoord = 5;
        [SerializeField]
        float minHorCoord = -5;

        [SerializeField]
        float maxVertCoord = 5;
        [SerializeField]
        float minVertCoord = -5;

        bool isActivated = false;
        public void ActivateMove(Vector2 moveDirection)
        {
            this.moveDirection = moveDirection;
            isActivated = true;
        }
        private void FixedUpdate()
        {
            if (!isActivated) return;

            Vector2 displacement = speed * moveDirection * Time.fixedDeltaTime;

            speed += acceleration * Time.fixedDeltaTime;
            if (speed >= maxSpeed)
                speed = maxSpeed;

            Vector3 targetPosition = transform.position + MSFD.AS.Coordinates.ConvertVector2ToVector3(displacement, convertMode: MSFD.AS.Coordinates.ConvertV2ToV3Mode.y_to_y);

            if (targetPosition.x >= maxHorCoord || targetPosition.x <= minHorCoord)
            {
                displacement = new Vector2(-displacement.x, displacement.y);
                moveDirection = new Vector2(-moveDirection.x, moveDirection.y);
            }
            //Check is collide with player
            transform.position = transform.position + MSFD.AS.Coordinates.ConvertVector2ToVector3(displacement, convertMode: MSFD.AS.Coordinates.ConvertV2ToV3Mode.y_to_y);

            if (targetPosition.y >= maxVertCoord) 
            {
                Messenger.Broadcast(GameEventsPong.I_BALL_IN_TOP_GATE, MessengerMode.DONT_REQUIRE_LISTENER);

                /*displacement = new Vector2(displacement.x, -displacement.y);
                moveDirection = new Vector2(moveDirection.x, -moveDirection.y);*/
            }            
            if (targetPosition.y <= minVertCoord) 
            {
                Messenger.Broadcast(GameEventsPong.I_BALL_IN_DOWN_GATE, MessengerMode.DONT_REQUIRE_LISTENER);
            }
           
        }
    }
}