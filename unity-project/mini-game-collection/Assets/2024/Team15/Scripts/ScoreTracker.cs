using UnityEngine;
using UnityEngine.UI; // Image reference
using TMPro;

namespace MiniGameCollection.Games2024.Team15
{
    public class ScoreTracker : MonoBehaviour
    {
        // Singleton instance
        public static ScoreTracker Instance;

        [SerializeField] private int player1Lives = 3;
        [SerializeField] private int player2Lives = 3;

        [SerializeField] private GameObject winScreen;
        [SerializeField] private TextMeshProUGUI winText;

        // Heart Image references
        [SerializeField] private Image player1Heart1;
        [SerializeField] private Image player1Heart2;
        [SerializeField] private Image player1Heart3;

        [SerializeField] private Image player2Heart1;
        [SerializeField] private Image player2Heart2;
        [SerializeField] private Image player2Heart3;

        // Initialize Singleton
        private void Awake()
        {
            // Ensure only one instance exists
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // Destroy this instance if one already exists
            }
        }

        private void Start()
        {
            UpdateLivesUI();
            winScreen.SetActive(false); // Hide win screen by default
        }

        public void Player1LoseLife()
        {
            player1Lives--;
            UpdateLivesUI();
            CheckGameEnd();
        }

        public void Player2LoseLife()
        {
            player2Lives--;
            UpdateLivesUI();
            CheckGameEnd();
        }

        private void UpdateLivesUI()
        {
            // Update heart visibility based on lives
            player1Heart1.enabled = player1Lives >= 1;
            player1Heart2.enabled = player1Lives >= 2;
            player1Heart3.enabled = player1Lives >= 3;

            player2Heart1.enabled = player2Lives >= 1;
            player2Heart2.enabled = player2Lives >= 2;
            player2Heart3.enabled = player2Lives >= 3;
        }

        private void CheckGameEnd()
        {
            if (player1Lives <= 0)
            {
                EndGame("Player 2 Wins!");
            }
            else if (player2Lives <= 0)
            {
                EndGame("Player 1 Wins!");
            }
        }

        private void EndGame(string winner)
        {
            winScreen.SetActive(true); // Show win screen
            winText.text = winner;     // Set the winner text

            // Optionally, stop further gameplay
            Time.timeScale = 0f; // Pauses the game when a winner is declared
        }
    }
}