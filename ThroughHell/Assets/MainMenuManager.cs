using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    public static bool isContinue;
    public GameObject continueButton;

    private void Update()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (!File.Exists(path))
        {
            continueButton.GetComponent<Button>().interactable=false;
        }
        else
        {
            continueButton.GetComponent<Button>().interactable = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("Save file deleted.");
            }
        }
    }

    public void NewGame()
    {
        isContinue = false;

        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        isContinue = true;

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
