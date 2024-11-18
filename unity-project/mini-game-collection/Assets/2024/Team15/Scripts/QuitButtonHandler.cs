using UnityEngine;
using UnityEngine.SceneManagement;  // Import SceneManager for scene loading
using UnityEngine.UI;  // Import UI for Button reference

public class EndGameController : MonoBehaviour
{
    [SerializeField] private Button quitButton;  // Reference to the Quit Button

    private void Start()
    {
        // Check if the Quit Button is set in the inspector
        if (quitButton != null)
        {
            // Add listener to the Quit button to call the QuitGame method when clicked
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    // Method to load the start screen scene
    private void QuitGame()
    {
        // Change "StartScene" to the actual name of your start scene
        SceneManager.LoadScene("StartScene");  // Load the start screen
    }
}