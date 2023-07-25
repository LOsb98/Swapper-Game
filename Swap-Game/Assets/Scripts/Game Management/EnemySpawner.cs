using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.CharacterComponents;

namespace SwapGame.GameManagement
{
    //This script handles spawning of enemies and also acts as an object pool
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _characterBase;
        [SerializeField] private float _enemySpawnTime;
        [SerializeField] private int _maxPossibleEnemies; //This is to determine the size of the pool, also relevant to gameplay balance and keeping performance in check
        [SerializeField] private List<GameObject> _pooledEnemies;
        [SerializeField] private GameObject _characterPrefab;

        private float _enemySpawnTimer;

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
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

            GameObject temp;
            for (int i = 0; i < _maxPossibleEnemies; i++)
            {
                temp = Instantiate(_characterPrefab);
                temp.SetActive(false);
                _pooledEnemies.Add(temp);
            }
        }

        //This can be a repeating coroutine
        void Update()
        {
            if (_enemySpawnTimer > 0)
            {
                _enemySpawnTimer -= Time.deltaTime;
                return;
            }

            SpawnEnemy(NewSpawnPosition());
            _enemySpawnTimer = _enemySpawnTime;
        }

        private Vector2 NewSpawnPosition()
        {
            Vector2 enemySpawnPos;
            enemySpawnPos.x = Random.Range(-screenBounds.x + 2, screenBounds.x - 2);
            enemySpawnPos.y = Random.Range(-screenBounds.y + 2, screenBounds.y - 2);

            return enemySpawnPos;
        }

        private void SpawnEnemy(Vector2 spawnPosition)
        {
            //GameObject newEnemy = Instantiate(_characterBase);

            GameObject newEnemy = GetPooledEnemy();

            if (!newEnemy)
            {
                return;
            }

            newEnemy.SetActive(true);
           // newEnemy.GetComponent<CharacterManager>().Initialize();
            newEnemy.transform.position = spawnPosition;
        }

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

        public void ManualSpawnEnemy()
        {
            SpawnEnemy(NewSpawnPosition());
        }
    }
}