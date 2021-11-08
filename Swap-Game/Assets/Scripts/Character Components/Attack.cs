using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.EntityComponents;

namespace SwapGame.CharacterComponents
{
    /// <summary>
    /// Handles whether the character is ready to attack, and spawns the projectile
    /// </summary>
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float _projectileSpawnDistance;

        private float _attackTimer;

        public float _attackDelay;

        public GameObject _projectilePrefab;

        private void Update()
        {
            if (_attackTimer > 0)
            {
                _attackTimer -= Time.deltaTime;
            }
        }

        public bool AttackReady()
        {
            if (_attackTimer > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void FireProjectile(Vector3 aimDirection)
        {
            Debug.Log("Firing projectile");
            Vector3 projectileSpawnPos = transform.position + (aimDirection * _projectileSpawnDistance);
            GameObject newProjectile = Instantiate(_projectilePrefab, projectileSpawnPos, Quaternion.Euler(0f, 0f, 0f));

            Projectile projectileScript = newProjectile.GetComponent<Projectile>();
            projectileScript.AssignMoveDirection(aimDirection);

            _attackTimer = _attackDelay;
        }
    }
}
