using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private float _spawnPointY;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _player;
    
    private float _nextSpawn = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemies();
    }

    
    void spawnEnemies()
    {
        if (Time.time >= _nextSpawn && _player != null)
        {
            print("Enemy Spawned");
            float randomX = UnityEngine.Random.Range(_player.transform.position.x - 5f, _player.transform.position.x + 5f);
            Instantiate(_enemy, new Vector3(Mathf.Clamp(randomX, -9f, 9), _spawnPointY, 0f), Quaternion.identity);
            _nextSpawn = Time.time + _spawnInterval;
        }
    }
}
