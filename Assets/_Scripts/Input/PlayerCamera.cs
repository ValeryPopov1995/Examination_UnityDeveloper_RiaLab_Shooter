using UnityEngine;

namespace RiaShooter.Scripts.PlayerInput
{
    internal class PlayerCamera : MonoBehaviour
    {
        [SerializeField, Min(1)] private float _sensetivity = 9;
        private Vector2Int _verticalClamp = new Vector2Int(-80, 80);
        private float _currentVerticalAngle;

        private void Awake()
        {
            _currentVerticalAngle = transform.eulerAngles.x;
        }

        private void Update()
        {
            var currentEulers = transform.eulerAngles;
            Vector2 mouseDelta = _sensetivity * Time.deltaTime * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle - mouseDelta.y, _verticalClamp.x, _verticalClamp.y);
            currentEulers.y += mouseDelta.x;
            currentEulers.x = _currentVerticalAngle;
            transform.eulerAngles = currentEulers;
        }
    }
}