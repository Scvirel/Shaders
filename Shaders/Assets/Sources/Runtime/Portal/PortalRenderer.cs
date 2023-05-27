using UnityEngine;

namespace RoadToTechArt.Client
{
    public sealed class PortalRenderer : MonoBehaviour
    {
        [SerializeField] private Color _outlineColor = default;
        [SerializeField] private Renderer _outlineRenderer = default;
        [SerializeField] private Camera _portalCamera = default;

        private Material _material = default;
        private Renderer _renderer = default;
        private RenderTexture _renderTexture = default;

        private const int RENDER_ITERATIONS = 3;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _renderer = GetComponent<Renderer>();
            _material = _renderer.material;
            _renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            _material.mainTexture = _renderTexture;
            _portalCamera.targetTexture = _renderTexture;
            _outlineRenderer.material.color = _outlineColor;

        }

        public void Render(Camera mainCamera, Transform otherPortalTransform)
        {
            if (!_renderer.isVisible)
            {
                return;
            }

            for (int i = RENDER_ITERATIONS; i > 0; i--)
            {

            }

            SetupProjection(mainCamera, otherPortalTransform);
            RenderInternal(mainCamera, otherPortalTransform);
        }

        private void SetupProjection(Camera mainCamera, Transform exitpoint)
        {
            Plane plane = new Plane(-exitpoint.forward, exitpoint.position);
            Vector4 clipPlane = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
            Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(_portalCamera.worldToCameraMatrix)) * clipPlane;

            Matrix4x4 resultMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
            _portalCamera.projectionMatrix = resultMatrix;
        }

        private void RenderInternal(Camera mainCamera, Transform otherPortalTransform)
        {
            Transform enterPoint = transform;
            Transform exitPoint = otherPortalTransform;

            Transform portalCamTransform = _portalCamera.transform;
            portalCamTransform.transform.position = mainCamera.transform.position;
            portalCamTransform.transform.rotation = mainCamera.transform.rotation;

            portalCamTransform.transform.position = portalCamTransform.MirrorPosition(enterPoint, exitPoint);
            portalCamTransform.transform.rotation = portalCamTransform.MirrorRotation(enterPoint, exitPoint);

            _portalCamera.Render();
        }
    }
}