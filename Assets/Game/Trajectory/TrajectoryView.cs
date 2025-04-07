using UnityEngine;

namespace GameCore
{
    public sealed class TrajectoryView : MonoBehaviour
    {
        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private GameObject _pointPrefab;

        [SerializeField]
        private float _lastPointSizeRatio = 0.5f;

        [SerializeField]
        private int _amount = 12;

        [SerializeField]
        private int _centerNumber = 10;

        private float _startPointX;

        private Transform _spawnPoint;

        private Transform[] _points;

        public void Init(Transform spawnPoint, float startXShift)
        {
            _spawnPoint = spawnPoint;
            _startPointX = startXShift;

            _points = new Transform[_amount];

            var scaleIndex = (1 - _lastPointSizeRatio) / (_amount - 1);

            for (int i = 0; i < _amount; i++)
            {
                var sizeRatio = 1 - scaleIndex * i;

                var point = Instantiate(_pointPrefab).transform;

                var sprite = point.GetComponentInChildren<SpriteRenderer>();

                sprite.transform.localScale = new Vector3(sizeRatio, sizeRatio, sizeRatio);

                point.SetParent(_parent);

                _points[i] = point;
            }

        }

        public void UpdatePoints(float distanceX, ParabolaCalculation parabola)
        {
            var deltaX = (distanceX / 2 - _startPointX) / _centerNumber;

            _parent.position = _spawnPoint.position;


            for (int i = 0; i < _points.Length; i++)
            {
                var x = i * deltaX + _startPointX;
                //var y = parabola.GetParabolaPointY(x - distanceX/2);
                var y = parabola.GetParabolaPointY(x);

                _points[i].localPosition = new Vector3(x, y);
            }
        }
    }
}