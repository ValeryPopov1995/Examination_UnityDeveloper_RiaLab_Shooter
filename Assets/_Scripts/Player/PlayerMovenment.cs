using UnityEngine;

namespace RiaShooter.Scripts.Player
{
    internal class PlayerMovenment : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField, Min(1)] private float _moveSpeed = 5;
        [SerializeField, Min(0)] private float _jumpSpeed = 3f;
        [SerializeField, Min(0)] private float _gravity = .5f;
        [SerializeField] LayerMask _groundLayer;
        private float _verticalSpeed;

        private void Update()
        {
            Vector3 moveVector = GetMoveVector();
            _characterController.Move(moveVector * Time.deltaTime);
        }

        private Vector3 GetMoveVector()
        {
            if (CanJump())
                _verticalSpeed = _jumpSpeed;
            else
            {
                if (CheckGround() && _verticalSpeed < 0)
                    _verticalSpeed = -.1f;
                else
                    _verticalSpeed -= _gravity;
            }

            var moveVector = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
            moveVector *= _moveSpeed;
            moveVector.y = _verticalSpeed;

            return moveVector;
        }

        private bool CanJump()
        {
            return Input.GetKeyDown(KeyCode.Space) && CheckGround();
        }

        private bool CheckGround()
        {
            return Physics.CheckSphere(transform.position, .2f, _groundLayer, QueryTriggerInteraction.Ignore);
        }
    }
}