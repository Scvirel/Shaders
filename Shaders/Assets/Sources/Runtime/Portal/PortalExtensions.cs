using UnityEngine;

namespace RoadToTechArt.Client
{
    public static class PortalExtensions
    {
        private static readonly Quaternion _halfTurn = Quaternion.Euler(0f, 180f, 0f);

        public static Vector3 MirrorPosition(this Transform target, Transform value1, Transform value2)
        {
            Vector3 relativePosition = value1.InverseTransformPoint(target.position);
            relativePosition = _halfTurn * relativePosition;
            return value2.TransformPoint(relativePosition);
        }

        public static Quaternion MirrorRotation(this Transform target, Transform value1, Transform value2)
        {
            Quaternion relativeRotation = Quaternion.Inverse(value1.rotation) * target.rotation;
            relativeRotation = _halfTurn * relativeRotation;
            return value2.rotation * relativeRotation;
        }
    }
}