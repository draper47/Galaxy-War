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
    [SerializeField] private float _gameOverFlickerInterval = 1f;

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
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLivesUI(int livesLeft)
    {
        _imageForLives.sprite = _livesSprites[livesLeft];
    }

    public IEnumerator StartGameOverAnimation()
    {
        _restartOrMainMenuMessage.SetActive(true);

        while (true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(_gameOverFlickerInterval);
            
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(_gameOverFlickerInterval);
        }
    }

    public void StopGameOverAnimation()
    {
        _restartOrMainMenuMessage.SetActive(false);

        StopCoroutine(StartGameOverAnimation());
    }
}
