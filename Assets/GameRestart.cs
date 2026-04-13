using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    public void RestartScene()
    {
        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}