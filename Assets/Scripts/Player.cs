using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _speedBoost = 3;
    [SerializeField] private float _xBounds;
    [SerializeField] private float _yBounds;
    [SerializeField] private float _yCenter;

    private int _lives;
    [SerializeField] private int _maxLives = 3;

    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private Vector3 _laserOffset;
    [SerializeField] private Spawner _spawnerScript;
    
    [SerializeField] private GameObject _singleShotPrefab;
    [SerializeField] private GameObject _trippleShotPrefab;
    
    private float _nextFire = 0.0f;    
    private bool _isTrippleShot;

    [SerializeField] private bool _shieldsUp;
    [SerializeField] private GameObject _shieldsVisualizer;

    [SerializeField] private float _powerupTimer = 5;

    public int _score = 0;
    private UIManager _UIScript;

    void Start()
    {
        _UIScript = GameObject.Find("Canvas").transform.GetComponent<UIManager>();

        transform.position = new Vector3(0, 0, 0);
        _lives = _maxLives;
        _spawnerScript = GameObject.Find("Spawner").GetComponent<Spawner>();

        if (_spawnerScript == null)
        {
            print("Spawner script is null. Cannot find the Spawner scipt.");
        }
    }

    void Update()
    {
        CalcMovement();        
        
        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextFire)
        {
            FireProjectile();
            _nextFire = Time.time + _fireRate;
        }
    }

    void FireProjectile()
    {
        if(_isTrippleShot == true)
        {
            TrippleShot();
        }
        else
        {
            SingleShot();
        }
    }

    private void SingleShot()
    {
        Instantiate(_singleShotPrefab, transform.position + _laserOffset, Quaternion.identity);
    }

    private void TrippleShot()
    {
        Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
    }


    public IEnumerator ActivateSpeedBoost()
    {
        _speed += _speedBoost;
        yield return new WaitForSeconds(_powerupTimer);
        _speed -= _speedBoost;
    }

    public IEnumerator ActivateTrippleShot()
    {
        _isTrippleShot = true;
        yield return new WaitForSeconds(_powerupTimer);
        _isTrippleShot = false;
    }

    public void ActivateShield()
    {
        Debug.Log("Activated Shield");
        _shieldsUp = true;
        _shieldsVisualizer.SetActive(true);
    }


    void CalcMovement()
    {
        float horizontalIn = Input.GetAxis("Horizontal");
        float verticalIn = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * verticalIn * (_speed / 2f) * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalIn * _speed * Time.deltaTime);

        if (transform.position.x > _xBounds)
        {
            transform.position = new Vector3((-_xBounds), transform.position.y, transform.position.z);
        }
        else if (transform.position.x < (_xBounds * -1))
        {
            transform.position = new Vector3(_xBounds, transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _yBounds * -1, _yCenter), transform.position.z);
    }

    public void Damage()
    {
        if (_shieldsUp == true) 
        {
            _shieldsUp = false;
            _shieldsVisualizer.SetActive(false);
            Debug.Log("Sheilds down.");
            return;
        }
        
        _lives -= 1;

        if (_UIScript != null)
        {
            _UIScript.UpdateLivesUI(_lives);
        }

        Debug.Log("Lives left: " + _lives);

        if (_lives < 1) 
        {   
            _spawnerScript.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void AddToPlayerScore(int points)
    {
        _score += 10;

        if (_UIScript != null)
        {
            _UIScript.UpdateScore(_score);
        }
    }
}
