using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Spawner : MonoBehaviour
{    
    [SerializeField] private float _minEnemySpawnInterval= .1f;
    [SerializeField] private float _maxEnemySpawnInterval = 5f;

    [SerializeField] private float _minPowerupSpawnInterval = 3f;
    [SerializeField] private float _maxPowerupSpawnInterval = 10f;

    [SerializeField] private float _ySpawnPoint;
    [SerializeField] private float _xEdgeOfScreen = 11;

    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _trippleShotPowerup;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _xMaxDistanceFromPlayer;
    
    [SerializeField] private GameObject _enemyContainer;

    private bool _isPlayerDead;
    void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        var playerScript = _player.GetComponent<Player>();
        
        while(_isPlayerDead == false)
        {
            float spawnInterval = Random.Range(_minEnemySpawnInterval, _maxEnemySpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            var playerPosition = _player.transform.position;
            float randomX = Random.Range(playerPosition.x - _xMaxDistanceFromPlayer, playerPosition.x + _xMaxDistanceFromPlayer);
            Vector3 randomSpawnPoint = new Vector3(Mathf.Clamp(randomX, -9f, 9f), _ySpawnPoint, 0f);
            
            GameObject newEnemy = Instantiate(_enemy, randomSpawnPoint, Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);

        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_isPlayerDead == false)
        {
            float spawnInterval = Random.Range(_minPowerupSpawnInterval, _maxPowerupSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            float xSpawnPoint = Random.Range(-_xEdgeOfScreen, _xEdgeOfScreen);
            Vector3 spawnPoint = new Vector3(xSpawnPoint, _ySpawnPoint, 0);
            Instantiate(_trippleShotPowerup, spawnPoint, Quaternion.identity);
        }
    }

    public void onPlayerDeath()
    {
        _isPlayerDead = true;
    }
}
