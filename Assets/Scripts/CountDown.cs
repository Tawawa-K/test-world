using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text countDownTimer;
    public Text gameOverText;
    public float totalTime;
    public GameObject Player;
    private int second;

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        second = (int)totalTime;
        countDownTimer.text = second.ToString();

        if (second == 0)
        {
            GameObject.Find("Package04_animChanger").GetComponent<Animator>().applyRootMotion = false;
            gameOverText.color = new Color(1.0f, 0.1f, 0.1f, 1.0f);
            Time.timeScale = 0;
        }

        if(Player.transform.position.y <= -20.0f)
        {
            gameOverText.color = new Color(1.0f, 0.1f, 0.1f, 1.0f);
            Time.timeScale = 0;
        }
    }
}
