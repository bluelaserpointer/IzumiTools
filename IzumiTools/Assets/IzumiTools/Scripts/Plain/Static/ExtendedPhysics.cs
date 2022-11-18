using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IzumiTools
{
    public static class ExtendedPhysics2D
    {
        public static RaycastHit2D[] RaycastAllBetween(Vector2 originPos, Vector2 targetPos)
        {
            Vector2 targetVec = targetPos - originPos;
            return Physics2D.RaycastAll(originPos, targetVec, targetVec.magnitude);
        }
    }
}