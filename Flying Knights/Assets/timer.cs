using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public  float timeStart = 60;
    public Text textScore;
    public GameObject endScreen;
    public Score score;
    public Text textEnd;
    // Start is called before the first frame update
    void Start()
    {
        timeStart = 60;
        textScore.text = timeStart.ToString();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStart < 30 && timeStart>0)
        {
            score.addScore(10);
        }
        timeStart = timeStart - Time.deltaTime;
        int currentTime= (int)Mathf.Round(timeStart);
        int min = currentTime / 60;
        int seconds = currentTime - (60 * min);
        textScore.text = min + ":" + seconds;
        if (timeStart <= 0)
        {
            endScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            textEnd.text = "Temps écoulé votre score est de " + score.scoreValue + "\n Félicitations";
        }
    }
}
