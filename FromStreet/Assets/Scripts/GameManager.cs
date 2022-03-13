using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    private static GameManager _instance = null;

    private bool _isGameStart = false;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
        }

        IsGameOver = true;
    }

    private void Update()
    {
        StartGame();
    }

    private void StartGame()
    {
        if (false == _isGameStart && Input.GetKeyDown(KeyCode.W))
        {
            _isGameStart = true;

            IsGameOver = false;
        }
    }

    public void EndGame()
    {
        IsGameOver = true;
    }
}
