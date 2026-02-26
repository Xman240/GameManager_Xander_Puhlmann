using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public string TitleSceneName;
    
    public InputAction pauseInput;
    
    private bool isPaused = false;


    private void Start()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }


    private void OnEnable()
    {
        pauseInput.Enable();
        pauseInput.performed += TogglePause;
    }

    private void OnDisable()
    {
        pauseInput.Disable();
        pauseInput.performed -= TogglePause;
    }

    private void TogglePause(InputAction.CallbackContext ctx)
    {
        if (isPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
    }


    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void SaveAndExit()
    {
        Time.timeScale = 1;
        isPaused = false;

        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.SaveGameState();
        }

        if (JSonSaving.Instance != null)
        {
            JSonSaving.Instance.SaveData();
        }
        
        SceneManager.LoadScene(TitleSceneName);
    }
}
