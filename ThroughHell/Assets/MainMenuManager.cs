using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {

    }

    public void ContinueGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
