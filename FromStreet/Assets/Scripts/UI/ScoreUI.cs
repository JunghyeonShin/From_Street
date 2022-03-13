using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;

    [SerializeField] private GameObject _bestScoreObject = null;

    [SerializeField] private Text _currentScoreText = null;

    [SerializeField] private Text _bestScoreText = null;

    private int _currScore = 0;

    private const string BEST_SCORE = "BestScore";

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            if (0 != _currScore)
            {
                UpdateCurrentBestScoreText();

                GameManager.Instance.EndGame();
            }
        }
        else
        {
            if (EPlayerMoveDirections.None == _player.gameObject.GetComponent<PlayerMovement>().PlayerMoveDirection)
            {
                int compareScore = _currScore;

                _currScore = (int)(_player.transform.position.z - 3) / 2;

                UpdateCurrentScoreText(compareScore);
            }
        }
    }

    private void UpdateCurrentScoreText(int compareScore)
    {
        if (_currScore > compareScore)
        {
            _currentScoreText.text = $"{_currScore}";
        }
    }

    private void UpdateCurrentBestScoreText()
    {
        _bestScoreObject.SetActive(true);

        int currBestScore = PlayerPrefs.GetInt(BEST_SCORE);

        if (currBestScore < _currScore)
        {
            currBestScore = _currScore;

            PlayerPrefs.SetInt(BEST_SCORE, currBestScore);
        }

        _bestScoreText.text = $"{currBestScore}";
    }
}
