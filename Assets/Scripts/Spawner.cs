using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnIntervalMax = 5f;
    [SerializeField] private float _spawnIntervalMin= .1f;

    [SerializeField] private float _spawnPointY;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _spawnOffset;
    [SerializeField] private GameObject _enemyContainer;

    private bool isPlayerDead;
    void Start()
    {
       StartCoroutine(SpawnEnemiesRoutine());
    }


    void Update()
    {
        
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        var playerScript = _player.GetComponent<Player>();
        
        while(isPlayerDead == false)
        {
            float spawnInterval = Random.Range(_spawnIntervalMin, _spawnIntervalMax);
            var playerPosition = _player.transform.position;
            float randomX = Random.Range(playerPosition.x - _spawnOffset, playerPosition.x + _spawnOffset);
            Vector3 randomSpawnPoint = new Vector3(Mathf.Clamp(randomX, -9f, 9f), _spawnPointY, 0f);
            GameObject newEnemy = Instantiate(_enemy, randomSpawnPoint, Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void onPlayerDeath()
    {
        isPlayerDead = true;
    }
}
