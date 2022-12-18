using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _imageForLives;
    [SerializeField] private GameObject _restartMessage;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private float _gameOverFlickerInterval = 1;

    void Start()
    {
        _scoreText.text = "Score: 0";

        _gameOverText.SetActive(false);
        _restartMessage.SetActive(false);

        _imageForLives = GameObject.Find("Lives_Display_image").GetComponent<Image>();
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLivesUI(int livesLeft)
    {
        _imageForLives.sprite = _livesSprites[livesLeft];

        if (livesLeft == 0) 
        {
            StartCoroutine(GameOverAnimation());
            _restartMessage.SetActive(true);
        }
    }

    IEnumerator GameOverAnimation()
    {
        while(true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(_gameOverFlickerInterval);
            
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(_gameOverFlickerInterval);
        }
    }
}
