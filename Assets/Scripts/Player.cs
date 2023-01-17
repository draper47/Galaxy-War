using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _totalSpeed;
    [SerializeField] private float _baseSpeed = 5f;
    [SerializeField] private float _powerupSpeedBoost = 3f;

    [SerializeField] private float _thrusterSpeedBoost = 5;
    [SerializeField] private float _thrusterCooldownRate = 10;
    [SerializeField] private float _thrusterHeatBuildup = 0;
    [SerializeField] private float _thrusterHeatBuildupRate = 50;
    private bool _coolingDownThruster;

    [SerializeField] private float _xBounds;
    [SerializeField] private float _yBounds;
    [SerializeField] private float _yCenter;

    [SerializeField] private int _lives;
    [SerializeField] private int _maxLives = 3;

    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private Vector3 _laserOffset;
    [SerializeField] private Spawner _spawnerScript;

    [SerializeField] private int _ammoMax = 15;
    [SerializeField] private int _ammoLeft;

    [SerializeField] private GameObject _singleShotPrefab;
    [SerializeField] private GameObject _trippleShotPrefab;

    private int _ammoTypeIndex;
    private float _nextFire = 0.0f;
    private bool _firingTrippleShot;

    private bool _firingExplodingLaser;
    [SerializeField] private GameObject _explodingLaserPrefab;

    [SerializeField] private bool _shieldsUp;
    [SerializeField] private GameObject _shieldsVisualizer;
    [SerializeField] private int _shieldHitsMax = 2;
    private int _shieldHitsLeft = 0;
    [SerializeField] private Color[] _shieldColors;

    [SerializeField] private float _powerupTimer = 5;

    private int _score = 0;
    private UIManager _uiScript;
    private GameManager _gameManagerScript;

    [SerializeField] private GameObject[] _enginesOutVisuals;

    [SerializeField] private GameObject _explosionPrefab;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _lives = _maxLives;
        _totalSpeed = _baseSpeed;
        _thrusterHeatBuildup = 0;
        _ammoLeft = _ammoMax;
        _ammoTypeIndex = 0;

        _uiScript = GameObject.Find("UI_Manager").transform.GetComponent<UIManager>();
        
        if (_uiScript == null )
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
        
        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextFire && _ammoLeft > 0)
        {
            FireProjectile();
            _nextFire = Time.time + _fireRate;
        }

        if (Input.GetAxis("Thruster") > 0 && _thrusterHeatBuildup < 1 && !_coolingDownThruster)
        {
            ThrusterOn();
        }
        else
        {
            _totalSpeed = _baseSpeed;

            ThrusterCooldown();
        }

        if (_uiScript != null)
        {
            _uiScript.UpdateThrusterHeatBuildupBar(_thrusterHeatBuildup, _coolingDownThruster);
        }

        if (_ammoLeft < 1 && Time.time >= _nextFire && _ammoTypeIndex > 0)
        {
            _ammoTypeIndex = 0;
            _ammoLeft = _ammoMax;
        }
    }

    void ThrusterOn()
    {
        float thrusterIn = Input.GetAxis("Thruster");

        _totalSpeed = _baseSpeed + (thrusterIn * _thrusterSpeedBoost);
        _thrusterHeatBuildup += (Time.deltaTime * _thrusterHeatBuildupRate);

        if (_thrusterHeatBuildup >= 1)
        {
            _coolingDownThruster = true;
        }
    }

    void ThrusterCooldown()
    {
        if (_thrusterHeatBuildup > 0)
        {
            _thrusterHeatBuildup -= Time.deltaTime * _thrusterCooldownRate;
        }

        if (_thrusterHeatBuildup <= 0)
        {
            _coolingDownThruster = false;
        }
    }

    void FireProjectile()
    {
        switch (_ammoTypeIndex)
        {
            default:
                FireSingleShot();
                _ammoLeft -= 1;
                break;
            case 1:
                FireTrippleShot();
                _ammoLeft -= 3;
                break;
            case 2:
                FireExplodingLaser();
                _ammoLeft -= 5;
                break;
        }

        _uiScript.UpdateAmmoClipUI(_ammoLeft);
    }

    public void RefillAmmo()
    {
        _ammoLeft = _ammoMax;

        _uiScript.UpdateAmmoClipUI(_ammoLeft);
    }

    private void FireSingleShot()
    {
        Instantiate(_singleShotPrefab, transform.position + _laserOffset, Quaternion.identity);
    }

    private void FireTrippleShot()
    {
        Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
    }

    private void FireExplodingLaser()
    {
        Instantiate(_explodingLaserPrefab, transform.position + _laserOffset, Quaternion.identity);
    }

    public void ActivateTrippleShot()
    {
        _ammoTypeIndex = 1;
    }

    public void ActivateExplodingLaser()
    {
        _ammoTypeIndex = 2;
    }

    public IEnumerator ActivateSpeedBoost()
    {
        _baseSpeed += _powerupSpeedBoost;
        
        yield return new WaitForSeconds(_powerupTimer);
        
        _baseSpeed -= _powerupSpeedBoost;
    }

    public void ActivateShield()
    {
        _shieldsUp = true;
        _shieldHitsLeft = _shieldHitsMax;
        _shieldsVisualizer.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        _shieldsVisualizer.SetActive(true);
    }

    void CalcMovement()
    {
        float horizontalIn = Input.GetAxis("Horizontal");
        float verticalIn = Input.GetAxis("Vertical");
        

        transform.Translate(Vector3.up * verticalIn * (_totalSpeed / 2f) * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalIn * _totalSpeed * Time.deltaTime);

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
        if (_shieldsUp)
        {
            ShieldsUpDamage();
        }
        else
        {
            ShieldsDownDamage();
        }
    }

    public void Repair()
    {
        if (_lives < _maxLives)
        {
            _lives += 1;
        }

        _uiScript.UpdateLivesUI(_lives);

        switch (_lives)
        {
            case 3:
                _enginesOutVisuals[0].SetActive(false);
                _enginesOutVisuals[1].SetActive(false);
                break;
            case 2:
                int randomEngineIndex = UnityEngine.Random.Range(0, _enginesOutVisuals.Length);
                _enginesOutVisuals[randomEngineIndex].SetActive(false);
                break;
            default:
                break;
        }
    }

    void ShieldsUpDamage()
    {
        SpriteRenderer shieldSpriteRenderer = _shieldsVisualizer.GetComponent<SpriteRenderer>();

        if (shieldSpriteRenderer == null)
        {
            Debug.LogError("Player.shieldColor == NULL");
        }

        switch (_shieldHitsLeft)
        {
            case 2:
                _shieldHitsLeft = 1;
                shieldSpriteRenderer.color = new Color(1f, 1f, 1f, .66f);
                break;
            case 1:
                _shieldHitsLeft = 0;
                shieldSpriteRenderer.color = new Color(1f, 1f, 1f, .33f);
                break;
            case 0:
                _shieldsUp = false;
                _shieldsVisualizer.SetActive(false);
                Debug.Log("Sheilds down.");
                break;
        }
    }

    void ShieldsDownDamage()
    {
        _lives -= 1;

        if (_uiScript != null)
        {
            _uiScript.UpdateLivesUI(_lives);
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
        int randomEngineOut = UnityEngine.Random.Range(0, _enginesOutVisuals.Length);

        switch (_lives)
        {
            case 1:
                _enginesOutVisuals[0].SetActive(true);
                _enginesOutVisuals[1].SetActive(true);
                break;
            case 2:
                _enginesOutVisuals[randomEngineOut].SetActive(true);
                break;
        }
    }

    public void AddToPlayerScore(int points)
    {
        _score += 10;

        if (_uiScript != null)
        {
            _uiScript.UpdateScore(_score);
        }
    }
}
