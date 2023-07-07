using UnityEngine;

namespace Assets.Scripts
{
    public class GameLoader : MonoBehaviour
    {
        
        public PlayerInputController PlayerInputController;

        public CharacterComponent PlayerCharacterComponent;

        public ProjectileComponent BulletPrefab;

        public CharacterComponent EnemyCharacterPrefab;
        public Transform[] EnemySpawners;

        void Start()
        {
            Physics2D.gravity = Vector2.zero;

            var playerCharacterController = new PlayerCharacterController(PlayerInputController,
                PlayerCharacterComponent,
                BulletPrefab);

            var levelController = new LevelController(EnemyCharacterPrefab,
                EnemySpawners);


            levelController.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}