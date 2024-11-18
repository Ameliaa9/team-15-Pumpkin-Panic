using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI; // Required for Button
using UnityEngine.SceneManagement; // Required for Scene Management

namespace MiniGameCollection.Games2024.Team15
{
    public class ScoreTracker : MonoBehaviour
    {
        // Singleton instance
        public static ScoreTracker Instance;

        private const int InitialLives = 3; // Easily adjustable starting lives

        [SerializeField] private int player1Lives = InitialLives;
        [SerializeField] private int player2Lives = InitialLives;
        [SerializeField] private TextMeshProUGUI player1StunText;
        [SerializeField] private TextMeshProUGUI player2StunText;

        [SerializeField] private GameObject endGameScreen; // Single screen for game results
        [SerializeField] private TextMeshProUGUI endGameText; // Text for showing result message
        [SerializeField] private TextMeshProUGUI timerText; // Timer UI

        [SerializeField] private Image player1Heart1;
        [SerializeField] private Image player1Heart2;
        [SerializeField] private Image player1Heart3;

        [SerializeField] private Image player2Heart1;
        [SerializeField] private Image player2Heart2;
        [SerializeField] private Image player2Heart3;

        [SerializeField] private GameObject stunUI; // Stun UI element (assuming you have a reference to this)

        public float gameTime = 30f; // Total game time (seconds)
        private float timeRemaining;
        private bool isGameOver = false;

        private int player1StunCount = 3; // Starting stun count for player 1
        private int player2StunCount = 3; // Starting stun count for player 2

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Optional: Keep the ScoreTracker across scenes
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (!ValidateUIReferences())
            {
                Debug.LogError("UI references are missing in ScoreTracker!");
                return;
            }

            timeRemaining = gameTime;
            UpdateLivesUI();

            // Ensure the end game screen is hidden at the start
            endGameScreen.SetActive(false);

            // Set time scale to normal to ensure game runs properly
            Time.timeScale = 1f;

            StartCoroutine(CountdownTimer());
        }

        private bool ValidateUIReferences()
        {
            return endGameScreen != null && endGameText != null && timerText != null;
        }

        private IEnumerator CountdownTimer()
        {
            while (timeRemaining > 0 && !isGameOver)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(Mathf.Max(0, timeRemaining));
                yield return null;
            }

            if (!isGameOver)
            {
                CheckGameEnd();
            }
        }

        private void UpdateTimerUI(float timeLeft)
        {
            int seconds = Mathf.CeilToInt(timeLeft);
            timerText.text = $"Time Left: {seconds}";
        }

        // Method to decrease Player 1's lives
        public void Player1LoseLife()
        {
            if (isGameOver) return;
            player1Lives--;
            UpdateLivesUI();
            CheckGameEnd();
        }

        // Method to decrease Player 2's lives
        public void Player2LoseLife()
        {
            if (isGameOver) return;
            player2Lives--;
            UpdateLivesUI();
            CheckGameEnd();
        }

        private void UpdateLivesUI()
        {
            // Update heart icons for Player 1
            player1Heart1.enabled = player1Lives >= 1;
            player1Heart2.enabled = player1Lives >= 2;
            player1Heart3.enabled = player1Lives >= 3;

            // Update heart icons for Player 2
            player2Heart1.enabled = player2Lives >= 1;
            player2Heart2.enabled = player2Lives >= 2;
            player2Heart3.enabled = player2Lives >= 3;
        }

        private void CheckGameEnd()
        {
            if (isGameOver) return;

            if (player1Lives <= 0)
            {
                EndGame("Player 2 Wins!");
            }
            else if (player2Lives <= 0)
            {
                EndGame("Player 1 Wins!");
            }
            else if (timeRemaining <= 0)
            {
                if (player1Lives > player2Lives)
                {
                    EndGame("Player 1 Wins!");
                }
                else if (player2Lives > player1Lives)
                {
                    EndGame("Player 2 Wins!");
                }
                else
                {
                    EndGame("It's a Draw!");
                }
            }
        }

        private void EndGame(string resultMessage)
        {
            isGameOver = true;
            Time.timeScale = 0f; // Stop the game when it's over
            endGameScreen.SetActive(true);
            endGameText.text = resultMessage;

            // Hide the timer UI when the winner screen is shown
            if (timerText != null)
            {
                timerText.enabled = false; // Hide timer when the game ends
            }

            // Stop the timer coroutine if it's still running
            StopCoroutine(CountdownTimer());

            // Reset the timer display visually
            timerText.text = "Time Left: 0"; // This shows a final timer of 0 when the game ends

            // Hide stun UI when the game ends
            if (stunUI != null)
            {
                stunUI.SetActive(false);
            }

            // Hide hearts and other UI elements (important to hide them when the winner screen appears)
            player1Heart1.enabled = false;
            player1Heart2.enabled = false;
            player1Heart3.enabled = false;
            player2Heart1.enabled = false;
            player2Heart2.enabled = false;
            player2Heart3.enabled = false;
        }

        public void RestartGame()
        {
            // Unpause the game and reset all variables
            isGameOver = false;
            Time.timeScale = 1f; // Resume time
            player1Lives = InitialLives;
            player2Lives = InitialLives;
            timeRemaining = gameTime;

            // Reset stun counts to 3 for both players
            player1StunCount = 3;
            player2StunCount = 3;

            // Update the TextMeshProUGUI elements to reflect the reset stun counts
            UpdateStunUI();

            // Reset hearts and other UI elements
            UpdateLivesUI();

            // Show heart images again
            player1Heart1.enabled = true;
            player1Heart2.enabled = true;
            player1Heart3.enabled = true;
            player2Heart1.enabled = true;
            player2Heart2.enabled = true;
            player2Heart3.enabled = true;

            // Hide the end game screen
            endGameScreen.SetActive(false);

            // Hide the timer UI when restarting the game
            if (timerText != null)
            {
                timerText.enabled = true; // Make sure the timer is visible when the game restarts
            }

            // Start the timer and game again
            StartCoroutine(CountdownTimer());
        }

        // Method to update stun UI elements
        private void UpdateStunUI()
        {
            // Update the TextMeshProUGUI elements to show the current stun counts
            if (player1StunText != null)
            {
                player1StunText.text = "Stuns Left: " + player1StunCount.ToString();
            }

            if (player2StunText != null)
            {
                player2StunText.text = "Stuns Left: " + player2StunCount.ToString();
            }
        }
    }
}
