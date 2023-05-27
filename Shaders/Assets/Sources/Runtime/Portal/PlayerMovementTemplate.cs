using UnityEngine;

namespace RoadToTechArt.Client
{
    public sealed class PlayerMovementTemplate : MonoBehaviour
    {
        [SerializeField] private Camera _camera = default;

        float horizontalInput;
        float verticalInput;

        Vector3 moveDirection;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        private void Update()
        {
            MyInput();
            CameraBehaviour();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }

        private void MovePlayer()
        {
            moveDirection = _camera.transform.forward * verticalInput + _camera.transform.right * horizontalInput;
            moveDirection.y = 0f;
            transform.position += moveDirection * 5f * Time.deltaTime;
        }

        private float xRotation = default;
        private float yRotation = default;

        private void CameraBehaviour()
        {
            xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * 50f;
            yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * 50f;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            _camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}