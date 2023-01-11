using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.CharacterComponents;

namespace SwapGame.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerIcon;
        [SerializeField] private int _score;

        [SerializeField] private EnemySpawner _enemySpawner;

        [SerializeField] private GameObject _currentPlayer;
        public GameObject CurrentPlayer => _currentPlayer;

        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

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

        void Start()
        {
            //This needs to be done in Start() and not Awake()
            //The execution order will try and call SetPlayer() on the CharacterManager before it has finished getting all its compoment references
            //It will throw an error + the PlayerIcon will not track the player (only in the build version?)
            SetNewPlayer(CurrentPlayer);
        }

        void Update()
        {
            //Increment timers for spawning enemies
            _playerIcon.transform.position = CurrentPlayer.transform.position;
        }

        public void SetNewPlayer(GameObject newPlayer)
        {
            if (CurrentPlayer != null)
            {
                var currentCharManager = CurrentPlayer.GetComponent<CharacterManager>();
                currentCharManager.SetAIControl();
            }

            var newCharManager = newPlayer.GetComponent<CharacterManager>();
            newCharManager.SetPlayerControl();

            _currentPlayer = newPlayer;
        }

        public void AddScore()
        {
            _score++;
        }

        public void StartNewGame()
        {
            _enemySpawner.enabled = true;
        }

        public void EndGame()
        {
            _enemySpawner.enabled = false;
            //Logic for ending the game
        }
    }
}
