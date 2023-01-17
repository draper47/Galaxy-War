using System;
using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Powerup ID: Triple Shot = 0, Speed Boost = 1, Shield = 2
    [SerializeField] private int _powerupID;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _bottomOfScreen = -6;

    private Player _playerScript;
    private string _playerTag = "Player";

    [SerializeField] private AudioClip _collectedPowerupSound;

    void Start()
    {
        if(_collectedPowerupSound == null)
        {
            Debug.LogError("PowerUp._collectedPowerupSound == NULL");
        }
    }

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

            CollectedPowerup();
        }
    }

    void ActivatePowerup()
    {
        if (_playerScript != null)
        {
            switch (_powerupID)
            {
                case 0:
                    _playerScript.RefillAmmo();
                    _playerScript.ActivateTrippleShot();
                    break;
                case 1:
                    _playerScript.StartCoroutine("ActivateSpeedBoost");
                    break;
                case 2:
                    _playerScript.ActivateShield();
                    break;
                case 3:
                    _playerScript.Repair();
                    break;
                case 4:
                    _playerScript.RefillAmmo();
                    break;
                case 5:
                    _playerScript.RefillAmmo();
                    _playerScript.ActivateExplodingLaser();
                    break;
                default:
                    Debug.Log("Invalid _powerupID value");
                    break;
            }
        }
        else
        {
            Debug.LogError("PowerUp._playerScript == NULL");
        }
    }

    void CollectedPowerup()
    {
        if (_collectedPowerupSound != null)
        {
            AudioSource.PlayClipAtPoint(_collectedPowerupSound, transform.position);
        }

        ActivatePowerup();
        Destroy(this.gameObject);
    }
}
