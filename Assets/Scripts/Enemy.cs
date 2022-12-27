using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _bottom = -5.5f;

    [SerializeField] private Player _playerScript;
    [SerializeField] private UIManager _UIManagerScript;
    [SerializeField] private int _EnemyID = 0;
    private int _points;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private Vector3 _laserOffset;
    [SerializeField] private float _minFireRate;
    [SerializeField] private float _maxFireRate;

    private Animator _animator;

    private bool _isDead;

    [SerializeField] private AudioClip _explosionSound;

    void Start()
    {
        switch (_EnemyID)
        {
            case 0:
                _points = 10;
                break;
            
            default:
                Debug.LogError("Invalid Enemy ID");
                break;
        }

        _playerScript = GameObject.Find("Player").transform.GetComponent<Player>();

        if(_playerScript == null )
        {
            Debug.LogError("Player Script is NULL");
        }

        _UIManagerScript = GameObject.Find("Canvas").transform.GetComponent<UIManager>();
        
        if (_UIManagerScript == null)
        {
            Debug.LogError("UIManager Script is NULL");
        }

        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("Enemy Animator is NULL");
        }

        if (_explosionSound == null)
        {
            Debug.LogError("Enemy Explosion Sound is NULL");
        }

        StartCoroutine(FireLaser());
    }
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
        // Collided with player 
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            else
            {
                print("No Player script attached.");
            }

            Death();
        }

        // Collided with projectile
        if (other.tag == "Projectile" && _isDead != true)
        {
            Destroy(other.gameObject);
            AddToScore();
            Death();
        }
    }

    void Death()
    {
        _animator.SetTrigger("Death");
        StartCoroutine(SlowDown());
        _isDead = true;
        AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.8f);
    }

    void AddToScore()
    {
        if (_playerScript != null)
        {
            _playerScript.AddToPlayerScore(_points);

        }
        else
        {
            Debug.Log("Player script is Null. No Player script attached.");
        }
    }

    IEnumerator SlowDown()
    {
        while(_speed > 0)
        {
            _speed -= .5f;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(.5f);
        float randomFireRate;

        while(_isDead != true)
        {
            GameObject laser = Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
            laser.GetComponent<Laser>().AssignEnemyLaser();
            randomFireRate = UnityEngine.Random.Range(_minFireRate, _maxFireRate);
            yield return new WaitForSeconds(randomFireRate);
        }
    }
}
