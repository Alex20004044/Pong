using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class BumperQuad : BumperBase
    {
        [SerializeField]
        float width = 1;
        [SerializeField]
        float height = 1;

        public override Bounds GetBounds()
        {
            return new Bounds(transform.position, new Vector3(width * transform.localScale.x, height * transform.localScale.y));
        }
    }
}