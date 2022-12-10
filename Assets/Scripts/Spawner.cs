using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnIntervalMax = 5f;
    [SerializeField] private float _spawnIntervalMin= .1f;

    [SerializeField] private float _yDistanceFromPlayer;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _xMaxDistanceFromPlayer;
    [SerializeField] private GameObject _enemyContainer;

    private bool _isPlayerDead;
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
        
        while(_isPlayerDead == false)
        {
            float spawnInterval = Random.Range(_spawnIntervalMin, _spawnIntervalMax);
            var playerPosition = _player.transform.position;
            float randomX = Random.Range(playerPosition.x - _xMaxDistanceFromPlayer, playerPosition.x + _xMaxDistanceFromPlayer);
            Vector3 randomSpawnPoint = new Vector3(Mathf.Clamp(randomX, -9f, 9f), _yDistanceFromPlayer, 0f);
            GameObject newEnemy = Instantiate(_enemy, randomSpawnPoint, Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void onPlayerDeath()
    {
        _isPlayerDead = true;
    }
}
