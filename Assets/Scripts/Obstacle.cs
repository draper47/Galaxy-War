using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _rotationDegreesPerSecond = 20;
    
    [SerializeField] private GameObject _explosion;

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
        Debug.Log("Hit" + other);

        if (other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
