using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float _speed = 8;
    [SerializeField] private AudioClip _laserSound;
    [SerializeField] private float _timeUntilOffScreen = 1.5f;

    [SerializeField] private bool _isEnemyLaser;
    
    [SerializeField] private bool _explodingLaser;
    [SerializeField] private float _explosionTimer;
    [SerializeField] private float _explosionLaserSlowDownMultiplier;

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

    void Update()
    {
        LaserMovement();

        if (_explodingLaser)
        {
            ExplodingLaser();
        }
    }

    void LaserExplosionCountdown()
    {
        _explosionTimer -= Time.deltaTime;
    }

    void ExplodingLaser()
    {
        LaserExplosionCountdown();

        if (_speed > 0)
        {
            _speed -= Time.deltaTime * _explosionLaserSlowDownMultiplier;
        }

        if (_explosionTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    public void ActivateExplodingLaser()
    {
        _explodingLaser = true;
    }

    public void DeactivateExplodingLaser()
    {
        _explodingLaser = false;
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
