using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwapGame.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Projectiles/New Projectile")]
    public class ProjectileData : ScriptableObject
    {
        public float _hitboxSize;
        public float _speed;
        public int _damage;
        public float _lifeSpan;
        public Sprite _sprite;
        public Color _colour; //Temporary until proper sprites are implemented
    }
}