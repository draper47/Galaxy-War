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

    [SerializeField]
    private float _speed = 1;



    void movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

    }
    
    [SerializeField]
    private float _yBounds = 8;

    [SerializeField]
    private float _xBounds = 10;

    [SerializeField]
    private GameObject _enemy;


    void hitBottomScreen()
    {
        // If the y position value hits the bottom of the screen
        // Respawn to the top of screen with a random horizontal position

        if(transform.position.y < _yBounds * -1)
        {
            Instantiate(_enemy, new Vector3(UnityEngine.Random.Range(-1 * _xBounds, _xBounds), _yBounds, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
