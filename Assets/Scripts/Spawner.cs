using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _ySpawnPoint;
    [SerializeField] private float _xEdgeOfScreen = 11;

    [SerializeField] private float _minEnemySpawnInterval= .1f;
    [SerializeField] private float _maxEnemySpawnInterval = 5f;

    [SerializeField] private GameObject _enemy;

    [SerializeField] private float _minPowerupSpawnInterval = 3f;
    [SerializeField] private float _maxPowerupSpawnInterval = 10f;

    [SerializeField] private GameObject[] _commonPowerups;
    [SerializeField] private GameObject[] _uncommonPowerups;
    [SerializeField] private GameObject[] _rarePowerups;

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

    void Update()
    {
        SetSpawnRandomSpawnPoint();
    }

    void SetSpawnRandomSpawnPoint()
    {
        var playerPosition = _player.transform.position;

        float randomX = UnityEngine.Random.Range(playerPosition.x - _xMaxDistanceFromPlayer, playerPosition.x + _xMaxDistanceFromPlayer);

        transform.position = new Vector3(Mathf.Clamp(randomX, -_xEdgeOfScreen, _xEdgeOfScreen), _ySpawnPoint, 0f);
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(SpawnPowerupsRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        yield return new WaitForSeconds(3f);

        while(_isPlayerDead == false)
        {
            float spawnInterval = UnityEngine.Random.Range(_minEnemySpawnInterval, _maxEnemySpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
            
            if(_player == null)
            {
                break;
            }
            
            GameObject newEnemy = Instantiate(_enemy, transform.position, Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);

        }
    }

    IEnumerator SpawnPowerupsRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_isPlayerDead == false)
        {
            float spawnInterval = UnityEngine.Random.Range(_minPowerupSpawnInterval, _maxPowerupSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            PickPowerupSpawnRarity();
        }
    }

    void PickPowerupSpawnRarity()
    {
        int randomPercentage = UnityEngine.Random.Range(1, 101);

        switch (randomPercentage)
        {
            case int percentageRange when (percentageRange > 30):
                SpawnCommonPowerup();
                break;
            case int percentageRange when (percentageRange <= 30 && percentageRange > 10):
                SpawnUncommonPowerup();
                break;
            case int percentageRange when (percentageRange < 10):
                SpawnRarePowerup();
                break;
            default:
                SpawnCommonPowerup();
                break;
        }
    }
    void SpawnCommonPowerup()
    {
        int randomPowerup = UnityEngine.Random.Range(0, _commonPowerups.Length);

        Instantiate(_commonPowerups[randomPowerup], transform.position, Quaternion.identity);
    }

    void SpawnUncommonPowerup()
    {
        int randomPowerup = UnityEngine.Random.Range(0, _uncommonPowerups.Length);

        Instantiate(_uncommonPowerups[randomPowerup], transform.position, Quaternion.identity);
    }

    void SpawnRarePowerup()
    {
        int randomPowerup = UnityEngine.Random.Range(0, _rarePowerups.Length);

        Instantiate(_rarePowerups[randomPowerup], transform.position, Quaternion.identity);
    }

    public void onPlayerDeath()
    {
        _isPlayerDead = true;
    }
}
