using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;
    private static int score = 0;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreTxt.text = "Score: " + score;
    }

    public static void ResetScore()
    {
        score = 0;
    }
}
