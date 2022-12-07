using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    //[SerializeField] private float _leftEdge = -10f;
    //[SerializeField] private float _rightEdge = 10f;
    [SerializeField] private float _bottom = -5.5f;
    //[SerializeField] private float _top = 10f;
    //[SerializeField] private float _fromPlayer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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


    public void OnTriggerEnter(Collider other)
    {
        print("Collided with " + other);

        Player player = other.transform.GetComponent<Player>();

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

        if (other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }


}
