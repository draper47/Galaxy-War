using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _UIManager;
    [SerializeField] private UIManager _scriptUIManager;
    [SerializeField] private bool isGameOver;
    private AudioSource _backgroundMusic;

    private void Start()
    {
        _scriptUIManager = _UIManager.GetComponent<UIManager>();

        if(_scriptUIManager == null)
        {
            Debug.LogError("GameManager._scriptUIManager == NULL");
        }

        _backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();

        if(_backgroundMusic == null)
        {
            Debug.LogError("GameManager._backgroundMusic == NULL");
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            ResetGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        _backgroundMusic.Pause();

        if (_scriptUIManager != null)
        {
            _scriptUIManager.StartCoroutine("StartGameOverAnimation");
        }
    }
    private void ResetGame()
    {
        if (_scriptUIManager != null)
        {
            _scriptUIManager.StopGameOverAnimation();
        }

        SceneManager.LoadScene(1);
    }

    private void ReturnToMainMenu()
    {
        if (_scriptUIManager != null)
        {
            _scriptUIManager.StopGameOverAnimation();
        }

        SceneManager.LoadScene(0);
    }
}
