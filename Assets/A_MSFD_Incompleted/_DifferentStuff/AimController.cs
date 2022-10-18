/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD
{
    public class AimController : MonoBehaviour
    {
        [SerializeField]
        float rotateSpeed = 5f;
        [SerializeField]
        float aimRange = 30f;
        [SerializeField]
        string[] targetTags;

        [SerializeField]
        Transform turretPoint;
        [SerializeField]
        UnityEvent onFindTarget;
        [SerializeField]
        UnityEvent onLostTarget;

        Transform target;
        private void Awake()
        {
            rotateSpeed *= Time.fixedDeltaTime;
        }
        private void OnEnable()
        {
            InvokeRepeating("FindTarget", 0, GameValuesCH.aimUpdateRate);
        }
        private void OnDisable()
        {
            CancelInvoke();
        }
        void FixedUpdate()
        {
            if (target != null)
            {
                Quaternion rot = Quaternion.LookRotation(AuxiliarySystem.ResetYAxis(target.position - turretPoint.position), Vector3.up);
                rot = Quaternion.RotateTowards(turretPoint.rotation, rot, rotateSpeed);
                turretPoint.rotation = rot;
            }
            else
            {
                //Debug.Log("Target is not setUp");
                Quaternion rot = Quaternion.LookRotation(transform.forward, Vector3.up);
                rot = Quaternion.RotateTowards(turretPoint.rotation, rot, rotateSpeed);
                turretPoint.rotation = rot;
            }
        }


        public void FindTarget()
        {
            Transform nearestTarget = null;
            float nearestDistSqr = float.PositiveInfinity;
            float distSqr;
            Collider[] colliders = Physics.OverlapSphere(AuxiliarySystem.ResetYAxis(turretPoint.position, GameValuesCH.zeroLevel), aimRange, GameValuesCH.ReturnUnitLayerMask());
            for (int i = 0; i < colliders.Length; i++)
            {
                if (MSFD.AuxiliarySystem.CompareTags(colliders[i].tag, targetTags))
                {
                    distSqr = (turretPoint.position - colliders[i].transform.position).sqrMagnitude;
                    if (distSqr < nearestDistSqr)
                    {
                        nearestTarget = colliders[i].transform;
                        nearestDistSqr = distSqr;
                    }
                }
            }
            if(target != nearestTarget)
            {
                if(nearestTarget == null)
                {
                    onLostTarget.Invoke();
                }
                else
                {
                    onFindTarget.Invoke();
                }
            }
            target = nearestTarget;
        }
    }
}*/