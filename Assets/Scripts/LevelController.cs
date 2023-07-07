using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behavior;
using Assets.Scripts.Weapons;
using UnityEditor.SearchService;
using UnityEngine;

namespace Assets.Scripts
{
    internal class LevelController
    {
        private List<EnemyController>_enemyControllers;

        public LevelController(CharacterComponent enemyCharacterPrefab, Transform[] enemySpawners)
        {
            _enemyControllers = new List<EnemyController>();

            foreach (var enemySpawner in enemySpawners)
            {

                //todo: should probably be in a factory
                var enemyComponent =
                    CharacterComponent.Instantiate(enemyCharacterPrefab, enemySpawner.position, Quaternion.identity);
                
                // the idea here is so I can change behaviors when necessary, the controller being a "container"
                // the controller should probably have multiple behaviors, and it would change between then (state machine)
                var enemyController = new EnemyController(enemyComponent);
                
                _enemyControllers.Add(enemyController);


            }

            LevelEvents.OnCharacterTriggerEnter += HandleCharacterTriggerEnter;
            LevelEvents.OnEnemyCharacterDeath += HandleEnemyCharacterDeath;
        }

        private void HandleEnemyCharacterDeath(EnemyController controller, CharacterComponent component)
        {
            controller.SetActive(false);
        }

        private void HandleCharacterTriggerEnter(CharacterComponent character, Collider2D otherCollider)
        {
            var projectile = otherCollider.GetComponent<ProjectileComponent>();
            if (projectile != null)
                HandleProjectileHitCharacter(character, projectile);
        }

        private void HandleProjectileHitCharacter(CharacterComponent character, ProjectileComponent projectile)
        {
            character.Health -= projectile.Damage;
            projectile.Alive = false;
        }


        public void Initialize()
        {
            foreach (var enemyController in _enemyControllers)
            {
                // Set all primary behaviors enabled
                enemyController.Behaviors.First().Active = true;
            }
        }
    }
}