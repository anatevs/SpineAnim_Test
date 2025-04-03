using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class FlyTrajectory : MonoBehaviour
    {
        [SerializeField]
        private float _tanTheta = 0.5f;

        [SerializeField]
        private float _distanceX = 5f;

        [SerializeField]
        private float _speedX = 1f;

        private Vector3 _initPos;

        private float _tStart;

        [SerializeField]
        private bool _isFlying;

        public void SetInitPos(Vector3 initPos)
        {
            _initPos = initPos;
        }

        private void Awake()
        {
            SetInitPos(transform.position);


            Debug.Log($"{Mathf.Atan(_tanTheta) * Mathf.Rad2Deg}, {Mathf.Acos(0) * Mathf.Rad2Deg}");
        }

        [SerializeField]
        private bool _isSetFly;

        private void Update()
        {
            if (_isSetFly)
            {
                _isSetFly = false;

                _isFlying = true;

                _tStart = Time.time;

                SetupTarget();

                SetRotation(Mathf.Atan(_tanTheta) * Mathf.Rad2Deg);
            }

            if (_isFlying)
            {
                var x = (Time.time - _tStart) * _speedX - _distanceX / 2;

                var y = BallisticCalculations.GetParabolaPointY(x);

                var theta = Mathf.Atan(BallisticCalculations.GetTangentCos(x)) * Mathf.Rad2Deg;

                SetPosition(new Vector3(x + _distanceX/2, y, transform.position.z));
                SetRotation(theta);
            }
        }

        private void SetupTarget()
        {
            BallisticCalculations.SetParabolaParams(_tanTheta, _distanceX, _speedX);
        }

        private void SetRotation(float angle)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void SetPosition(Vector3 pos)
        {
            transform.position = _initPos + pos;
        }
    }
}