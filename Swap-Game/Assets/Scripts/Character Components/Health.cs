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
        [SerializeField] private CharacterManager _characterManager;

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
                    //Check whether the character dying is the player or AI controlled and do its death logic
                    _characterManager.CurrentInput.Die();
                    gameObject.SetActive(false);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }
    }
}
