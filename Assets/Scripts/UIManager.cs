using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _imageForLives;
    [SerializeField] private GameObject _restartOrMainMenuMessage;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private GameObject _gameManager;
    private GameManager _gameManagerScript;

    private float _flickerTimer;
    [SerializeField] private float _flickerInterval = 1f;

    private Slider _thrusterHeatBuildupSlider;
    [SerializeField] private GameObject _thrusterHeatBuildupBar;
    [SerializeField] private GameObject _thrusterSilderFill;

    

    void Start()
    {
        _scoreText.text = "Score: 0";

        _gameOverText.SetActive(false);
        _restartOrMainMenuMessage.SetActive(false);

        _gameManagerScript = _gameManager.GetComponent<GameManager>();
        
        if(_gameManagerScript == null )
        {
            Debug.Log("_gameManagerScript is NULL");
        }

        _imageForLives = GameObject.Find("Lives_Display_image").GetComponent<Image>();

        _thrusterHeatBuildupSlider = _thrusterHeatBuildupBar.GetComponent<Slider>();

        if (_thrusterHeatBuildupSlider == null)
        {
            Debug.LogError("UIManager._thrusterHeatBuildupSlider == NULL");
        }

   
    }

    void Update()
    {
        _flickerTimer += Time.deltaTime;

        if (_flickerTimer > _flickerInterval)
        {
            _flickerTimer = 0;
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLivesUI(int livesLeft)
    {
        _imageForLives.sprite = _livesSprites[livesLeft];
    }

    public void UpdateThrusterHeatBuildupBar(float heatBuildupPercentage, bool coolingDownThruster)
    {
        Image fillColor = _thrusterSilderFill.GetComponent<Image>();

        if (_thrusterHeatBuildupSlider != null)
        {
            _thrusterHeatBuildupSlider.value = heatBuildupPercentage;

            //redOverlayFill.color = new Color(1f, .5f, 0, _thrusterHeatBuildupSlider.value - .1f);

            fillColor.color = new Color(1f, Mathf.Clamp(1f - heatBuildupPercentage, .5f, 1f), Mathf.Clamp(1f - heatBuildupPercentage, .3f, 1f));
        }
        
        if (coolingDownThruster)
        {
            FlashingRedHeatBuildupBar();
        }
    }

    private void FlashingRedHeatBuildupBar()
    {
        Image fillColor = _thrusterSilderFill.GetComponent<Image>();

        switch (_flickerTimer)
        {
            case float redOverlayFlickerOnRange when (redOverlayFlickerOnRange < _flickerInterval / 2):
                fillColor.color = new Color(1f, 1f, 1f);
                break;
            case float redOverlayFlickerOnRange when (redOverlayFlickerOnRange > _flickerInterval / 2):
                fillColor.color = new Color(1f, .5f, .3f);
                break;
            default:
                fillColor.color = new Color(1f, 1f, 1f);
                break;
        }
    }

    public void UpdateAmmoClipUI(int shotsLeft)
    {
        Slider ammoClipSlider = GameObject.Find("AmmoClip").GetComponent<Slider>();

        if (ammoClipSlider != null)
        {
            ammoClipSlider.value = shotsLeft;
        }
    }

    public IEnumerator StartGameOverAnimation()
    {
        _restartOrMainMenuMessage.SetActive(true);

        while (true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(_flickerInterval);
            
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(_flickerInterval);
        }
    }

    public void StopGameOverAnimation()
    {
        _restartOrMainMenuMessage.SetActive(false);

        StopCoroutine(StartGameOverAnimation());
    }
}
