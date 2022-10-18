using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class ShakeActivate : MonoBehaviour
    {
        [SerializeField] private ShakeType shakeType = ShakeType.shellExplosion;
        [SerializeField]
        bool isCountUse = true;
        [SerializeField] 
        int activateEffectOnNumberOfShootMinValue = 1;
        [SerializeField] 
        int activateEffectOnNumberOfShootMaxValue = 3;

        int _currentCount;
        int _currentValue;

        void Start()
        {
            _currentValue = Random.Range(activateEffectOnNumberOfShootMinValue,
                activateEffectOnNumberOfShootMaxValue + 1);
        }
        public void ShakeCamera()
        {
            if (isCountUse)
            {
                _currentCount++;

                if (_currentCount >= _currentValue)
                {
                    CameraShakeManager.instance.OnShakeStarts(shakeType);
                    _currentCount = 0;
                    _currentValue = Random.Range(activateEffectOnNumberOfShootMinValue,
                        activateEffectOnNumberOfShootMaxValue + 1);
                }
            }
            else
            {
                CameraShakeManager.instance.OnShakeStarts(shakeType);   
            }
        }
    }
}