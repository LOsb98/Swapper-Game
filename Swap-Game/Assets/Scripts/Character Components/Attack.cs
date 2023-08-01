using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.EntityComponents;
using SwapGame.GameManagement;
using SwapGame.ScriptableObjects;

namespace SwapGame.CharacterComponents
{
    /// <summary>
    /// Handles whether the character is ready to attack, and spawns the projectile
    /// </summary>
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float _projectileSpawnDistance;

        private float _attackTimer;

        public ProjectileData _projectileData;

        public float _attackDelay;

        private void Update()
        {
            if (_attackTimer > 0)
            {
                _attackTimer -= Time.deltaTime;
            }
        }

        public void TryNewAttack(Vector2 aimDirection)
        {
            if (_attackTimer > 0)
            {
                return;
            }

            Debug.Log("Firing projectile");
            Vector2 projectileSpawnPos = transform.position.ConvertToVector2() + (aimDirection * _projectileSpawnDistance);

            ProjectileManager.Instance.SpawnProjectile(projectileSpawnPos, aimDirection, _projectileData, gameObject);

            _attackTimer = _attackDelay;
        }
    }
}
