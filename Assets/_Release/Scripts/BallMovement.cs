using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class BallMovement : NetworkBehaviour
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

        private void Start()
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        public override void OnNetworkSpawn()
        {
            if (!IsServer)
                enabled = false;
        }
        private void FixedUpdate()
        {
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
            if (targetPosition.y >= maxVertCoord || targetPosition.y <= minVertCoord)
            {
                displacement = new Vector2(displacement.x, -displacement.y);
                moveDirection = new Vector2(moveDirection.x, -moveDirection.y);
            }
            //Check is collide with player

            transform.position = transform.position + MSFD.AS.Coordinates.ConvertVector2ToVector3(displacement, convertMode: MSFD.AS.Coordinates.ConvertV2ToV3Mode.y_to_y);
        }
    }
}