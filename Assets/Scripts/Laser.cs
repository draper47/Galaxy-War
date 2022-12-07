using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _edgeOfScreenY;

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

    void laserMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    void offScreen()
    {
        if (transform.position.y > _edgeOfScreenY)
        {
            Destroy(this.gameObject);
            print("Destroyed laser.");
        }
    }

}
