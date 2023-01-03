using System;
using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float _speed = 8;
    [SerializeField] private AudioClip _laserSound;
    [SerializeField] private float _timeUntilOffScreen = 1.5f;

    [SerializeField] private bool _isEnemyLaser = false;

    void Start()
    {
        if(_laserSound != null)
        {
            AudioSource.PlayClipAtPoint(_laserSound, transform.position);
        }
        else
        {
            Debug.Log("Laser._laserSound == NULL");
        }

        Destroy(gameObject, _timeUntilOffScreen);
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    void Update()
    {
        LaserMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(_isEnemyLaser && other.tag == "Player")
        {
            Player playerScript = other.GetComponent<Player>();

            if (playerScript != null)
            {
                playerScript.Damage();
            }
        }
    }
    void LaserMovement()
    {
        if (_isEnemyLaser)
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * _speed);
        }  
    }
}
