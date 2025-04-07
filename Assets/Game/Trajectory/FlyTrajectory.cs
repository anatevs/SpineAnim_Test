using UnityEngine;

namespace GameCore
{
    public sealed class FlyTrajectory : MonoBehaviour
    {
        [SerializeField]
        private TrajectoryView _trajectoryView;

        //[SerializeField]
        private float _tanTheta = 0.5f;

        //[SerializeField]
        private float _distanceX = 5f;

        [SerializeField]
        private float _speedX = 1f;

        [SerializeField]
        private bool _isFlying;

        [SerializeField]
        private Transform _spawnPoint;

        private readonly float _trajectoryStartXShift = 0.5f * 1.81f;

        private ParabolaCalculation _parabola = new();

        [SerializeField]
        private bool _isSetFly;

        private void Awake()
        {
            _trajectoryView.Init(_spawnPoint, _trajectoryStartXShift);
        }

        private void Update()
        {
            if (_isSetFly)
            {
                _isSetFly = false;

                //StartFlying();
            }
        }

        public void UpdateTrajectory(float tanTheta, float distanceX)
        {
            _parabola.UpdateParabolaParams(tanTheta, distanceX);

            _tanTheta = tanTheta;
            _distanceX = distanceX;

            _trajectoryView.UpdatePoints(distanceX, _parabola);
        }
    }
}