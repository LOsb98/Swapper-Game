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

        public void TryNewAttack(Vector3 aimDirection)
        {
            if (_attackTimer > 0)
            {
                return;
            }

            Debug.Log("Firing projectile");
            Vector3 projectileSpawnPos = transform.position + (aimDirection * _projectileSpawnDistance);
            GameObject newProjectile = Instantiate(_projectilePrefab, projectileSpawnPos, Quaternion.Euler(0f, 0f, 0f));

            Projectile projectileScript = newProjectile.GetComponent<Projectile>();
            projectileScript.AssignMoveDirection(aimDirection);
            projectileScript._projectileOwner = gameObject;

            _attackTimer = _attackDelay;
        }
    }
}
