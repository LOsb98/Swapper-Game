using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.CharacterComponents;
using SwapGame.GameManagement;

namespace SwapGame.Inputs
{
    //This is the base class used for the player and AI inputs
    //The CharacterManager will switch between each
    public abstract class InputBase : MonoBehaviour
    {
        [SerializeField] protected Movement _movement;
        [SerializeField] protected Attack _attackScript;
        [SerializeField] protected Vector2 _moveDirection;

        private void OnValidate()
        {
            if (TryGetComponent<Attack>(out Attack attackScript))
            {
                _attackScript = attackScript;
            }
        }
        /// <summary>
        /// Step() is essentially the update function for the input classes
        /// </summary>
        public abstract void Step();

        /// <summary>
        /// When the player dies the game ends, when an AI dies the score is increased
        /// </summary>
        public abstract void Die();
    }
}
