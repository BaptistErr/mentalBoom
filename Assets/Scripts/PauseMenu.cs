using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject pauseMenuUI;
    public GameObject TutoUI;

    private void Start()
    {
        GameIsPause = GameManager.Instance.IsGamePaused;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        if (TutoUI)
        {
            TutoUI.SetActive(false);
        }

        // Resume the game
        GameIsPause = false;
        SetGameInPause();

        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (TutoUI)
        {
            TutoUI.SetActive(true);
        }

        // Pause the game
        GameIsPause = true;
        SetGameInPause();

        Cursor.visible = true;
    }

    void SetGameInPause()
    {
        GameManager.Instance.IsGamePaused = GameIsPause;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
