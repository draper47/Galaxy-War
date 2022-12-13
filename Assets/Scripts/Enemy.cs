using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    [SerializeField] private float _bottom = -5.5f;

    [SerializeField] private GameObject _player;

    void Update()
    {
        Movement();
        hitBottomOfScreen();
    }
    
    void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }

    void hitBottomOfScreen()
    {
        if(transform.position.y <= _bottom)
        {
            Destroy(this.gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        print("Collided with " + other);

        Player player = other.transform.GetComponent<Player>();
        
        // Collided with player 
        if (other.tag == "Player")
        {
            if (player != null)
            {
                player.Damage();
            }
            else
            {
                print("No Player script attached.");
            }
            Destroy(this.gameObject);
        }

        // Collided with projectile
        if (other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }


}
