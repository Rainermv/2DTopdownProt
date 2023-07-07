using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterComponent : MonoBehaviour
    {
        public Transform HeadRotatorTransform;
        public Collider2D BottomCollider;

        private Bounds _bounds;

        public float MoveSpeed { get; set; }
        public Vector2 MoveDirection { get; set; }
        public List<IWeapon> Weapons { get; } = new();
        public Vector2 LookTarget { get; set; }


        public Action OnUpdate { get; set; }
        public Action OnCharacterDeath { get; set; }

        public Action<Vector3> OnMove { get; set; }
        public Action<Collider2D> OnCharacterTriggerEnter { get; set; }


        public int Health { get; set; }

        void Awake()
        {
            _bounds = BottomCollider.bounds;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        void Update()
        {
            OnUpdate?.Invoke();

            if (Health < 0)
                OnCharacterDeath?.Invoke();
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            LookAt(LookTarget);

            if (MoveDirection != Vector2.zero)
                MoveButCheckCollisionFirst(MoveDirection, Time.fixedDeltaTime);


        }

        private void MoveButCheckCollisionFirst(Vector2 moveVector, float time)
        {

            var position = transform.position;
            var moveDistance = MoveSpeed * time;
            var movePosition = (Vector3)(moveVector * moveDistance);

            Debug.DrawRay(position, moveVector, Color.blue);

            var contactFilter = new ContactFilter2D();
            contactFilter.NoFilter();

            var hits = Physics2D.BoxCastAll(position, 
                _bounds.extents, 
                0, 
                moveVector, 
                moveDistance);

            if (hits.Any(h => h.collider != BottomCollider)) 
                return;

            transform.Translate(movePosition);
            OnMove?.Invoke(transform.position);


        }

        // static method?
        public void LookAt(Vector2 target)
        {
            Debug.DrawLine(HeadRotatorTransform.position, target, Color.magenta);

            var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            HeadRotatorTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void UseWeapons()
        {
            foreach (var weapon in Weapons)
            {
                weapon.Use(LookTarget);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            OnCharacterTriggerEnter?.Invoke(other);
        }

    }
}
