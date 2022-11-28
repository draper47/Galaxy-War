using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration = 1f;

    [SerializeField] private float _xBounds;
    [SerializeField] private float _yBounds;
    [SerializeField] private float _yCenter;

    public GameObject player;
    public static int health;
    public int maxHealth = 100;
    private float _nextFire = 0.0f;
    

    [SerializeField] private Vector3 laserOffset;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    


    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = new position (0, 0, 0) 
        transform.position = new Vector3(0, 0, 0);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        calcMovement();
        fireLaser();
    }


    void fireLaser()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextFire)
        {
            Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
            _nextFire = Time.time + _fireRate;
        }
    }

    void calcMovement()
    {
        float horizontalIn = Input.GetAxis("Horizontal");
        float verticalIn = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * (verticalIn * acceleration) * maxSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * (horizontalIn * acceleration) * maxSpeed * Time.deltaTime);

        if (transform.position.x > _xBounds)
        {
            transform.position = new Vector3((-1 * _xBounds), transform.position.y, transform.position.z);
        }
        else if (transform.position.x < (_xBounds * -1))
        {
            transform.position = new Vector3(_xBounds - .5f, transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _yBounds * -1, _yCenter), transform.position.z);
    }

}
