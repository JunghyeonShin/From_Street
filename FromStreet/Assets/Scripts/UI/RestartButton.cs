using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _restartButton = null;

    private bool _restartGame = false;

    private const int GAME_SCENE = 0;

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            if (_restartGame)
            {
                _restartButton.gameObject.SetActive(true);
            }
        }
        else
        {
            _restartGame = true;

            _restartButton.gameObject.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }
}
