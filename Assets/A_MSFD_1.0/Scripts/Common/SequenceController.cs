using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MSFD;
using Sirenix.OdinInspector;

namespace MSFD
{
    public class SequenceController : MonoBehaviour
    {
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "description")]
        [SerializeField]
        SequenceStep[] sequenceSteps;
        [SerializeField]
        SequenceStep onComplete;
        [SerializeField]
        string sequenceControllerName = "";
        [ReadOnly]
        int currentStepNum = 0;

        private void Awake()
        {
            Messenger.AddListener(sequenceControllerName + SEQUENCE_START_NEXT_STEP, OnStartNextStep);
        }
        private void OnDestroy()
        {
            Messenger.RemoveListener(sequenceControllerName + SEQUENCE_START_NEXT_STEP, OnStartNextStep);
        }

        void OnStartNextStep()
        {
            StartNextStep();
        }
        [Button]
        public void StartNextStep()
        {
            if(currentStepNum >= sequenceSteps.Length)
            {
                Debug.LogError("Attempt to StartNextStep, when all steps completed in " + sequenceControllerName);
            }
            CancelInvoke(nameof(StartNextStep));
            Messenger<int>.Broadcast(sequenceControllerName + INT_SEQUENCE_STEP, currentStepNum, MessengerMode.DONT_REQUIRE_LISTENER);
            sequenceSteps[currentStepNum].unityEvent.Invoke();
            if (sequenceSteps[currentStepNum].isActivateNextStepThroughTime)
            {
                Invoke(nameof(StartNextStep), sequenceSteps[currentStepNum].activateTime);
            }
            currentStepNum++;

            if (currentStepNum >= sequenceSteps.Length)
            {
                onComplete.unityEvent.Invoke();
            }
        }
        public const string INT_SEQUENCE_STEP = "INT_SEQUENCE_STEP";
        public const string SEQUENCE_START_NEXT_STEP = "SEQUENCE_START_NEXT_STEP";
    }

    [System.Serializable]
    public class SequenceStep
    {
        [HorizontalGroup("Top")]
        public string description;
        [HorizontalGroup("Top")]
        public bool isActivateNextStepThroughTime = false;
        [ShowIf("$" + nameof(isActivateNextStepThroughTime))]
        public float activateTime = 0;
        [FoldoutGroup("Events")]
        public UnityEvent unityEvent;
    }
}