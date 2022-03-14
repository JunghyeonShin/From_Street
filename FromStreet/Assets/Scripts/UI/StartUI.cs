using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private GameObject _camera = null;

    [SerializeField] private GameObject _startObject = null;

    [SerializeField] private Text _startText = null;

    private const string START_TEXT = "Press W to Start";

    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            if (_camera.transform.position.z <= -4.5)
            {
                _startObject.SetActive(true);
                
                _startText.text = START_TEXT;

                return;
            }
        }
        _startObject.SetActive(false);
    }
}
