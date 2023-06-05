using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused;

    public GameObject pauseMenuUI;
    public GameObject setingsMenuUI;

    bool AreSettingsOpened;

    void Start()
    {
        IsGamePaused = false;
        AreSettingsOpened = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!AreSettingsOpened)
            {
                if(IsGamePaused) Resume();
                else Pause();
            }
            else
            {
                pauseMenuUI.SetActive(true);
                setingsMenuUI.SetActive(false);
                AreSettingsOpened = false;
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        IsGamePaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        IsGamePaused = true;
    }

    public void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        setingsMenuUI.SetActive(false);
        AreSettingsOpened = false;
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        setingsMenuUI.SetActive(true);
        AreSettingsOpened = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
