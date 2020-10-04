using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private Text scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        scoreLabel = GameObject.Find("Score").GetComponent<Text>();
        scoreLabel.text = score.ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreLabel.text = score.ToString();
    }
}
