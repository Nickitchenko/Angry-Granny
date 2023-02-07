using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public static int currentScore = 0;

    void Start()
    {
        scoreText.text = currentScore.ToString();
        EventManager.EnemyDied += OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        scoreText.text = currentScore.ToString();
    }

}
