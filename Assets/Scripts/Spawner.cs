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
    [SerializeField] private GameObject[] _powerups;

    [SerializeField] private Player _playerScript;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _xMaxDistanceFromPlayer;
    
    [SerializeField] private GameObject _enemyContainer;

    private bool _isPlayerDead;
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerScript = _player.transform.GetComponent<Player>();

        if(_playerScript == null )
        {
            Debug.Log("Player Script is NULL");
        }
    }

    public void StartSpawning()
    {
        Debug.Log("Start Spawning called");

        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(SpawnPowerupsRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        yield return new WaitForSeconds(3f);

        while(_isPlayerDead == false)
        {
            float spawnInterval = Random.Range(_minEnemySpawnInterval, _maxEnemySpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
            
            if(_player == null)
            {
                break;
            }

            var playerPosition = _player.transform.position;
            float randomX = Random.Range(playerPosition.x - _xMaxDistanceFromPlayer, playerPosition.x + _xMaxDistanceFromPlayer);
            Vector3 randomSpawnPoint = new Vector3(Mathf.Clamp(randomX, -9f, 9f), _ySpawnPoint, 0f);
            
            GameObject newEnemy = Instantiate(_enemy, randomSpawnPoint, Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);

        }
    }

    IEnumerator SpawnPowerupsRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_isPlayerDead == false)
        {
            float spawnInterval = Random.Range(_minPowerupSpawnInterval, _maxPowerupSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            float xSpawnPoint = Random.Range(-_xEdgeOfScreen, _xEdgeOfScreen);
            Vector3 spawnPoint = new Vector3(xSpawnPoint, _ySpawnPoint, 0);

            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], spawnPoint, Quaternion.identity);
        }
    }

    public void onPlayerDeath()
    {
        _isPlayerDead = true;
    }
}
