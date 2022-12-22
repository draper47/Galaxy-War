using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _bottom = -5.5f;

    [SerializeField] private Player _playerScript;
    [SerializeField] private UIManager _UIManagerScript;
    [SerializeField] private int _EnemyID = 0;
    private int _points;

    private Animator _anim;

    private bool _isDead;

    void Start()
    {
        switch (_EnemyID)
        {
            case 0:
                _points = 10;
                break;
            
            default:
                Debug.Log("Invalid Enemy ID");
                break;
        }

        _playerScript = GameObject.Find("Player").transform.GetComponent<Player>();

        if(_playerScript != null )
        {
            Debug.Log("Player Script is NULL");
        }

        _UIManagerScript = GameObject.Find("Canvas").transform.GetComponent<UIManager>();
        
        if (_UIManagerScript != null)
        {
            Debug.Log("UIManager Script is NULL");
        }
        _anim = GetComponent<Animator>();

        if (_anim != null)
        {
            Debug.Log("Animator is NULL");
        }
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
            Destroy(this.gameObject, 2.8f);
        }

        // Collided with projectile
        if (other.tag == "Projectile" && _isDead != true)
        {
            _isDead = true;
            Destroy(other.gameObject);
            AddToScore();
            _anim.SetTrigger("Death");
            StartCoroutine(SlowDown());
            Destroy(this.gameObject, 2.8f);
        }

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
}
