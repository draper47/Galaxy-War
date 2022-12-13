using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrippleShot : MonoBehaviour
{
    private GameObject _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player = other.gameObject;
            CollectedTrippleShot();
        }

    }

    void CollectedTrippleShot()
    {
        if(_player.transform.GetComponent<Player>() != null)
        {
            _player.transform.GetComponent<Player>().StartCoroutine("ActivateTrippleShot");
        }
    }
}
