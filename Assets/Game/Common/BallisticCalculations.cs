using UnityEngine;

namespace GameCore
{
    public static class BallisticCalculations
    {
        private static float _aParabolicCoef;

        private static float _cParabolicCoef;

        public static float GetParabolaPointY(float pointX)
        {
            return _aParabolicCoef * Mathf.Pow(pointX, 2) + _cParabolicCoef;
        }

        public static float GetTangentCos(float pointX)
        {
            return 2 * _aParabolicCoef * pointX;
        }

        public static void SetParabolaParams(float tanTheta, float distanceX, float speedX)
        {
            _aParabolicCoef = -tanTheta / distanceX;

            _cParabolicCoef = tanTheta / 4;
        }
    }
}