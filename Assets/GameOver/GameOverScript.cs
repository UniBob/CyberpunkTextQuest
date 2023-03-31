using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] int mainSceneIndex;
    private void ResetState()
    {
        PlayerPrefs.SetInt("pageNumber", 0);
        PlayerPrefs.Save();
    }

    public void QuitGame()
    {     
        ResetState();
        Application.Quit();
    }

    public void ResetGame()
    {
        ResetState();
        SceneManager.LoadScene(mainSceneIndex);
    }
}
