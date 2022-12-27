using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _speedBoost = 3;
    [SerializeField] private float _xBounds;
    [SerializeField] private float _yBounds;
    [SerializeField] private float _yCenter;

    private int _lives;
    [SerializeField] private int _maxLives = 3;

    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private Vector3 _laserOffset;
    [SerializeField] private Spawner _spawnerScript;

    [SerializeField] private GameObject _singleShotPrefab;
    [SerializeField] private GameObject _trippleShotPrefab;

    private float _nextFire = 0.0f;
    private bool _isTrippleShot;

    [SerializeField] private bool _shieldsUp;
    [SerializeField] private GameObject _shieldsVisualizer;

    [SerializeField] private float _powerupTimer = 5;

    public int _score = 0;
    private UIManager _UIScript;
    private GameManager _gameManagerScript;

    [SerializeField] private GameObject[] _enginesOutVisuals;
    private bool _engine00_Out;
    private bool _engine01_Out;
    private int _engineOutIndex;

    [SerializeField] private GameObject _explosionPrefab;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _lives = _maxLives;

        _UIScript = GameObject.Find("Canvas").transform.GetComponent<UIManager>();
        
        if (_UIScript == null )
        {
            Debug.LogError("Player._UIScript == NULL");
        }

        _gameManagerScript = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManagerScript == null )
        {
            Debug.LogError("Player._gameManagerScript == NULL");
        }

        _spawnerScript = GameObject.Find("Spawner").GetComponent<Spawner>();

        if (_spawnerScript == null)
        {
            print("Spawner script is null. Cannot find the Spawner scipt.");
        }

        if (_explosionPrefab == null)
        {
            Debug.LogError("Player._explosionPrefab == NULL");
        }
    }

    void Update()
    {
        CalcMovement();        
        
        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextFire)
        {
            FireProjectile();
            _nextFire = Time.time + _fireRate;
        }
    }

    void FireProjectile()
    {
        if(_isTrippleShot == true)
        {
            TrippleShot();
        }
        else
        {
            SingleShot();
        }
    }

    private void SingleShot()
    {
        Instantiate(_singleShotPrefab, transform.position + _laserOffset, Quaternion.identity);
    }

    private void TrippleShot()
    {
        Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
    }


    public IEnumerator ActivateSpeedBoost()
    {
        _speed += _speedBoost;
        yield return new WaitForSeconds(_powerupTimer);
        _speed -= _speedBoost;
    }

    public IEnumerator ActivateTrippleShot()
    {
        _isTrippleShot = true;
        yield return new WaitForSeconds(_powerupTimer);
        _isTrippleShot = false;
    }

    public void ActivateShield()
    {
        Debug.Log("Activated Shield");
        _shieldsUp = true;
        _shieldsVisualizer.SetActive(true);
    }


    void CalcMovement()
    {
        float horizontalIn = Input.GetAxis("Horizontal");
        float verticalIn = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * verticalIn * (_speed / 2f) * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalIn * _speed * Time.deltaTime);

        if (transform.position.x > _xBounds)
        {
            transform.position = new Vector3((-_xBounds), transform.position.y, transform.position.z);
        }
        else if (transform.position.x < (_xBounds * -1))
        {
            transform.position = new Vector3(_xBounds, transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _yBounds * -1, _yCenter), transform.position.z);
    }

    public void Damage()
    {
        if (_shieldsUp == true) 
        {
            _shieldsUp = false;
            _shieldsVisualizer.SetActive(false);
            Debug.Log("Sheilds down.");
            return;
        }
        
        _lives -= 1;

        if (_UIScript != null)
        {
            _UIScript.UpdateLivesUI(_lives);
        }

        EnginesOutVisual();

        Debug.Log("Lives left: " + _lives);

        if (_lives < 1) 
        {
            Death();
        }
    }

    void Death()
    {
        _spawnerScript.onPlayerDeath();
        _gameManagerScript.GameOver();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void EnginesOutVisual()
    {
        if (_engine00_Out != true && _engine01_Out != true)
        {
            _engineOutIndex = UnityEngine.Random.Range(0, 2);
        } 
        else if (_engine01_Out == true)
        {
            _engineOutIndex = 0;
        }
        else if (_engine00_Out == true)
        {
            _engineOutIndex = 1;
        }

        switch (_engineOutIndex)
        {
            case 0:
                _enginesOutVisuals[0].SetActive(true);
                _engine00_Out = true;
                break;
            case 1:
                _enginesOutVisuals[1].SetActive(true);
                _engine01_Out = true;
                break;
            default:
                Debug.Log("Invalid random number");
                break;
        }
    }

    public void AddToPlayerScore(int points)
    {
        _score += 10;

        if (_UIScript != null)
        {
            _UIScript.UpdateScore(_score);
        }
    }
}
