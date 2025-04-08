using UnityEngine;

namespace GameCore
{
    public sealed class TrajectoryAndSpawn : MonoBehaviour
    {
        [SerializeField]
        private TrajectoryView _trajectoryView;

        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private Arrow _projectilePrefab;

        [SerializeField]
        private Transform _poolParent;

        private float _tanTheta = 0.5f;

        private float _distanceX = 5f;

        private readonly float _trajectoryStartXShift = 0.5f * 1.81f;

        private readonly ParabolaCalculation _parabola = new();

        private ArrowsPool _projectilesPool;

        private void Awake()
        {
            _trajectoryView.Init(_trajectoryStartXShift);

            ShowTrajectory(false);

            _projectilesPool = new ArrowsPool(_projectilePrefab, _poolParent);
        }

        public void ShowTrajectory(bool _isShow)
        {
            _trajectoryView.gameObject.SetActive(_isShow);
        }

        public void UpdateTrajectory(float tanTheta, float distanceX, float yFloor)
        {
            _parabola.UpdateParabolaParams(tanTheta, distanceX,
                (_spawnPoint.position.y - yFloor));

            _tanTheta = tanTheta;
            _distanceX = distanceX;

            transform.position = _spawnPoint.position;

            _trajectoryView.UpdatePoints(distanceX, _parabola);
        }

        public void ShootProjectile()
        {
            var projectile = _projectilesPool.Spawn(transform);

            projectile.StartFlying(_spawnPoint.position, _distanceX, _tanTheta, _parabola);
        }
    }
}