using UnityEngine;

namespace RiaShooter.Scripts.Player
{
    internal class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _camera;
        [SerializeField, Min(1)] private float _sensetivity = 9;
        private Vector2Int _verticalClamp = new Vector2Int(-80, 80);
        private float _currentVerticalAngle;

        private void Awake()
        {
            _currentVerticalAngle = transform.eulerAngles.x;
        }

        private void Update()
        {
            Vector2 mouseDelta = _sensetivity * Time.deltaTime * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle - mouseDelta.y, _verticalClamp.x, _verticalClamp.y);

            _body.Rotate(0, mouseDelta.x, 0);
            _camera.localEulerAngles = new(_currentVerticalAngle, 0, 0);
        }
    }
}