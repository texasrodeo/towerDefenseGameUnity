using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private static bool isGameEnded;

    public static bool IsGameEnded => isGameEnded;

    public GameObject gameOverUI;

    public GameObject gameWonUI;

    private static GameController instance;

    public static GameController getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    private void Start()
    {
        isGameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnded)
        {
            return;
        }
        if ( PlayerController.CurrentHp <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        isGameEnded = true;
        gameOverUI.SetActive(true);
    }
    
    public void WinLevel ()
    {
        isGameEnded = true;
        gameWonUI.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
