using UnityEngine;

namespace GameCore
{
    public sealed class Arrow : MonoBehaviour
    {
        [SerializeField]
        private float _speedX = 1f;

        private Vector3 _spawnPoint;

        private float _tStart;

        private ParabolaCalculation _parabola;

        public void StartFlying(Vector3 spawnPoint, float distanceX, float tanTheta, ParabolaCalculation parabola)
        {
            _tStart = Time.time;

            _spawnPoint = spawnPoint;

            _parabola = parabola.Clone();

            SetRotation(Mathf.Atan(tanTheta) * Mathf.Rad2Deg);
        }

        private void Update()
        {
            var x = (Time.time - _tStart) * _speedX;

            var y = _parabola.GetParabolaPointY(x);

            var theta = Mathf.Atan(_parabola.GetTangent(x)) * Mathf.Rad2Deg;

            SetPosition(new Vector3(x, y, transform.position.z));
            SetRotation(theta);
        }

        private void SetRotation(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void SetPosition(Vector3 pos)
        {
            transform.position = _spawnPoint + pos;
        }
    }
}