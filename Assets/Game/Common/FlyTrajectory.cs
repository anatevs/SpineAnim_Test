using UnityEngine;

namespace GameCore
{
    public sealed class FlyTrajectory : MonoBehaviour
    {
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

        private float _tStart;

        private ParabolaCalculation _parabola = new();

        [SerializeField]
        private bool _isSetFly;

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

            //TODO: visualize trajectory
        }
    }
}