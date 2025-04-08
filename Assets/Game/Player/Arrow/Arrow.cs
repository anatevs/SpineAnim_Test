using System;
using UnityEngine;

namespace GameCore
{
    public sealed class Arrow : MonoBehaviour
    {
        public event Action<Arrow> OnAttackEnded;

        [SerializeField]
        private float _speedX = 1f;

        [SerializeField]
        private LayerMask _obstacleLayer;

        [SerializeField]
        private ArrowAnimation _arrowAnimation;

        private bool _isFlying;

        private Vector3 _spawnPoint;

        private float _tStart;

        private ParabolaCalculation _parabola;

        public void StartFlying(Vector3 spawnPoint, float distanceX, float tanTheta, ParabolaCalculation parabola)
        {
            _isFlying = true;

            _tStart = Time.time;

            _spawnPoint = spawnPoint;

            _parabola = parabola.Clone();

            SetRotation(Mathf.Atan(tanTheta) * Mathf.Rad2Deg);
        }

        private void Update()
        {
            if (_isFlying)
            {
                var x = (Time.time - _tStart) * _speedX;

                var y = _parabola.GetParabolaPointY(x);

                var theta = Mathf.Atan(_parabola.GetTangent(x)) * Mathf.Rad2Deg;

                SetPosition(new Vector3(x, y, transform.position.z));
                SetRotation(theta);
            }
        }

        private void SetRotation(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void SetPosition(Vector3 pos)
        {
            transform.position = _spawnPoint + pos;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collisionLayer = 1 << collision.gameObject.layer;

            if ((collisionLayer & _obstacleLayer.value) > 0)
            {
                _isFlying = false;

                _arrowAnimation.SetAttack();

                _arrowAnimation.OnAttackEnded += EndAttack;
            }
        }

        private void EndAttack()
        {
            OnAttackEnded?.Invoke(this);

            _arrowAnimation.OnAttackEnded -= EndAttack;
        }
    }
}