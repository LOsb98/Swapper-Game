using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.CharacterComponents;
using SwapGame.ScriptableObjects;

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
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private List<GameObject> _hitCharacterList; //Stopping the same projectile from hitting a character multiple times, want projectiles to be piercing

        public Vector2 _moveVector;

        public GameObject _projectileOwner;

        public void InitializeProjectile(ProjectileData projectileData, GameObject projectileOwner)
        {
            _hitCharacterList.Clear();

            _hitboxSize = projectileData._hitboxSize;
            _speed = projectileData._speed;
            _damage = projectileData._damage;
            _lifeSpan = projectileData._lifeSpan;
            _spriteRenderer.color = projectileData._colour;

            _projectileOwner = projectileOwner;
        }

        public void RotateProjectile(Vector2 direction)
        {
            _moveVector = direction;
            Debug.Log("Projectile direction: " + direction);

            _moveVector *= _speed;

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
                gameObject.SetActive(false);
                return;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _hitboxSize);
        }
    }
}
