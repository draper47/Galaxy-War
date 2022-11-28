using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 5f;
    private float _nextSpawn = 1f;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemies();
    }

    void spawnPlayer()
    {
        Instantiate(_player, new Vector3(0, 0, 0), Quaternion.identity);
    }
    void spawnEnemies()
    {
        if (Time.time >= _nextSpawn)
        {
            print("Enemy Spawned");
            float randomX = UnityEngine.Random.Range(Mathf.Clamp(_player.transform.position.x - 5, -10f, 10f), Mathf.Clamp(_player.transform.position.x + 5, -10f, 10f));
            Instantiate(_enemy, new Vector3(randomX, 12f, 0f), Quaternion.identity);
            _nextSpawn = Time.time + _spawnRate;
        }
    }
}
