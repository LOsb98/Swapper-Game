using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.CharacterComponents;

namespace SwapGame.EntityComponents
{
    /// <summary>
    ///<para>Base class for projectiles.</para>
    ///<para>Projectiles are stored as prefabs instead of Scriptable Objects, as they will be created very frequently.</para>
    ///<para>Less expensive than passing in projectile properties every single time.</para>
    ///<para>Projectile stores information about the projectile and its properties.</para>
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        private Vector3 _moveVector;

        public float _hitboxSize;
        public float _speed;
        public int _damage;
        public float _lifeSpan;

        public void AssignMoveDirection(Vector3 direction)
        {
            //Control stick value SHOULD be normalised already
            _moveVector = direction;

            _moveVector *= _speed;
        }

        private void Update()
        {
            //We don't need a separate lifeSpanTimer value, since we don't reset this value at any point
            //The object will be destroyed once it hits 0
            if (_lifeSpan > 0)
            {
                _lifeSpan -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }

            transform.Translate(_moveVector * Time.deltaTime);
            CheckHitbox();
        }

        private void CheckHitbox()
        {
            Collider2D hitboxCheck = Physics2D.OverlapCircle(transform.position, _hitboxSize);

            if (hitboxCheck)
            {
                //Deal damage if we hit a character
                if (hitboxCheck.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(_damage);
                }

                //Regardless of what we hit, destroy the projectile
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _hitboxSize);
        }
    }
}
