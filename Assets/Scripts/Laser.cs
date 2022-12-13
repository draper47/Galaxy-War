using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _topScreen = 8;

    void Update()
    {
        LaserMovement();
        OffScreen();
    }

    void LaserMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    void OffScreen()
    {
        if (transform.position.y > _topScreen)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
            print("Destroyed laser.");
        }
    }

}
