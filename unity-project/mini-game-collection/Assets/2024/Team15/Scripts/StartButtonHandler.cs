using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGameCollection.Games2024.Team15
{
    public class StartButtonHandler : MonoBehaviour
    {
        // This method will be called when the button is clicked
        public void StartGame()
        {
            // Load the next scene (replace "GameScene" with the actual scene name)
            SceneManager.LoadScene("GameScene");
        }
    }
}