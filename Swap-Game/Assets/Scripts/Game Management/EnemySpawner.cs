using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.CharacterComponents;
using SwapGame.ScriptableObjects;
using SwapGame.StaticMethods;

namespace SwapGame.GameManagement
{
    //This script handles spawning of enemies and also acts as an object pool
    public class EnemySpawner : MonoBehaviour
    {
        /// <summary>
        /// Prefab used to spawn an enemy, data is then used to change its properties
        /// </summary>
        [SerializeField] private GameObject _characterPrefab;

        /// <summary>
        /// How frequently to spawn a new enemy
        /// </summary>
        [SerializeField] private float _enemySpawnTime; 

        /// <summary>
        /// Maximum size of enemy pool
        /// </summary>
        [SerializeField] private int _maxPossibleEnemies;

        [SerializeField] private List<GameObject> _pooledEnemies;

        [SerializeField] private CharacterGroup[] _characterGroups;

        private Vector2 screenBounds;

        private static EnemySpawner _instance;
        public static EnemySpawner Instance => _instance;

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

        private void Start()
        {
            _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

            GameObject temp;
            for (int i = 0; i < _maxPossibleEnemies; i++)
            {
                temp = Instantiate(_characterPrefab);
                temp.SetActive(false);
                _pooledEnemies.Add(temp);
            }
        }

        private Vector2 NewSpawnPosition()
        {
            Vector2 enemySpawnPos;
            enemySpawnPos.x = Random.Range(-screenBounds.x + 2, screenBounds.x - 2);
            enemySpawnPos.y = Random.Range(-screenBounds.y + 2, screenBounds.y - 2);

            return enemySpawnPos;
        }

        private CharacterGroup SelectRandomCharacterGroup()
        {
            return _characterGroups[0]; //Temporary, need to decide how new groups are selected
        }

        private Character SelectRandomEnemy()
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

            CharacterGroup group = SelectRandomCharacterGroup();

            int characterIndex = 0;

            int rollCount = group._list.Length - 1; //We flip a coin up to the number of enemies in the group -1

            for (int i = 0; i < rollCount; i++)
            {
                if (MathsStuff.CoinToss())
                {
                    characterIndex++; //If we flip "heads", increment the index and flip again
                }
                else
                {
                    break; //If we flip "tails", exit the loop and use the character at the current index
                }
            }

            Debug.Log($"Spawn value: {characterIndex}");

            Character finalCharacterSpawn = group._list[characterIndex];

            Debug.Log($"Spawn character: {finalCharacterSpawn.name}");

            return finalCharacterSpawn;
        }

        private void SpawnEnemy()
        {
            GameObject newEnemy = GetPooledEnemy();

            if (newEnemy == null)
            {
                Debug.Log("No pooled enemies available");
                return;
            }

            newEnemy.transform.position = NewSpawnPosition();
            newEnemy.SetActive(true);

            CharacterManager manager = newEnemy.GetComponent<CharacterManager>();

            manager._currentCharacter = SelectRandomEnemy();
            manager.SetAIControl(); //Spawned enemies will always start as AI
            manager.Initialize();
        }

        /// <summary>
        /// Find a pooled enemy to use
        /// </summary>
        /// <returns>The first pooled enemy object which isn't currently active</returns>
        private GameObject GetPooledEnemy()
        {
            for (int i = 0; i < _maxPossibleEnemies; i++)
            {
                if (!_pooledEnemies[i].activeInHierarchy)
                {
                    return _pooledEnemies[i];
                }
            }

            return null;
        }

        public void ClearAllEnemies()
        {
            //Disable all enemies in pooled list
            foreach (GameObject enemy in _pooledEnemies)
            {
                enemy.SetActive(false);
            }
        }

        /// <summary>
        /// Spawn an enemy as normal, called from UI button, just for debugging until another use is found for this
        /// </summary>
        public void ManualSpawnEnemy()
        {
            SpawnEnemy();
        }

        public void StartEnemySpawner()
        {
            _attackRoutine = EnemySpawnerLoop();
            StartCoroutine(_attackRoutine);
        }

        public void StopEnemySpawner()
        {
            Debug.Log("Stop enemy spawner");
            StopCoroutine(_attackRoutine);
        }

        /// <summary>
        /// Main loop for spawning enemies during gameplay
        /// </summary>
        private IEnumerator EnemySpawnerLoop()
        {
            do
            {
                yield return new WaitForSeconds(_enemySpawnTime);

                SpawnEnemy();

                yield return null;

            } while (true);
        }
    }
}