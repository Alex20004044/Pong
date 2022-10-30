using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Pong
{
    public class RacketAI : NetworkBehaviour
    {
        BallMovement ballMovement;

        public override void OnNetworkSpawn()
        {

            if (!IsOwner) enabled = false;
        }

        private void FixedUpdate()
        {
            ballMovement = FindObjectOfType<BallMovement>();
            //!!!!!!!!!!!!!!!!
            if(ballMovement!= null)
                transform.position = new Vector3(ballMovement.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}