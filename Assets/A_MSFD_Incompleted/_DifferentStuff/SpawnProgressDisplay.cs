/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MSFD
{
    public class SpawnProgressDisplay : MonoBehaviour
    {
        //public float currentProgress = 0;
        [SerializeField]
        float lerpSpeed = 0.1f;
        [SerializeField]
        Slider slider;
        [SerializeField]
        GameObject nickPrefab;
        [Header("Nick parent must copy rectTransform parametres from FillArea. Other parametres will set automatically")]
        [SerializeField]
        HorizontalLayoutGroup nickParent;
        [SerializeField]
        private TextMeshProUGUI progressText;
        
        float value = 0;
        private void Start()
        {
            if (nickPrefab != null)
            {
                int halfNickWidth = (int)nickPrefab.GetComponent<RectTransform>().sizeDelta.x / 2;
                nickParent.padding.right = -halfNickWidth;
                nickParent.padding.left = halfNickWidth;
                nickParent.childAlignment = TextAnchor.MiddleRight;
                nickParent.childForceExpandWidth = true;

                int waveCount = LevelProgressCalculatorBase.Instance.GetNumLevelSteps();
                GameObject nick;
                for (int i = 0; i < waveCount; i++)
                {
                    nick = Instantiate<GameObject>(nickPrefab);
                    nick.transform.SetParent(nickParent.transform);
                }
            }
        }
        private void LateUpdate()
        {
            UpdateSpawnProgress();
        }
        public void UpdateSpawnProgress()
        {
            float currentSpawnProgress = LevelProgressCalculatorBase.Instance.GetCurrentLevelProgress();
             //LevelProgressCalculatorBase.Instance.GetNumLevelSteps()
            float realLerpSpeed;
            realLerpSpeed = Time.deltaTime * lerpSpeed;
            
            if (currentSpawnProgress - value >= 0.5)
            {
                realLerpSpeed *= 3f;
            }
            
            progressText.text = Mathf.RoundToInt(Mathf.Lerp(value * 100,currentSpawnProgress * 100, realLerpSpeed)).ToString() + "%";
            value = Mathf.Lerp(value, currentSpawnProgress, realLerpSpeed);
            slider.value = value;
        }
    }
}*/