using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        // Reloads the current scene (restart the game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
