using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        Vector3 rotateVector = Vector3.up;

        [SerializeField]
        float rotateSpeed = 1;

        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Quaternion rotation = Quaternion.AngleAxis(rotateSpeed * UnityEngine.Time.fixedDeltaTime, rotateVector);
            rb.MoveRotation(rb.rotation * rotation);
        }

        public void SetRotateVector(Vector3 rotateDir)
        {
            rotateVector = rotateDir;
        }
        public void SetRotateSpeed(float speed)
        {
            rotateSpeed = speed;
        }
    }
}