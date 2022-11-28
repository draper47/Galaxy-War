using System;
using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement();
        hitBottomScreen();
    }

    [SerializeField] private float _speed = 1;



    void movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

    }


    void hitBottomScreen()
    {
        // If the y position value hits the bottom of the screen
        // destory itself

        if(transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        print("Hit: " + other.transform.name);
    // If other is player
    // Hurt Player
    // Destroy self

        if(other.tag == "Player")
        {
            Player.health -= 10;
            print("Health is " + Player.health);
            Destroy(this.gameObject);
        }
    // If other is laser
    // destory laser
    // Destroy self

        if(other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }






}
