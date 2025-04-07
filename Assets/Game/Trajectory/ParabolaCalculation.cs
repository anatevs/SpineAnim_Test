using UnityEngine;

namespace GameCore
{
    public sealed class ParabolaCalculation
    {
        private float _aParabolicCoef;

        private float _cParabolicCoef;

        public float GetParabolaPointY(float pointX)
        {
            return _aParabolicCoef * Mathf.Pow(pointX, 2) + _cParabolicCoef;
        }

        public float GetTangent(float pointX)
        {
            return 2 * _aParabolicCoef * pointX;
        }

        public void UpdateParabolaParams(float tanTheta, float distanceX)
        {
            _aParabolicCoef = -tanTheta / distanceX;

            _cParabolicCoef = tanTheta * distanceX / 4;
        }
    }
}