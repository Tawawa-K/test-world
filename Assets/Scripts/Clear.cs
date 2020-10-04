using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public Text clearText;
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        clearText.color = new Color(0.1f, 0.9f, 0.1f);
        Time.timeScale = 0;
    }
}
