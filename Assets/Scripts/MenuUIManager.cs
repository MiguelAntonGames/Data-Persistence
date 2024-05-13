using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour {
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TMP_InputField _playerNameInput;

    private void Start() {
        if (MainManager.Instance.HighScore == 0) {
            _bestScoreText.text = $"Best Score: 0";
        } else {
            _playerNameInput.text = MainManager.Instance.CurrentPlayerName;
            _bestScoreText.text = $"Best Score: {MainManager.Instance.HighScorePlayerName}: {MainManager.Instance.HighScore}";
        }
        _mainMenu.SetActive(true);
    }

    public void StartGame() {
        MainManager.Instance.StartGame(_playerNameInput.text);
    }

    public void Exit() {
        MainManager.Instance.SaveAllData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
