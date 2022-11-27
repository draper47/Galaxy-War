using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float xBounds;
    public float yBoundsPos;
    public float yBoundsNeg;
    public GameObject player;
    
    [SerializeField]
    private Vector3 laserOffset;
    
    [SerializeField] 
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.5f; 
    
    private float _nextFire = 0.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = new position (0, 0, 0) 
        transform.position = new Vector3(0, 0, 0);
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

        transform.Translate(Vector3.up * verticalIn * maxSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalIn * maxSpeed * Time.deltaTime);

        if (transform.position.x > xBounds)
        {
            transform.position = new Vector3((-1 * xBounds), transform.position.y, transform.position.z);
        }
        else if (transform.position.x < (xBounds * -1))
        {
            transform.position = new Vector3(xBounds - .5f, transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, yBoundsNeg, yBoundsPos), transform.position.z);
    }

}
