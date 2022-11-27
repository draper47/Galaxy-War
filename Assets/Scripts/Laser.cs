using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        laserMovement();
        offScreen();
    }

    //Laser movement

    [SerializeField]
    private float _speed;

    void laserMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    // Destroy the laser when it is off the screen
    
    [SerializeField]
    private float _edgeOfScreenY;

    void offScreen()
    {
        if (transform.position.y > _edgeOfScreenY)
        {
            Destroy(this.gameObject);
            print("Destroyed laser.");
        }
    }

}
