using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _characterBase;
    [SerializeField] private float _enemySpawnTime;
    private float _enemySpawnTimer;

    private Vector2 screenBounds;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

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
        GameObject newEnemy = Instantiate(_characterBase);

        newEnemy.transform.position = spawnPosition;
    }
}
