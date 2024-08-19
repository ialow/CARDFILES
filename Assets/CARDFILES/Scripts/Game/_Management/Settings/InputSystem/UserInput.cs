using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.System
{
    [CreateAssetMenu(menuName = "Scriptable object/Input")]
    public class UserInput : ScriptableObject, CardInput.IGameplayActions
    {
        private CardInput userInput;

        private void OnEnable()
        {
            if (userInput is null)
            {
                userInput = new CardInput();
                userInput.Gameplay.SetCallbacks(this);
            }
        }

        public event Func<Vector2, bool> ConditionTrackingOffsetMouseEvent;

        //public event Action<float> ScaleEvent;
        public event Action<Vector2, Vector2> OffsetMouseEvent;
        public event Action FocusOnPointEvent;

        public void OnGameplay()
        {
            userInput.Gameplay.Enable();
        }

        public void OnPause()
        {
            userInput.Gameplay.Disable();
        }

        public void OnWheel(InputAction.CallbackContext context)
        {
            if (ConditionTrackingOffsetMouseEvent?.Invoke(InputParam.CURRENT_MOUSE_POS) == true)
            {
                if (context.phase == InputActionPhase.Performed)
                    InputParam.WHEEL_IS_HOLDING = true;
            }

            if (context.phase == InputActionPhase.Canceled)
                InputParam.WHEEL_IS_HOLDING = false;
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            InputParam.CURRENT_MOUSE_POS = context.ReadValue<Vector2>();

            if (InputParam.WHEEL_IS_HOLDING)
                OffsetMouseEvent?.Invoke(context.ReadValue<Vector2>(), InputParam.PREVIOUS_MOUSE_POS);
            InputParam.PREVIOUS_MOUSE_POS = InputParam.CURRENT_MOUSE_POS;
        }

        public void OnScale(InputAction.CallbackContext context)
        {
            //if (context.phase == InputActionPhase.Performed)
            //    Debug.Log(context.ReadValue<float>());
            //ScaleEvent?.Invoke(context.ReadValue<float>());
        }

        public void OnFocusOnPoint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                FocusOnPointEvent?.Invoke();
        }
    }
}
