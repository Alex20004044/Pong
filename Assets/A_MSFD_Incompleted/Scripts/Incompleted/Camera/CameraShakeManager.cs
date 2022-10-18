using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD.EZCameraShake;

namespace MSFD
{
    public class CameraShakeManager : MonoBehaviour
    {

        public ShakeParameter[] shakeParameters;

        public static CameraShakeManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                //Debug.LogError("!");
            }

        }

        public void OnShakeStarts(ShakeType shakeType)
        {
            ShakeParameter shakeParameter = new ShakeParameter();
            switch (shakeType)
            {
                case ShakeType.shellExplosion:
                    {
                        shakeParameter = shakeParameters[0];
                        break;
                    }
                case ShakeType.explosion:
                    {
                        shakeParameter = shakeParameters[1];
                        break;
                    }
                case ShakeType.bigExplosion:
                    {
                        shakeParameter = shakeParameters[2];
                        break;
                    }

            }
            CameraShaker.Instance.ShakeOnce(shakeParameter.magnitude, shakeParameter.roughness, shakeParameter.fadeInTime, shakeParameter.fadeOutTime);
        }
    }
    public enum ShakeType { shellExplosion, explosion, bigExplosion };
    [System.Serializable]
    public class ShakeParameter
    {
        public string name = "Default";
        public float magnitude = 1f;
        public float roughness = 1f;
        public float fadeInTime = 0.1f;
        public float fadeOutTime = 0.5f;
    }
}