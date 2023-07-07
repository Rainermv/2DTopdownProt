using System;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public static  class PlayerInputEvents
    {
        public static Action<InputAction.CallbackContext> OnActionTriggered;

    }
}