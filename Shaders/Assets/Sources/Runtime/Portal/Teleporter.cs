using UnityEngine;

namespace RoadToTechArt.Client
{
    public sealed class Teleporter : MonoBehaviour
    {
        private Portal _portalEnter = default;
        private Portal _portalExit = default;

        private Collider _colider = default;

        private void Awake()
        {
            _colider = GetComponent<Collider>();
        }

        public void EnterPortal(Portal enterPortal, Portal exitPortal, Collider[] wallColiders)
        {
            _portalEnter = enterPortal;
            _portalExit = exitPortal;
            foreach (Collider item in wallColiders)
            {
                Physics.IgnoreCollision(_colider, item);
            }
        }

        public void ExitPortal(Collider[] wallColiders)
        {
            foreach (Collider item in wallColiders)
            {
                Physics.IgnoreCollision(_colider, item, false);
            }
        }

        public void Teleport()
        {
            Transform enterPortalTransform = _portalEnter.transform;
            Transform exitPortalTransform = _portalExit.transform;

            transform.position = transform.MirrorPosition(enterPortalTransform, exitPortalTransform);
            transform.rotation = transform.MirrorRotation(enterPortalTransform, exitPortalTransform);
        }
    }
}