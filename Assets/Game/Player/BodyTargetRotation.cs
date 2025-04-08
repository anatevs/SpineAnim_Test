using UnityEngine;

namespace GameCore
{
    public sealed class BodyTargetRotation : MonoBehaviour
    {
        [SerializeField]
        private Transform _rotPoint;

        [SerializeField]
        private Transform[] _bodyTransform;

        private float _prevAngle;

        [SerializeField]
        private float _angle;

        [SerializeField]
        private bool _isRotate = false;

        public void Update()
        {
            if (_isRotate)
            {
                _isRotate = false;

                var dir = new Vector3(
                    Mathf.Cos(_angle * Mathf.Deg2Rad),
                    Mathf.Sin(_angle * Mathf.Deg2Rad),
                    0);

                RotateBody(dir);
            }
        }

        public void RotateToDefault()
        {
            RotateBody(0);
        }

        public void RotateBody(Vector2 direction)
        {
            var rotQ = Quaternion.FromToRotation(Vector3.right, direction);

            var angle = rotQ.eulerAngles.z;

            var deltaAngle = angle - _prevAngle;

            RotateBody(deltaAngle);

            _prevAngle = angle;
        }

        private void RotateBody(float angle)
        {
            foreach (Transform t in _bodyTransform)
            {
                t.RotateAround(_rotPoint.position, Vector3.forward, angle);
            }
        }
    }
}