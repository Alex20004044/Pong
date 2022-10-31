using MSFD;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Pong
{
    /// <summary>
    /// Hardcoded for save 2 last collisions with players
    /// </summary>
    [RequireComponent(typeof(BumperBase))]
    public class BumpPlayerGOGetter : MonoBehaviour, IBumpGOGetter
    {
        [SerializeField]
        string targetTag = GameValues.playerTag;
        GameObject[] bumpedGos = new GameObject[2];



        void Start()
        {
            Init();
        }
        void Init()
        {
            bumpedGos = GameObject.FindGameObjectsWithTag(GameValues.playerTag);
            GetComponent<BumperBase>().Subscribe(OnBump).AddTo(this);
        }

        void OnBump(BumpInfo bumpInfo)
        {
            if (bumpInfo.contactedBumper.gameObject.tag != targetTag) return;

            bumpedGos[1] = bumpedGos[0];
            bumpedGos[0] = bumpInfo.contactedBumper.gameObject;
        }

        public GameObject[] GetBumpGos()
        {
            return bumpedGos;
        }
    }
}