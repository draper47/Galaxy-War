using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _UIManager;
    [SerializeField] private UIManager _scriptUIManager;
    [SerializeField] private bool isGameOver;

    private void Start()
    {
        _scriptUIManager = _UIManager.GetComponent<UIManager>();
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
