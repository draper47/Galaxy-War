using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotationDegreesPerSecond = 20;
    
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Spawner _spawnManager;
    private bool _hit;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawner").GetComponent<Spawner>();

        if(_spawnManager == null)
        {
            Debug.Log("Spawner Script is null");
        }
    }

    private void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        transform.Rotate(0, 0, _rotationDegreesPerSecond * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile" && _hit != true)
        {
            _hit = true;
            _spawnManager.StartSpawning();
            Destroy(other.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 1f);
        }
    }
}
