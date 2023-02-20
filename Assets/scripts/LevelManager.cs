using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void LoadSandBox() {
        Debug.Log("Loading sandbox");
        SceneManager.LoadScene("Sandbox");
    }

    public void QuitGame() {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void LoadLevel(string levelName) {
        Debug.Log("Loading level: " + levelName);
        SceneManager.LoadScene(levelName);
    }

    public void LoadWorldTest() {
        Debug.Log("Loading world test");
        SceneManager.LoadScene("WorldTest");
    }
}
