using System;
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip _explosionSound;

    void Start()
    {
        if (_explosionSound != null)
        {
           AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
        }
        else
        {
            Debug.LogError("Explosion._explosionSound is NULL");
        }
        
        Destroy(this.gameObject, 2.37f);
    }
}
