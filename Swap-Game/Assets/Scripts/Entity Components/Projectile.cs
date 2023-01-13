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
        [SerializeField] private float _hitboxSize;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private float _lifeSpan;
        [SerializeField] private List<GameObject> _hitCharacterList;
        [SerializeField] private bool _isMeleeAttack;

        private Vector3 _moveVector;

        public GameObject _projectileOwner;

        public void AssignMoveDirection(Vector3 direction)
        {
            _moveVector = direction;
            //if (_isMeleeAttack)
            //{
            //    _moveVector -= currentDirection.normalized;
            //}

            _moveVector *= _speed;

            //If the attack is melee we want it to move at a speed consistent with the player
            //It looks strange when the player moves as fast as the melee attack
            //Also gives it more range

            //Rotating the projectile to its move direction
            //Uses right as the forward direction, so apply sprites accordingly
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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

            transform.Translate(_moveVector * Time.deltaTime, Space.World);
            CheckHitbox();
        }

        private void CheckHitbox()
        {
            Collider2D hitboxCheck = Physics2D.OverlapCircle(transform.position, _hitboxSize);

            //Hit nothing
            if (!hitboxCheck)
            {
                return;
            }

            GameObject hitObject = hitboxCheck.gameObject;

            //Stop projectiles hitting the character who fired them
            if (hitObject == _projectileOwner)
            {
                return;
            }

            //Projectiles are piercing so need to stop them hitting the same character multiple times
            if (_hitCharacterList.Contains(hitObject))
            {
                return;
            }

            //Deal damage if we hit a character
            //Add them to the list of characters the projectile has hit
            if (hitObject.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(_damage);
                _hitCharacterList.Add(hitObject);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _hitboxSize);
        }
    }
}
