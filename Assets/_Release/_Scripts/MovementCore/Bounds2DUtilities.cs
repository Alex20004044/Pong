using MSFD.AS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public static class Bounds2DUtilities
    {
        public static Vector3 TopRightPoint(this Bounds bounds)
        {
            return new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y);
        }        
        public static Vector3 TopLeftPoint(this Bounds bounds)
        {
            return new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y);
        }        
        public static Vector3 DownRightPoint(this Bounds bounds)
        {
            return new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y);
        }        
        public static Vector3 DownLeftPoint(this Bounds bounds)
        {
            return new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);
        }
    }
}