using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class timer : MonoBehaviour
{
    [SerializeField] private float timeStart = 60;
    public Text textScore;
    public GameObject endScreen;
    public Score score;
    public Text textEnd;
    // Start is called before the first frame update
    void Start()
    {
        textScore.text = timeStart.ToString();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timeStart = timeStart - Time.deltaTime;
        int currentTime= (int)Mathf.Round(timeStart);
        int min = currentTime / 60;
        int seconds = currentTime - (60 * min);
		string minDisplay;
		if(min<10){
			minDisplay="0"+min.ToString();
		}
		else{
			minDisplay=min.ToString();
		}
		string secDisplay;
		if(seconds<10){
			secDisplay="0"+seconds.ToString();
		}
		else{
			secDisplay=seconds.ToString();
		}
        textScore.text = minDisplay + ":" + secDisplay;
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
