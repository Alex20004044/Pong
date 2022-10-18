/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [RequireComponent(typeof(AutoAim))]
    public class TargetMarkerController : MonoBehaviour
    {
        [SerializeField]
        GameObject targetMarkerPrefab;

        GameObject targetMarker;
        AutoAim autoAim;
        private void Start()
        {
            targetMarker = Instantiate<GameObject>(targetMarkerPrefab);
            autoAim = GetComponent<AutoAim>();
            GetComponent<SimpleHP>().onUnitDestroyed.AddListener(Destruct);
        }

        private void LateUpdate()
        {
            Transform target = autoAim.GetAimTarget();
            if (target != null)
            {
                targetMarker.SetActive(true);
                targetMarker.transform.position = AuxiliarySystem.ResetYAxis(target.position, GameValuesCH.zeroLevel);
            }
            else
            {
                targetMarker.SetActive(false);
            }
        }
        private void OnDisable()
        {
            targetMarker.SetActive(false);
        }
        private void OnDestroy()
        {
            Destruct();
        }
        public void Destruct()
        {
            Destroy(targetMarker);
        }
    }
}*/