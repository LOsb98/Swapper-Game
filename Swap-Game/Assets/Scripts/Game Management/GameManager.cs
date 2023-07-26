using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.CharacterComponents;
using UnityEngine.Events;

namespace SwapGame.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerIcon;
        [SerializeField] private int _score;

        [SerializeField] private EnemySpawner _enemySpawner;

        /// <summary>
        /// A reference to the game object which the player is currently controlling
        /// </summary>
        public CharacterManager _currentPlayer;

        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        public UnityEvent OnGameStart;
        public UnityEvent OnPlayerSwapped;
        public UnityEvent OnPlayerDied;
        public UnityEvent OnEnemyDied;

        //An error comes up if this is assigned to the first instance of the Character object
        //i.e. If this =  Character, the error comes up
        //If this = Character(1), no error
        //The game still runs as normal regardless (it seems)

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void StartNewGame()
        {
            //This needs to be done in Start() and not Awake()
            //The execution order will try and call SetPlayer() on the CharacterManager before it has finished getting all its component references
            //It will throw an error + the PlayerIcon will not track the player (only in the build version?)

            //SetNewPlayer(_currentPlayer);
            OnGameStart.Invoke();
        }

        public void SetNewPlayer(CharacterManager newPlayer)
        {
            if (_currentPlayer)
            {
                var currentCharManager = _currentPlayer;
                currentCharManager.SetAIControl();
            }

            var newCharManager = newPlayer;
            newCharManager.SetPlayerControl();

            _currentPlayer = newPlayer;

            OnPlayerSwapped.Invoke();
        }

        public void AddScore()
        {
            _score++;
        }

        public void EndGame()
        {
            //Logic for ending the game
        }

        public void MovePlayerIndicator()
        {
            _playerIcon.transform.parent = _currentPlayer.transform;
            _playerIcon.transform.localPosition = Vector2.zero;
        }
    }
}
