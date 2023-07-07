using UnityEngine;

namespace Assets.Scripts.Weapons
{
    internal class RangedWeapon : IWeapon
    {
        private readonly CharacterComponent _parent;
        private readonly ProjectileComponent _projectilePrefab;
        private readonly float _force;

        public int WeaponDamage { get; set; }


        public RangedWeapon(CharacterComponent parent, ProjectileComponent projectilePrefab, float projectileForce, int weaponDamage)
        {
            WeaponDamage = weaponDamage;
            _parent = parent;
            _force = projectileForce;
            _projectilePrefab = projectilePrefab;
        }
        
        public void Use(Vector2 vector2)
        {
            var origin = _parent.HeadRotatorTransform.position;
            var direction = (vector2 - (Vector2)_parent.HeadRotatorTransform.position);
            direction.Normalize();
            var projectileComponent = ProjectileComponent.Instantiate(_projectilePrefab, origin, Quaternion.identity);

            projectileComponent.Fire(direction, _force, _ =>
            {
                projectileComponent.Alive = false;
            }, WeaponDamage);
        }

    }
}