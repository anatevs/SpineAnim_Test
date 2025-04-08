using UnityEngine;

namespace GameCore
{
    public sealed class ParabolaCalculation
    {
        private float _aParabolicCoef;

        private float _cParabolicCoef;

        private float _xShift;

        public ParabolaCalculation()
        {
        }

        private ParabolaCalculation(float aParabolicCoef, float cParabolicCoef, float xShift)
        {
            _aParabolicCoef = aParabolicCoef;
            _cParabolicCoef = cParabolicCoef;
            _xShift = xShift;
        }

        public float GetParabolaPointY(float pointX)
        {
            return _aParabolicCoef * Mathf.Pow((pointX + _xShift), 2) + _cParabolicCoef;
        }

        public float GetTangent(float pointX)
        {
            return 2 * _aParabolicCoef * (pointX + _xShift);
        }

        public void UpdateParabolaParams(float tanTheta, float distanceX, float yHight)
        {
            if (tanTheta >= 0)
            {
                _aParabolicCoef = -tanTheta / distanceX;

                _cParabolicCoef = tanTheta * distanceX / 4;

                _xShift = -distanceX / 2;
            }

            else
            {
                _aParabolicCoef = (yHight - distanceX * tanTheta) / distanceX / distanceX;

                var x0 = tanTheta / 2 / _aParabolicCoef;

                _cParabolicCoef = yHight - _aParabolicCoef * Mathf.Pow(x0 + distanceX, 2);

                _aParabolicCoef *= -1;
                _cParabolicCoef *= -1;

                _xShift = -x0;
            }
        }

        public ParabolaCalculation Clone()
        {
            return new ParabolaCalculation(
                _aParabolicCoef,
                _cParabolicCoef,
                _xShift
                );
        }
    }
}