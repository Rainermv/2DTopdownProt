using System;
using Unity.VisualScripting;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Assets.Scripts
{
    public class ProjectileComponent : SerializedMonoBehaviour, ICollidable
    {

        [SerializeField, ChildGameObjectsOnly]
        public Rigidbody2D RigidBody { get; set; }


        private Action<Collision2D> _onProjectileCollistionEnter;
        public bool Alive { get; set; }
        public int Damage { get; set; }

        public void Fire(Vector2 direction, float projectileForce, Action<Collision2D> onProjectileCollistionEnter,
            int damage)
        {
            _onProjectileCollistionEnter = onProjectileCollistionEnter;
            Alive = true;
            RigidBody.AddForce(direction * projectileForce);
            Damage = damage;
        }


        void LateUpdate()
        {
            if (!Alive)
                Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            _onProjectileCollistionEnter(collision);

        }


    }
}