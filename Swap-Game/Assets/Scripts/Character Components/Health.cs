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

        public void TakeDamage(int damage)
        {
            Debug.Log($"Hit {gameObject} for {damage} damage");
        }
    }
}
