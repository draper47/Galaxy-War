using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingLaser : MonoBehaviour
{
    private Laser _laserScript;

    [SerializeField] private GameObject _laserExplosionPrefab;
    void Start()
    {
        _laserScript = this.GetComponent<Laser>();

        if (_laserScript == null)
        {
            Debug.LogError("ExplodingLaser._laserScript == NULL");
        }

        _laserScript.ActivateExplodingLaser();
    }

    private void OnDestroy()
    {
        Instantiate(_laserExplosionPrefab, transform.position, Quaternion.identity);
    }
}
