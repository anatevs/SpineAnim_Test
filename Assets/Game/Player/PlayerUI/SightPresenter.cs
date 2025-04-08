using Assets.Input;
using System;
using UnityEngine;

namespace GameCore
{
    public class SightPresenter : 
        IDisposable
    {
        public event Action<Vector2> OnDirectionChanged;

        public bool IsConstrained
        {
            get => _isConstrained;
            set => _isConstrained = value;
        }

        private readonly SightView _view;

        private readonly InputHandler _input;

        private bool _isConstrained;

        private Vector2 _constraintDirection;

        public SightPresenter(InputHandler input, SightView view)
        {
            _input = input;
            _view = view;
        }

        public void Show()
        {
            _view.gameObject.SetActive(true);

            _input.OnDragging += ProcessDragging;
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);

            _input.OnDragging -= ProcessDragging;
        }

        void IDisposable.Dispose()
        {
            _input.OnDragging -= ProcessDragging;
        }

        public void SetConstrainDirection(Vector2 direction)
        {
            _constraintDirection = direction;
        }

        private void ProcessDragging(Vector2 position)
        {
            var lineDirection = _view.CalculateLine(position);

            OnDirectionChanged?.Invoke(-lineDirection);

            if (!_isConstrained)
            {
                _view.SetupLine(position);
                return;
            }

            _view.SetupLineDirection(_constraintDirection);
        }
    }
}