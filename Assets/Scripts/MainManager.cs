using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {
    [HideInInspector] public string CurrentPlayerName = string.Empty;
    [HideInInspector] public string HighScorePlayerName = string.Empty;
    [HideInInspector] public int HighScore;
    public static MainManager Instance { get; private set; };
    public Action OnNewHighScore;
    private const string SAVE_FILE_NAME = "/savefile.json";

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }
    public void StartGame(string playerName) {
        CurrentPlayerName = playerName;
        SceneManager.LoadScene(1);
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }
    public void OnGameOver(int newScore) {
        if (newScore > HighScore) {
            HighScore = newScore;
            HighScorePlayerName = CurrentPlayerName;
            OnNewHighScore?.Invoke();
        }
        //I save data even if the Highest Score has not changed,
        //because I want to save possible name changes
        SaveAllData();
    }

    [System.Serializable]
    class SaveData {
        public string CurrentPlayerName;
        public string HighScorePlayerName;
        public int HighScore;
    }

    public void SaveAllData() {
        SaveData data = new() {
            CurrentPlayerName = CurrentPlayerName,
            HighScorePlayerName = HighScorePlayerName,
            HighScore = HighScore
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + SAVE_FILE_NAME, json);
    }

    private void LoadData() {
        string path = Application.persistentDataPath + SAVE_FILE_NAME;
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            CurrentPlayerName = data.CurrentPlayerName;
            HighScorePlayerName = data.HighScorePlayerName;
            HighScore = data.HighScore;
        }
    }

}
