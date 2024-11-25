using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerScores
    {
        public int player1score;
        public int player2score;
    }

    public int scoreToReach;
    private int player1score = 0;
    private int player2score = 0;

    public Text player1scoreText;
    public Text player2ScoreText;

    private string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/scores.json";
    }

    private void Start()
    {
        LoadScores();
        UpdateScoreUI();
    }

    public void Player1Goal()
    {
        player1score++;
        player1scoreText.text = player1score.ToString();
        SaveScores();
        CheckScore();
    }

    public void Player2Goal()
    {
        player2score++;
        player2ScoreText.text = player2score.ToString();
        SaveScores();
        CheckScore();
    }

    private void CheckScore()
    {
        if (player1score == scoreToReach || player2score == scoreToReach)
        {
            SceneManager.LoadScene(2); // Load the end game scene or leaderboard scene
        }
    }

    private void UpdateScoreUI()
    {
        player1scoreText.text = player1score.ToString();
        player2ScoreText.text = player2score.ToString();
    }

    public void SaveScores()
    {
        PlayerScores scores = new PlayerScores
        {
            player1score = player1score,
            player2score = player2score
        };

        string json = JsonUtility.ToJson(scores, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Scores saved to: " + filePath);
    }

    public void LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerScores scores = JsonUtility.FromJson<PlayerScores>(json);
            player1score = scores.player1score;
            player2score = scores.player2score;
            Debug.Log("Scores loaded from: " + filePath);
        }
        else
        {
            Debug.Log("No saved scores found.");
        }
    }
}
