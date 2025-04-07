using Assets.Input;
using System;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class Player : MonoBehaviour
    {
        public event Action<float, float> OnSightChanged;

        [SerializeField]
        private BodyTargetRotation _bodyTargetRotation;

        [SerializeField]
        private FlyTrajectory _flyTrajectory;

        [SerializeField]
        private float[] _targetAnglesRange = new float[2] {80, -40};

        private float[] _targetTanRange;

        private Vector2[] _directionsRange;

        private InputHandler _input;

        private SightPresenter _sightPresenter;

        private float _defaultSightLength = 1f;

        private float _defaultWeaponDistance = 5f;

        private Vector2 _weaponDirection;

        [Inject]
        private void Construct(InputHandler input,
            SightPresenter sightPresenter)
        {
            _input = input;
            _sightPresenter = sightPresenter;

            SetupConstraints();
        }

        private void OnEnable()
        {
            _sightPresenter.Show();

            _sightPresenter.OnDirectionChanged += ProcessSight;
        }

        private void OnDisable()
        {
            _sightPresenter.OnDirectionChanged -= ProcessSight;

            _sightPresenter.Hide();
        }

        private void SetupConstraints()
        {
            _targetTanRange = new float[_targetAnglesRange.Length];
            _directionsRange = new Vector2[_targetAnglesRange.Length];
            for (int i = 0; i < _targetTanRange.Length; i++)
            {
                _targetTanRange[i] = Mathf.Tan(_targetAnglesRange[i] * Mathf.Deg2Rad);
                _directionsRange[i] = (new Vector2(1, _targetTanRange[i])).normalized;
            }
        }

        private void ProcessSight(Vector2 direction)
        {
            _weaponDirection = direction;

            var x = direction.x;

            if (direction.x == 0)
            {
                x = 0.01f;
            }

            var tan = direction.y / x;

            if (tan > _targetTanRange[0])
            {
                tan = _targetTanRange[0];
                _weaponDirection = _directionsRange[0];

                _sightPresenter.IsConstrained = true;
                _sightPresenter.SetConstrainDirection(_weaponDirection);
            }

            else if (tan < _targetTanRange[1])
            {
                tan = _targetTanRange[1];
                _weaponDirection = _directionsRange[1];

                _sightPresenter.IsConstrained = true;
                _sightPresenter.SetConstrainDirection(_weaponDirection);
            }

            else
            {
                _sightPresenter.IsConstrained = false;
            }

            _bodyTargetRotation.RotateBody(_weaponDirection);

            var distanceX = direction.magnitude / _defaultSightLength * _defaultWeaponDistance;

            Debug.Log(distanceX);

            _flyTrajectory.UpdateTrajectory(tan, distanceX);
        }
    }
}