using UnityEngine;

namespace RoadToTechArt.Client
{
    public sealed class Portal : MonoBehaviour
    {
        [SerializeField] private Portal _otherPortal = default;
        [SerializeField] private PortalRenderer _portalRenderer = default;

        public void OnRender(Camera mainCamera)
        {
            _portalRenderer.Render(mainCamera, _otherPortal.transform);
        }
    }
}