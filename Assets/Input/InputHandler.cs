using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Assets.Input
{
    public class InputHandler :
        IInitializable,
        IDisposable
    {
        public event Action OnDragStarted;

        public event Action<Vector2> OnDragging;

        public event Action OnDropped;

        private readonly GameControls _controls;

        private Camera _camera;

        public InputHandler()
        {
            _controls = new GameControls();
        }

        void IInitializable.Initialize()
        {
            _camera = Camera.main;

            _controls.Enable();

            _controls.Gameplay.Drag.started += StartDragging;
            _controls.Gameplay.Drag.performed += FollowPointer;
            _controls.Gameplay.Drag.canceled += ReleasePointer;
        }

        void IDisposable.Dispose()
        {
            _controls.Gameplay.Drag.started -= StartDragging;
            _controls.Gameplay.Drag.performed -= FollowPointer;
            _controls.Gameplay.Drag.canceled -= ReleasePointer;
        }

        private void StartDragging(InputAction.CallbackContext context)
        {
            OnDragStarted?.Invoke();
        }

        private void FollowPointer(InputAction.CallbackContext context)
        {
            var screenPos = context.ReadValue<Vector2>();

            var clickPos = _camera.ScreenToWorldPoint(screenPos);

            OnDragging?.Invoke(clickPos);
        }

        private void ReleasePointer(InputAction.CallbackContext context)
        {
            OnDropped?.Invoke();
        }
    }
}