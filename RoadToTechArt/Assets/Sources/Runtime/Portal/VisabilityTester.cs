using UnityEngine;
using UnityEngine.UIElements;

namespace RoadToTechArt.Client
{
    public sealed class VisabilityTester : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer = default;

        private void OnBecameVisible()
        {
            Debug.LogWarning(gameObject.name + "become visible");
        }

        private void Update()
        {
            if (_renderer.isVisible)
            {
                Debug.LogWarning("_" + gameObject.name + "visible");
            }
        }
    }
}