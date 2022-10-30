using JetBrains.Annotations;
using MSFD.AS;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    [RequireComponent(typeof(BumperBase))]
    public class SolidBody2D : MonoBehaviour
    {
        BumperBase bumper;
        private void Awake()
        {
            bumper = GetComponent<BumperBase>();
        }
        [Button]
        public void Move(Vector3 position)
        {
            SolidBody2DManager.Instance.Move(this, position);
        }

        /*        [Button]
                public void GetSizeFromSprite()
                {
                    if (sprite == null)
                        sprite = GetComponentInChildren<SpriteRenderer>();

                    //sprite.ResetBounds();
                    //width = sprite.bounds.size.x;
                    //height = sprite.bounds.size.y;

                    width = sprite.localBounds.size.x;
                    height = sprite.localBounds.size.y;            
                }*/

        public BumperBase GetBumper()
        {
            return bumper;
        }
    }
}