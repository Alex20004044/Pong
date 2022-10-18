using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public struct Coordinate
    {
        public Vector3 position;
        public Quaternion rotation;

        public Coordinate(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
