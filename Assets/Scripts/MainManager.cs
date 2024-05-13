using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {
    public static MainManager Instance;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void SaveAllData() {
        
    }

    private void LoadData() {
        
    }

}
