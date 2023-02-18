using Cinemachine;
using UnityEngine;

namespace RiaShooter.Scripts.Level
{
    internal class LevelStartUp : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private CinemachineSmoothPath _moveCameraPath;
        [SerializeField] private CinemachineSmoothPath _lookCameraPath;

        private void Awake()
        {
            var player = _playerSpawner.SpawnPlayer();
            SetCinemachineTrajectory(player.position);
        }

        private void SetCinemachineTrajectory(Vector3 endPoint)
        {
            _moveCameraPath.m_Waypoints[_moveCameraPath.m_Waypoints.Length - 1].position = endPoint;
            _moveCameraPath.InvalidateDistanceCache();
            _lookCameraPath.m_Waypoints[_lookCameraPath.m_Waypoints.Length - 2].position = endPoint;
            _lookCameraPath.InvalidateDistanceCache();
        }
    }
}
