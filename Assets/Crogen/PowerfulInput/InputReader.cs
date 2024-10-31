using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Crogen.PowerfulInput
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Crogen/InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions
    {
        #region Input Event

        public event Action<Vector3> MoveEvent;
        public event Action<Vector2> MosueDelta;
        public event Action<bool> MouseLeftDown;
        public event Action<bool> MouseRightDown;
        public event Action DashEvent;
        public event Action AttackEvent;

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
            if(context.performed)
                DashEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector3>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                AttackEvent?.Invoke();
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            MousePos = context.ReadValue<Vector2>();
        }

        public void OnMouseLeftClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                MouseLeftDown?.Invoke(true);
            if (context.canceled)
                MouseLeftDown?.Invoke(false);
        }

        public void OnMouseRightClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                MouseRightDown?.Invoke(true);
            if (context.canceled)
                MouseRightDown?.Invoke(false);
        }

        public void OnMosueDelta(InputAction.CallbackContext context)
        {
            MosueDelta?.Invoke(context.ReadValue<Vector2>());
        }
    }
}