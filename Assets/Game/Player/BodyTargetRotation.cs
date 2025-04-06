using UnityEngine;

namespace GameCore
{
    public sealed class BodyTargetRotation : MonoBehaviour
    {
        [SerializeField]
        private Transform _rotPoint;

        [SerializeField]
        private Transform[] _bodyTransform;

        [SerializeField]
        private float _angle;

        [SerializeField]
        private float _rotSpeed = 1f;

        [SerializeField]
        private bool _isRotate = false;

        public void Update()
        {
            if (_isRotate)
            {
                _isRotate = false;


                var dir = new Vector3(-Mathf.Sin(_angle * Mathf.Deg2Rad), Mathf.Cos(_angle * Mathf.Deg2Rad), 0);

                var rotQ = Quaternion.FromToRotation(Vector3.up, dir);

                var angle = rotQ.eulerAngles.z;

                RotateBody(angle);
            }
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