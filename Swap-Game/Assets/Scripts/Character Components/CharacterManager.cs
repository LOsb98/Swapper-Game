using UnityEngine;
using System.Collections;
using SwapGame.ScriptableObjects;
using SwapGame.Inputs;

namespace SwapGame.CharacterComponents
{
    /// <summary>
    /// Handles assigning character data to necessary components when a character is spawned
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
        public Character _currentCharacter;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Movement _movement;

        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private AIInput _aiInput;

        [SerializeField] private Attack _attackScript;
        [SerializeField] private Health _healthManager;

        [SerializeField] private BoxCollider2D boxCollider;

        private InputBase _currentInput;

        public InputBase CurrentInput => _currentInput;

        public void Initialize()
        {
            Debug.Log("Initialize");

            gameObject.name = _currentCharacter.name; //This should already be set by the time we get here

            //Sending character data to the relevant components
            //For attacks, things like fire rate and projectiles can be passed into the Attack script beforehand
            //The input scripts can then call a method to attempt an attack when necessary, all other logic is handled in the Attack script separately
            //May need to pass in an "aim direction" Vector2

            _healthManager.CurrentHealth = _currentCharacter._health;
            _spriteRenderer.sprite = _currentCharacter._sprite;
            _movement._speed = _currentCharacter._speed;
            _attackScript._attackDelay = _currentCharacter._fireRate;
            _attackScript._projectileData = _currentCharacter._projectile;

            boxCollider.size = _currentCharacter._size;

            StartStepCoroutine();
        }

        //This should automatically turn off when the object is disabled
        public void StartStepCoroutine()
        {
            StartCoroutine(DoStep());
        }

        private IEnumerator DoStep()
        {
            do
            {
                _currentInput.Step();

                yield return null;

            } while (true);
        }

        //Keeping this as a separate method that can be called from other scripts later
        public void SetPlayerControl()
        {
            _playerInput.enabled = true; //Have to set this enabled/disabled for the controls package
            _currentInput = _playerInput;
        }

        public void SetAIControl()
        {
            _playerInput.enabled = false;
            _currentInput = _aiInput;
        }

        private void OnDisable()
        {
            _currentCharacter = null;
        }
    }
}
