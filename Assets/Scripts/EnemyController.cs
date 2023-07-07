using System.Collections.Generic;
using Assets.Scripts.Behavior;
using Assets.Scripts.Weapons;

namespace Assets.Scripts
{
    internal class EnemyController
    {
        private readonly CharacterComponent _enemyCharacterComponent;

        public EnemyController(CharacterComponent enemyCharacterComponent)
        {
            _enemyCharacterComponent = enemyCharacterComponent;

            _enemyCharacterComponent.Weapons.Add(new MelleWeapon());
            _enemyCharacterComponent.MoveSpeed = 0.5f;

            _enemyCharacterComponent.OnCharacterTriggerEnter += collider2D => LevelEvents.OnCharacterTriggerEnter(_enemyCharacterComponent, collider2D);
            _enemyCharacterComponent.OnCharacterDeath += () => LevelEvents.OnEnemyCharacterDeath(this, _enemyCharacterComponent);
            
            Behaviors.Add(new FollowPlayerBehavior(_enemyCharacterComponent));

        }

        public List<IBehavior> Behaviors { get; } = new();

        public void SetActive(bool active)
        {
            _enemyCharacterComponent.gameObject.SetActive(active);

            foreach (var behavior in Behaviors)
            {
                behavior.Active = active;
            }
        }
    }
}