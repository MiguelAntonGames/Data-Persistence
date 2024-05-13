using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour {
    [SerializeField] private int _lineCount = 6;
    [SerializeField] private Brick _brickPrefab;
    [SerializeField] private Rigidbody _ball;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _gameOverMessage;

    public static GameplayManager Instance;

    private bool _gameHasStarted = false;
    private int _points;
    private bool _gameOver = false;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start() {
        MainManager.Instance.OnNewHighScore += OnNewHighScore;
        UpdateScoreUI();
        if (MainManager.Instance.HighScore == 0) {
            _bestScoreText.text = $"Best Score: 0";
        } else {
            _bestScoreText.text = $"Best Score: {MainManager.Instance.HighScorePlayerName}: {MainManager.Instance.HighScore}";
        }
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < _lineCount; ++i) {
            for (int x = 0; x < perLine; ++x) {
                Vector3 position = new(-1.5f + step * x, 2.7f + i * 0.3f, 0);
                var brick = Instantiate(_brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.OnDestroyed.AddListener(AddPoints);
            }
        }
    }

    private void Update() {
        if (!_gameHasStarted) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _gameHasStarted = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new(randomDirection, 1, 0);
                forceDir.Normalize();

                _ball.transform.SetParent(null);
                _ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        } else if (_gameOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                MainManager.Instance.RestartGame();
            } else if (Input.GetKeyDown(KeyCode.Escape)) {
                MainManager.Instance.ReturnToMainMenu();
            }
        }
    }

    void AddPoints(int points) {
        _points += points;
        UpdateScoreUI();
    }

    public void GameOver() {
        _gameOver = true;
        _gameOverMessage.SetActive(true);
        MainManager.Instance.OnGameOver(_points);
        UpdateScoreUI();
    }

    private void UpdateScoreUI() {
        _scoreText.text = $"Score: {_points}";
    }
    private void OnNewHighScore() {
        _bestScoreText.text = $"Best Score: {MainManager.Instance.HighScorePlayerName}: {MainManager.Instance.HighScore}";
    }

    private void OnDestroy() {
        MainManager.Instance.OnNewHighScore -= OnNewHighScore;
    }

}
