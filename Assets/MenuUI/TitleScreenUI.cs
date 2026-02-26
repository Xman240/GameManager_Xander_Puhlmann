using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    public JSonSaving jsonSaving;
    public GameObject continueButton;

    public string GameSceneName;

    private void Start()
    {
        bool hasSave = jsonSaving != null && jsonSaving.HasSaveData();
        if (continueButton != null)
        {
            continueButton.SetActive(hasSave);
        }
    }

    public void OnContinueButtonPressed()
    {

        if (jsonSaving == null)
        {
            jsonSaving = JSonSaving.Instance;
        }
        
        if (jsonSaving.LoadData())
        {
            SceneManager.LoadScene(GameSceneName);
            
        }
    }
    public void OnNewGameButtonPressed()
    {
        if (jsonSaving == null)
        {
            jsonSaving = JSonSaving.Instance;
        }
        if (jsonSaving != null)
        {
            jsonSaving.DeleteSave();
        }
        GameStateManager.Instance.ResetToNewGame();
        SceneManager.LoadScene(GameSceneName);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
