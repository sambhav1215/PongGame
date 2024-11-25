using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LeaderboardUI : MonoBehaviour
{
    public Transform scoresContainer; // Container for score entries
    public GameObject leaderboardEntryPrefab; // Prefab for each leaderboard entry
    private string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/scores.json";
    }

    private void Start()
    {
        // Load and display the leaderboard at the start
        UpdateLeaderboardUI(LoadScores());
    }

    public void UpdateLeaderboardUI(ScoreManager.PlayerScores scores)
    {
        // Clear existing entries
        foreach (Transform child in scoresContainer)
        {
            Destroy(child.gameObject);
        }

        // Populate the leaderboard
        List<KeyValuePair<string, int>> playerScores = new List<KeyValuePair<string, int>>
        {
            new KeyValuePair<string, int>("Player 1", scores.player1score),
            new KeyValuePair<string, int>("Player 2", scores.player2score)
        };

        // Sort scores in descending order
        playerScores.Sort((a, b) => b.Value.CompareTo(a.Value));

        foreach (var playerScore in playerScores)
        {
            GameObject entry = Instantiate(leaderboardEntryPrefab, scoresContainer);
            Text[] texts = entry.GetComponentsInChildren<Text>();
            texts[0].text = playerScore.Key; // Player Name
            texts[1].text = playerScore.Value.ToString(); // Player Score
        }
    }

    public ScoreManager.PlayerScores LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<ScoreManager.PlayerScores>(json);
        }
        else
        {
            Debug.Log("No saved scores found for leaderboard.");
            return new ScoreManager.PlayerScores(); // Return an empty scores object
        }
    }
}
