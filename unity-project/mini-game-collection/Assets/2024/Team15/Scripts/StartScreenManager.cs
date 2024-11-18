using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace MiniGameCollection.Games2024.Team15
{
    public class StartScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject startScreen;  // The start screen UI
        [SerializeField] private GameObject gameScreen;   // The game screen UI (where the actual gameplay happens)
        [SerializeField] private Button startButton;      // The start button

        [SerializeField] private GameObject livesUI;     // The lives UI elements (e.g., hearts, etc.)
        [SerializeField] private GameObject timerUI;     // The timer UI
        [SerializeField] private TextMeshProUGUI timerText; // Timer Text
        [SerializeField] private TextMeshProUGUI livesText; // Lives Text (if you use text for lives)
        [SerializeField] private TextMeshProUGUI stunText; // Stun Text (if you want to hide this on start)

        // Start is called before the first frame update
        private void Start()
        {
            // Make sure the start screen is active at the beginning
            startScreen.SetActive(true);
            gameScreen.SetActive(false);  // Hide the game screen initially
            livesUI.SetActive(false);     // Hide the lives UI initially
            timerUI.SetActive(false);     // Hide the timer UI initially
            stunText.gameObject.SetActive(false); // Hide the stun text initially

            // Assign the start game action to the button's OnClick event
            startButton.onClick.AddListener(StartGame);

            // Ensure the game is paused initially
            Time.timeScale = 0f; // Pauses the game
        }

        // Method that starts the game when the start button is clicked
        private void StartGame()
        {
            Debug.Log("StartGame called!"); // Debugging line to confirm the method is called

            // Hide the start screen and show the game screen
            startScreen.SetActive(false);
            gameScreen.SetActive(true);

            // Show the lives and timer UI
            livesUI.SetActive(true);
            timerUI.SetActive(true);

            // Show the stun text when the game starts
            stunText.gameObject.SetActive(true);

            // Unpause the game
            Time.timeScale = 1f; // Unpauses the game

            // Start the countdown timer
            StartCoroutine(CountdownTimer());
        }

        private IEnumerator CountdownTimer()
        {
            float timeRemaining = 30f;  // 30 seconds countdown
            while (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(timeRemaining);
                yield return null;
            }
        }

        private void UpdateTimerUI(float timeLeft)
        {
            int seconds = Mathf.CeilToInt(timeLeft); // Converts the time left into seconds
            timerText.text = $"Time Left: {seconds}";  // Updates the TextMeshPro UI with the remaining time
        }
    }
}