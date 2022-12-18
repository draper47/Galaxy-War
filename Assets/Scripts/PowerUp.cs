using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PowerUp : MonoBehaviour
{
    // Powerup ID: Triple Shot = 0, Speed Boost = 1, Shield = 2
    [SerializeField] private int _powerupID;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _bottomOfScreen = -6;
    
    private Player _playerScript;


    private string _playerTag = "Player";

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if(transform.position.y <= _bottomOfScreen)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == _playerTag)
        {
            _playerScript = other.transform.GetComponent<Player>();
            ActivatePowerup();
            Destroy(this.gameObject);
        }
    }

    void ActivatePowerup()
    {
        if (_playerScript != null)
        {
            switch (_powerupID)
            {
                case 0:
                    _playerScript.StartCoroutine("ActivateTrippleShot");
                    break;
                case 1:
                    _playerScript.StartCoroutine("ActivateSpeedBoost");
                    break;
                case 2:
                    _playerScript.ActivateShield();
                    break;
                default:
                    Debug.Log("Invalid _powerupID");
                    break;
            }
        }
        else
        {
            Debug.Log("No player script attached");
        }
    }
}
