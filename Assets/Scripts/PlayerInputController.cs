using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Assets.Scripts
{
    public class PlayerInputController : MonoBehaviour
    {
        public PlayerInput pInput;

        //public Action<AxisEventData> OnEventMove;

        public void Initialize()
        {
            
            pInput.onActionTriggered += context =>
            {
                PlayerInputEvents.OnActionTriggered(context);

            };



        }

    }
}