using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.ScriptableObjects;
using SwapGame.Inputs;
using SwapGame.StaticMethods;

namespace SwapGame.CharacterComponents
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private Character _currentCharacter;
        [SerializeField] private CharacterGroup[] _characterGroups;

        [SerializeField] private int _health;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Movement _movement;

        [SerializeField] private InputBase _playerInput;
        [SerializeField] private InputBase _aiInput;

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

            #region Old Random Char Selector
            //int spawnValue = Random.Range(2, 101);

            //Character selectedCharacter = null;

            //foreach (Character character in groupToSpawnFrom._list)
            //{
            //    if (spawnValue > character._spawnRate)
            //    {
            //        selectedCharacter = character;
            //    }
            //}
            #endregion
        }

        private Character SelectRandomCharacter(CharacterGroup charGroup)
        {
            //For each enemy:
            //Roll a boolean
            //If True: move up 1 rarity
            //Give final rarity int

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
            _health = _currentCharacter._health;
            _spriteRenderer.sprite = _currentCharacter._sprite;
            _movement._speed = _currentCharacter._speed;
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
            _currentInput = _playerInput;
        }

        public void SetAIControl()
        {
            _playerInput.enabled = false;
            _currentInput = _aiInput;
        }

        public void ChangeHealth(int value)
        {
            _health += value;

            if (_health <= 0)
            {
                _currentInput.Die();
            }
        }
    }
}
