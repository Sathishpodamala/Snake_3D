using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Alpha
{
    public class InputManager : MonoBehaviour
    {
        #region Variables
        private Vector2 inputValue;
        private Input input;
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }
        #endregion Variables

        #region Unity Methods
        private void Awake()
        {
            input = new Input();
        }

        private void OnDisable()
        {
            input.Snake.Move.performed -= OnMovePerformed;
            input.Snake.Move.canceled -= OnMoveCanceled;
            input.Snake.Enable();
            input.Disable();
        }

        private void OnEnable()
        {
            input.Enable();
            input.Snake.Enable();
            input.Snake.Move.performed += OnMovePerformed;
            input.Snake.Move.canceled += OnMoveCanceled;
        }

        private void OnMoveCanceled(InputAction.CallbackContext obj)
        {
            inputValue = obj.ReadValue<Vector2>();
            SetInputValues();
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            inputValue = obj.ReadValue<Vector2>();
            SetInputValues();
        }

        #endregion Unity Methods

        #region Public Methods
        #endregion Public Methods

        #region Private Methods
        private void SetInputValues()
        {
            HorizontalInput = inputValue.x;
            VerticalInput = inputValue.y;
        }
        #endregion Private Methods

        #region Callbacks
        #endregion Callbacks

    }
}

