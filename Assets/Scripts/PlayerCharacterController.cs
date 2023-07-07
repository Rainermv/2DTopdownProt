using System;
using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    internal class PlayerCharacterController
    {
        private readonly PlayerInputController _playerInputController;
        private readonly CharacterComponent _playerCharacterComponent;

        public CharacterComponent PlayerCharacter => _playerCharacterComponent;


        public PlayerCharacterController(PlayerInputController playerInputController,
            CharacterComponent playerCharacterComponent, ProjectileComponent projectilePrefab)
        {
            _playerInputController = playerInputController;
            _playerCharacterComponent = playerCharacterComponent;

            _playerCharacterComponent.MoveSpeed = 2f;

            _playerCharacterComponent.Weapons.Add(new RangedWeapon(
                _playerCharacterComponent,
                projectilePrefab,
                100f, 1));

            // Set global event (player character moved) to this character component onMove
            _playerCharacterComponent.OnMove += vector3 =>
            {
                LevelEvents.OnPlayerCharacterMoved(vector3);
            };

            playerInputController.Initialize();

            PlayerInputEvents.OnActionTriggered += OnPlayerActionTriggered;

        }


        private void OnPlayerActionTriggered(InputAction.CallbackContext callbackContext)
        {
            switch (callbackContext.action.name)
            {
                case "Move" when callbackContext.action.IsPressed():
                    _playerCharacterComponent.MoveDirection = callbackContext.action.ReadValue<Vector2>();
                    return;
                case "Move":
                    _playerCharacterComponent.MoveDirection = Vector2.zero;
                    break;

                case "Point":
                    var pointerScreenPosition = callbackContext.action.ReadValue<Vector2>() ;
                    var pointerWorldPosition = Camera.main.ScreenToWorldPoint(pointerScreenPosition);
                    _playerCharacterComponent.LookTarget = pointerWorldPosition;
                    break;

                case "Fire":
                    _playerCharacterComponent.UseWeapons();
                    break;

            }

        }
    }

    internal static class LevelEvents
    {
        public static Action<Vector3> OnPlayerCharacterMoved;

        public static Action<CharacterComponent, Collider2D> OnCharacterTriggerEnter;

        public static Action<EnemyController, CharacterComponent> OnEnemyCharacterDeath;

    }
}