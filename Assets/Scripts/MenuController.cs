using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    public GameObject victoryCanvas;

    bool paused = false;

    // Use this for initialization
    void Start () {
        if (pauseCanvas) {
            pauseCanvas.SetActive(false);
        }

        if (gameOverCanvas) {
            gameOverCanvas.SetActive(false);
        }
        if (victoryCanvas) {
            victoryCanvas.SetActive(false);
        }
        Time.timeScale = 1f;
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    /// <summary>
    /// Starts the game scene.
    /// </summary>
    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Starts the game scene.
    /// </summary>
    public void StartGame() {
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Starts the 3D Application (Animation).
    /// </summary>
    public void Start3DApplication() {
        SceneManager.LoadScene("Animation3DScene");
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }

    /// <summary>
    /// Toggle games' pause.
    /// </summary>
    public void ToggleGamePause() {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        pauseCanvas.SetActive(paused);
    }

    /// <summary>
    /// Sets victory Canvas active.
    /// </summary>
    public void SetVictory() {
        Time.timeScale = 0f;
        victoryCanvas.SetActive(true);
    }

    /// <summary>
    /// Sets game over Canvas active.
    /// </summary>
    public void SetGameOver() {
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
    }

    /// <summary>
    /// Restarts the current active scene (Game or Application).
    /// </summary>
    public void RestartGame() {
        int index = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(index);
    }
}
