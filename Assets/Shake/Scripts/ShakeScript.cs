using UnityEngine;
using UnityEngine.SceneManagement;

public class ShakeScript : MonoBehaviour
{
    public void EndScene()
    {
        Debug.Log("I'm pressed");
        SceneManager.LoadScene(0);
    }
}
