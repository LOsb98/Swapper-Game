using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwapGame.ScriptableObjects
{
    /// <summary>
    /// <para>Data for character properties.</para>
    /// <para>Includes move speed, health and attack speed.</para>
    /// <para>Information about projectile properties is stored in its own class, makes them more modular.</para>
    /// </summary>
    [CreateAssetMenu(menuName = "Characters/New Character")]
    public class Character : ScriptableObject
    {
        //Characters were going to have separate types (melee/projectile)
        //It will be a lot easier to just implement "projectile" characters
        //Melee can be represented by very short-range projectiles
        //Like the sword beam from the original Legend of Zelda
        public Sprite _sprite;
        public int _health;
        public int _speed;
        public float _fireRate;
        public ProjectileData _projectile;
        public Vector2 _size;
    }
}