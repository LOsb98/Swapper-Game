using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwapGame.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Characters/Character Group")]
    public class CharacterGroup : ScriptableObject
    {
        /// <summary>
        /// List of the characters in this group
        /// </summary>
        public Character[] _list;
    }
}
