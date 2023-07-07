using UnityEngine;

namespace Assets.Scripts
{
    public interface ICollidable
    {

        Rigidbody2D RigidBody { get; set; }
    }
}