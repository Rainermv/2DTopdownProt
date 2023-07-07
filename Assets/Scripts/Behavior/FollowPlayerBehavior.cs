using UnityEngine;

namespace Assets.Scripts.Behavior
{
    internal class FollowPlayerBehavior : IBehavior
    {
        private readonly CharacterComponent _characterComponent;
        private bool _enabled;

        public FollowPlayerBehavior(CharacterComponent characterComponent)
        {
            _characterComponent = characterComponent;
            
        }

        public bool Active
        {
            get => _enabled;
            set
            {
                _enabled = value;

                if (_enabled)
                {
                    LevelEvents.OnPlayerCharacterMoved += SetCharacterMoveTargetTo;
                    return;
                }

                LevelEvents.OnPlayerCharacterMoved -= SetCharacterMoveTargetTo;
            }
        }

        private void SetCharacterMoveTargetTo(Vector3 vector3)
        {
            _characterComponent.LookTarget = vector3;

            var direction = vector3 - _characterComponent.transform.position;
            direction.Normalize();
            _characterComponent.MoveDirection = direction;
            return;
        }
    }
}