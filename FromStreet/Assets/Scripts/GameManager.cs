using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            IsGameOver = false;
        }
    }

    public void EndGame()
    {
        IsGameOver = true;
    }
}
