using MSFD;
using MSFD.AS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pong
{
    public class SolidBody2DManager : SingletoneBase<SolidBody2DManager>
    {
        Bounds bounds;

        protected override void AwakeInitialization()
        {
            bounds = new Bounds(Vector3.zero, Vector3.positiveInfinity);
        }

        List<BumperBase> bumpers = new List<BumperBase>();

        public void InitializeMapBounds(Bounds bounds)
        {
            this.bounds = bounds;
        }

        public void RegisterSolidBody(BumperBase bumper)
        {
            bumpers.Add(bumper);
        }
        public void UnregisterSolidBody(BumperBase bumper)
        {
            bumpers.Remove(bumper);
        }
        public void Move(SolidBody2D moveBody, Vector3 position)
        {
            /*solidBodies.Sort((x, y) =>
            {
                return (int)((moveBody.transform.position - x.transform.position).sqrMagnitude -
                             (moveBody.transform.position - y.transform.position).sqrMagnitude);
            }
            );*/
            foreach (var b in bumpers)
            {
                if (b.Equals(moveBody.GetBumper())) continue;

                //Check Is Move not allowed
                Bounds bBounds = b.GetBounds();
                Bounds moveBodyBounds = moveBody.GetBumper().GetBounds();


                moveBodyBounds.center = position;

                ///Naive attempt to create ~continious detection
                Vector3 delta = position - moveBody.transform.position;

                delta.Normalize();

                delta = delta * moveBodyBounds.extents.magnitude;
                moveBodyBounds.Encapsulate(moveBody.transform.position);// + delta);
                ///
                //moveBodyBounds.Encapsulate(position);
                if (!moveBodyBounds.Intersects(bBounds)) continue;
                else
                {
                    //!!!!!!!!!!!!!!!
                    BumpInfo bumpInfo = new BumpInfo(moveBody.GetBumper(), b.GetBounds().ClosestPoint(moveBody.transform.position));
                    b.Bump(bumpInfo);
                    bumpInfo.contactedBumper = b;
                    moveBody.GetBumper().Bump(bumpInfo);
                }
            }
            var targetBounds = moveBody.GetBumper().GetBounds();
            targetBounds.center = position;
            moveBody.transform.position = MSFD.AS.Coordinates.Clamp(targetBounds, bounds);
        }

    }
}