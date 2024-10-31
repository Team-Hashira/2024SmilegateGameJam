using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Crogen.PowerfulInput
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Crogen/InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions
    {
        #region Input Event
        public event Action<Vector3> OnMoveEvent;
        public event Action<Vector2> OnMosueDeltaEvent;
        public event Action<bool> OnMouseLeftDownEvent;
        public event Action<bool> OnMouseRightDownEvent;
        public event Action OnDashEvent;
        public event Action OnAttackEvent;
        public event Action<bool, Vector2> OnLeftMouseClickEvent;
        public event Action<bool, Vector2> OnRightMouseClickEvent;
        public event Action<Vector2> OnMouseMoveEvent;
        #endregion

        public Vector2 MousePos { get; private set; }

        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDashEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            OnMoveEvent?.Invoke(context.ReadValue<Vector3>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackEvent?.Invoke();
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
        }

        public void OnMouseLeftClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnMouseLeftDownEvent?.Invoke(true);
            if (context.canceled)
                OnMouseLeftDownEvent?.Invoke(false);
        }

        public void OnMouseRightClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnMouseRightDownEvent?.Invoke(true);
            if (context.canceled)
                OnMouseRightDownEvent?.Invoke(false);
        }

        public void OnMosueDelta(InputAction.CallbackContext context)
        {
            OnMosueDeltaEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLeftMouse(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnLeftMouseClickEvent?.Invoke(true, MousePos);
            else if(context.canceled)
                OnLeftMouseClickEvent?.Invoke(false, MousePos); 
        }

        public void OnRightMouse(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnRightMouseClickEvent?.Invoke(true, MousePos);
            else if (context.canceled)
                OnRightMouseClickEvent?.Invoke(false, MousePos);
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
            MousePos = context.ReadValue<Vector2>();
            OnMouseMoveEvent?.Invoke(MousePos);
        }
    }
}