using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.ScriptableObjects;
using SwapGame.Inputs;
using SwapGame.StaticMethods;

namespace SwapGame.CharacterComponents
{
    /// <summary>
    /// Handles assigning character data to necessary components when a character is spawned
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private Character _currentCharacter;
        [SerializeField] private CharacterGroup[] _characterGroups;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Movement _movement;

        [SerializeField] private InputBase _playerInput;
        [SerializeField] private InputBase _aiInput;

        [SerializeField] private Attack _attackScript;
        [SerializeField] private Health _healthManager;

        private InputBase _currentInput;

        private void Awake()
        {
            if (_currentCharacter != null)
            {
                return;
            }

            int groupCount = _characterGroups.Length;
            int index = Random.Range(0, groupCount);
            CharacterGroup groupToSpawnFrom = _characterGroups[index];
            print($"Picked group {index}");

            _currentCharacter = SelectRandomCharacter(groupToSpawnFrom);

            gameObject.name = _currentCharacter.name;
        }

        private Character SelectRandomCharacter(CharacterGroup charGroup)
        {
            /*---Random Character selection---
             * For each character in the selected group:
             * Roll a boolean
             * If false:
             * Break loop, use current index
             * Else:
             * Increment index
             * Roll again
             * 
             * Characters should be ordered ascending in rarity in their group
             */

            int characterIndex = 0;

            int rollCount = charGroup._list.Length - 1;

            for (int i = 0; i < rollCount; i++)
            {
                if (MathsStuff.CoinToss())
                {
                    characterIndex++;
                }
                else
                {
                    break;
                }
            }

            Debug.Log($"Spawn value: {characterIndex}");

            Character finalCharacterSpawn = charGroup._list[characterIndex];

            Debug.Log($"Spawn character: {finalCharacterSpawn.name}");

            return finalCharacterSpawn;
        }

        private void Start()
        {
            //Sending character data to the relevant components here to avoid passing it to a bunch of component references/method parameters later
            //For attacks, things like fire rate and projectiles can be passed into the Attack script beforehand
            //The input scripts can then call a method to attempt an attack when necessary, all other logic is handled in the Attack script separately
            //May need to pass in an "aim direction" Vector2

            _healthManager.CurrentHealth = _currentCharacter._health;
            _spriteRenderer.sprite = _currentCharacter._sprite;
            _movement._speed = _currentCharacter._speed;
            _attackScript._attackDelay = _currentCharacter._fireRate;
            _attackScript._projectilePrefab = _currentCharacter._projectile;

            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.size = _currentCharacter._size;

            SetAIControl();
        }

        private void Update()
        {
            _currentInput.Step();
        }

        //Keeping this as a separate method that can be called from other scripts later
        public void SetPlayerControl()
        {
            _playerInput.enabled = true;
            _aiInput.enabled = false;
            _currentInput = _playerInput;
        }

        public void SetAIControl()
        {
            _playerInput.enabled = false;
            _aiInput.enabled = true;
            _currentInput = _aiInput;
        }
    }
}
