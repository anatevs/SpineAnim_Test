using Assets.Input;
using System;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class SightPresenter
    {
        public event Action<Vector2> OnDirectionChanged;

        private readonly SightView _view;

        private readonly InputHandler _input;

        [Inject]
        public SightPresenter(SightView view, InputHandler input)
        {
            _view = view;
            _input = input;
        }

        public void Show()
        {
            _view.gameObject.SetActive(true);

            _input.OnDragging += _view.SetupLine;

            _view.OnDrawLine += ChangeSight;
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);

            _input.OnDragging -= _view.SetupLine;

            _view.OnDrawLine -= ChangeSight;
        }

        private void ChangeSight(Vector2 sightVector)
        {
            OnDirectionChanged?.Invoke(-sightVector);
        }
    }
}