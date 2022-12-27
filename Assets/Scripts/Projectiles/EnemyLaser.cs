using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] private float _speed = 12;
    [SerializeField] private float _bottomScreen = 8;
    [SerializeField] private AudioClip _laserSound;

    void Start()
    {
        if (_laserSound != null)
        {
            AudioSource.PlayClipAtPoint(_laserSound, transform.position);
        }
        else
        {
            Debug.Log("Laser._laserSound == NULL");
        }
    }
    void Update()
    {
        LaserMovement();
        OffScreen();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player playerScript;

        if (other.tag == "Player")
        {
            playerScript = other.GetComponent<Player>();

            if (playerScript != null)
            {
                playerScript.Damage();
            }

            Destroy(this.gameObject);
        }
    }

    void LaserMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }

    void OffScreen()
    {
        if (transform.position.y > _bottomScreen)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
            print("Destroyed laser.");
        }
    }
}
