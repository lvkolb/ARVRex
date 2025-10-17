using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMeshPro UI components

public class GameManager : MonoBehaviour
{
    // === UI Fields ===
    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winLoseText;

    // === Game State Fields ===
    [Header("Game Settings")]
    public float timeLimit = 60f; // 1 minute countdown
    private float currentTime;
    private bool isGameOver = false;

    // === Win/Lose Fields ===
    [Header("Win Condition")]
    public GameObject fireworksPrefab; // Assign your fireworks particle system here
    public ScoreController scoreController; // Drag the ScoreController here

    private int totalPickups;

    private void Start()
    {
        currentTime = timeLimit;
        winLoseText.gameObject.SetActive(false); // Hide message at start

        // Find all PickUps in the scene (assuming they are active at start)
        // This count is essential for the win condition.
        totalPickups = GameObject.FindGameObjectsWithTag("PickUp").Length;

        // Subscribe to the event when score/pickups change
        if (scoreController != null)
        {
            scoreController.OnScoreChanged.AddListener(CheckWinCondition);
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        // 7g) Timer Logic
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            EndGame(false); // Time is up!
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // Format time to display minutes and seconds
            int minutes = Mathf.FloorToInt(currentTime / 60F);
            int seconds = Mathf.FloorToInt(currentTime % 60F);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    // This method is automatically called when ScoreController.OnScoreChanged is invoked.
    public void CheckWinCondition()
    {
        // 7i) Check if all pickups are collected
        if (scoreController.Score >= totalPickups)
        {
            EndGame(true); // You won!
        }
    }

    private void EndGame(bool hasWon)
    {
        isGameOver = true;

        winLoseText.gameObject.SetActive(true);

        if (hasWon)
        {
            // 7i) "Congratulations" Display
            winLoseText.text = "CONGRATULATIONS!";

            // 7j) Firework/Confetti Particle System
            if (fireworksPrefab != null)
            {
                Instantiate(fireworksPrefab, transform.position, Quaternion.identity);
            }
        }
        else
        {
            // 7g) "Time's up" Display
            winLoseText.text = "Time's up! Try again.";
        }

        // Optionally freeze the player and game world
        Time.timeScale = 0f;
    }
}