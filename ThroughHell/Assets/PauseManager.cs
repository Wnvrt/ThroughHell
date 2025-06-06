using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu, optionsMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (!optionsMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (optionsMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
