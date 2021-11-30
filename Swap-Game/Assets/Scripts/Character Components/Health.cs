using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwapGame.CharacterComponents
{
    /// <summary>
    /// Handles health for a character
    /// </summary>
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health;

        public int CurrentHealth
        {
            get
            {
                return _health;
            }
            set
            {
                Debug.Log($"Dealt {gameObject} {_health - value} damage");

                _health = value;

                if (_health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }
    }
}
