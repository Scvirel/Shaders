using UnityEngine;

namespace RoadToTechArt.Client
{
    public sealed class MainCamera : MonoBehaviour
    {
        [SerializeField] private Portal[] _portals = default;

        private Camera _playerCamera = default;

        private void Awake()
        {
            _playerCamera = GetComponent<Camera>();
        }

        private void OnPreRender()
        {
            for (int i = 0; i < _portals.Length; i++)
            {
                _portals[i].OnRender(_playerCamera);
            }
        }
    }
}