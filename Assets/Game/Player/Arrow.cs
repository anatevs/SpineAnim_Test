using UnityEngine;

namespace GameCore
{
    public sealed class Arrow : MonoBehaviour
    {
        [SerializeField]
        private float _speedX = 1f;

        [SerializeField]
        private bool _isFlying;

        private Transform _spawnPoint;

        private float _tStart;

        private ParabolaCalculation _parabola;
        private float _distanceX = 5f;

        private void Update()
        {
            if (_isFlying)
            {
                var x = (Time.time - _tStart) * _speedX - _distanceX / 2;

                var y = _parabola.GetParabolaPointY(x);

                var theta = Mathf.Atan(_parabola.GetTangent(x)) * Mathf.Rad2Deg;

                SetPosition(new Vector3(x + _distanceX / 2, y, transform.position.z));
                SetRotation(theta);
            }
        }

        public void StartFlying(Transform spawnPoint, float distanceX, float tanTheta, ParabolaCalculation parabola)
        {
            _isFlying = true;

            _tStart = Time.time;

            _spawnPoint = spawnPoint;

            _distanceX = distanceX;

            _parabola = parabola;

            SetRotation(Mathf.Atan(tanTheta) * Mathf.Rad2Deg);
        }

        private void SetRotation(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void SetPosition(Vector3 pos)
        {
            transform.position = _spawnPoint.position + pos;
        }
    }
}