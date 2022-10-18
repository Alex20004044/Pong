/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class MoveDirectionMarker : MonoBehaviour, IVectorInput
    {
        [SerializeField]
        JoystickInputController joystickInputController;

        [SerializeField]
        GameObject moveDirectionMarkerPrefab;
        [SerializeField]
        Transform moveDirectionMarkerParent;
        GameObject moveDirectionMarker;

        Vector3 moveDirection;
        void Start()
        {
            joystickInputController.SubscribeToInputController(this);

            moveDirectionMarker = Instantiate<GameObject>(moveDirectionMarkerPrefab);
            moveDirectionMarker.transform.SetParent(moveDirectionMarkerParent);
            moveDirectionMarker.transform.localPosition = Vector3.zero;
            moveDirectionMarker.transform.localRotation = Quaternion.identity;
        }

        void IVectorInput.SetVectorInput(Vector3 input)
        {
            moveDirection = input;
        }
        void LateUpdate()
        {
            if (moveDirection != Vector3.zero)
            {
                moveDirectionMarker.SetActive(true);
                moveDirectionMarker.transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            }
            else
            {
                moveDirectionMarker.SetActive(false);
            }
        }
    }
}*/