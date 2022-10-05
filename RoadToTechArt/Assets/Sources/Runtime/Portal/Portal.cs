using UnityEngine;

namespace RoadToTechArt.Client
{
    public sealed class Portal : MonoBehaviour
    {
        [SerializeField] private Portal _otherPortal = default;
        [SerializeField] private PortalRenderer _portalRenderer = default;
        [SerializeField] private Collider[] _wallColiders = default;

        private Teleporter _enteredObject = default;

        public void OnRender(Camera mainCamera)
        {
            _portalRenderer.Render(mainCamera, _otherPortal.transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            Teleporter teleporter = default;

            if (other.TryGetComponent(out teleporter))
            {
                _enteredObject = teleporter;
                teleporter.EnterPortal(this, _otherPortal, _wallColiders);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Teleporter teleporter = default;

            if (other.TryGetComponent(out teleporter))
            {
                teleporter.ExitPortal(_wallColiders);
                _enteredObject = null;
            }
        }

        private void Update()
        {
            if (_enteredObject != null)
            {
                Vector3 relativePosition = transform.InverseTransformPoint(_enteredObject.transform.position);
                if (relativePosition.z > 0.0f)
                {
                    _enteredObject.Teleport();
                }
            }
        }
    }
}