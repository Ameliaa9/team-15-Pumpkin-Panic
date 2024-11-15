using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGameCollection.Games2024.Team15
{
    public class RestartButton : MonoBehaviour
    {
        public void RestartGame()
        {
            // Reloads the current scene (restart the game)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}